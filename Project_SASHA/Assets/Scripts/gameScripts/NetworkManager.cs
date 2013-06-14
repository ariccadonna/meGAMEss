using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;


public class NetworkManager : MonoBehaviour {
	private bool running = false;
	
	public Texture edu;
	public Texture ban;
	public Texture sci;
	public Texture mil;
	public Texture gov;
	public Texture bas;
	
	public GameObject rayPrefab; 
	private GameObject ray;
 	private float rayLenght;
 	private float rayRotation;
 	private float xt,xs,yt,ys;
 	private Quaternion rotation;
	
	private float prevTime, statTime;
	private float policeUpdateTime = 9.0f;
	private float statUpdatetime = 1.0f;
	
	private GameObject spots;
	private int playerNumbers;

	private Dictionary<string, Color> playerColors = new Dictionary<string,Color>();
	private Color[] colors = { Color.red, Color.green, Color.cyan, Color.magenta, Color.yellow };
	private string currentPlayer;
	public GameObject policePrefab = null;
	public GameObject glowParticlePrefab = null;
	private GameObject glow = null;
	
	private static NetworkManager instance;	
	public GameObject GatewayPrefab = null;
	private GameObject currentPolice;
	
	private System.Object messagesLocker = new System.Object();
	private SmartFox smartFox;
	private bool installSuccess = false;
	
	private bool lost = false;
	
	private ISFSObject shop;

	void Awake() 
	{
		instance = this;
	}

	void Start() 
	{
		playerNumbers = PlayerPrefs.GetInt("playerNumber");
		smartFox = SmartFoxConnection.Connection;
		if (smartFox == null) {
			Application.LoadLevel("loginScreen");
			return;
		}
		prevTime = Time.time;
		statTime = Time.time;
		currentPolice = Instantiate(policePrefab) as GameObject;
		currentPolice.transform.name = "Police";
		SubscribeDelegates();
		
		//DICHIARAZIONE CARTELLA SPOT
		spots = new GameObject();
		spots.transform.name = "spots";
		
		currentPlayer=smartFox.MySelf.Name;
	}
	
	void Update()
	{
	
		if(smartFox.LastJoinedRoom.UserCount == playerNumbers && !running)
		{
			SendWorldSetupRequest();
			ColorSetup();
			running = true;
		}
		if(Time.time - prevTime > policeUpdateTime)
		{
			prevTime = Time.time;
			AskPolicePosition();
		}
		
		if(Time.time - statTime > statUpdatetime)
		{
			statTime = Time.time;
			getStat();
		}
		
	}
	
	void FixedUpdate() {
		if (!running) return;
		smartFox.ProcessEvents();
	}

	private void SubscribeDelegates() {
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
		smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserLeaveRoom);
		smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener(SFSEvent.PUBLIC_MESSAGE, OnChatMessage);
	}
	
	private void UnsubscribeDelegates() 
	{
		smartFox.RemoveAllEventListeners();
	}
	
	private void OnChatMessage(BaseEvent evt) 
	{
		/*try {
			string message = (string)evt.Params["message"];
			User sender = (User)evt.Params["sender"];
	
			lock (messagesLocker) {
				GameInterface.messages.Add(sender.Name + ": " + message);
                GameInterface.chatScrollPosition.y = Mathf.Infinity;
			}
			
			Debug.Log("User " + sender.Name + ": " + message);
		}
		catch (Exception ex) {
			Debug.Log("Exception handling public message: "+ex.Message+ex.StackTrace);
		}*/
	}

   /* public void SendInfoRequest(String gatewayName) 
	{
        Room room = smartFox.LastJoinedRoom;
        ISFSObject data = new SFSObject();
        data.PutUtfString("selctedGateway", gatewayName);
        ExtensionRequest request = new ExtensionRequest("gatewayInfo", data, room);
        smartFox.Send(request);
    }*/
	
	public void SendWorldSetupRequest()
	{
		Room room = smartFox.LastJoinedRoom;
		ExtensionRequest worldSetupRequest = new ExtensionRequest("getWorldSetup", new SFSObject(), room);
		smartFox.Send(worldSetupRequest);

		ExtensionRequest objectiveRequest = new ExtensionRequest("getObjectives", new SFSObject(), room);
		smartFox.Send(objectiveRequest);
		
		ExtensionRequest policePositionRequest = new ExtensionRequest("policePosition", new SFSObject(), smartFox.LastJoinedRoom);
		smartFox.Send(policePositionRequest);
		
		ExtensionRequest getStats = new ExtensionRequest("playerInfo", new SFSObject(), smartFox.LastJoinedRoom);
		smartFox.Send(getStats);
		
		ExtensionRequest getShop = new ExtensionRequest("shopInfo", new SFSObject(), smartFox.LastJoinedRoom);
		smartFox.Send(getShop);
		
		
	}
	
	private void OnConnectionLost(BaseEvent evt) 
	{
        UnsubscribeDelegates();
		Screen.lockCursor = false;
		Screen.showCursor = true;
		SmartFoxConnection.Connection = null; 
		Application.LoadLevel("loginScreen");
	}
	
	private void OnExtensionResponse(BaseEvent evt) 
	{
		try {
			string cmd = (string)evt.Params["cmd"];
			ISFSObject data = (ISFSObject)evt.Params["params"];
											
			if (cmd == "hack") 
			{
				GameObject.Find("hackInfo").GetComponent<hackInfo>().isHacking = false;
				smartFox.Send(new ExtensionRequest("sync", new SFSObject(), smartFox.LastJoinedRoom));
				foreach(GameObject g in GameObject.FindGameObjectsWithTag("ray"))
					Destroy(g);
				if(data.GetBool("victoryReached") == true)
					DisplayWindow("YOUWON");
			}
			if (cmd == "syncWorld") 
			{
				Debug.Log("got sync");
				UpdateWorldSetup(data);
			}
			else if (cmd == "gatewayInfo") 
			{
				Debug.Log ("info request recived for "+data.GetUtfString("state"));	
				//handle result
			}
			else if (cmd == "getObjectives") 
			{
				UpdateObjective(data);
			}
			else if (cmd == "getWorldSetup")
			{
				InstantiateWorld(data);
			}
			else if (cmd == "policePosition")
			{
				currentPolice.transform.position = new Vector3(data.GetInt ("police_x"),data.GetInt ("police_y")+13, -5);
			}
			else if (cmd == "playerInfo")
			{
				GameObject.Find("playerStats").GetComponent<printPlayerStat>().printStat(data.GetUtfString("name"), data.GetInt("money"), data.GetLong("time"), playerColors[smartFox.MySelf.Name]);
				gameObject.GetComponent<referencePanel>().inventoryPanel.GetComponent<inventory>().refreshInventory(data.GetSFSArray("inventory"));
			}
			else if (cmd == "install")
			{
				installSuccess = data.GetBool("success");
				smartFox.Send(new ExtensionRequest("sync", new SFSObject(), smartFox.LastJoinedRoom));
				DisplayWindow("INSTALLED");
			}
			else if (cmd == "shopInfo")
			{
				ShopSetup(data);
			}
			else if (cmd == "buy")
			{
				DisplayWindow("SUCCESS");
			}
			else if (cmd == "error")
			{
				DisplayWindow(data.GetUtfString("error"));
			}
			else if (cmd == "lost")
			{
				DisplayWindow("LOST");
			}
			else if (cmd == "endGameInfo")
			{
				DisplayStats(data);
			}
			else if (cmd == "path")
				tracePath(data);
		}
		catch (Exception e) {
			Debug.Log("Exception handling response: "+e.Message+" >>> "+e.StackTrace);
		}
	}
	
	private void OnUserLeaveRoom(BaseEvent evt) 
	{
		User user = (User)evt.Params["user"];
		Room room = (Room)evt.Params["room"];
		smartFox.Send(new ExtensionRequest("sync", new SFSObject(), smartFox.LastJoinedRoom));
		if(room.UserList.Count == 1)
			DisplayWindow("YOUWON");
        Debug.Log("User " + user.Name + " left room " + room);
	}
	
	void OnApplicationQuit() 
	{
		UnsubscribeDelegates();
		smartFox.Disconnect();
	}
	
	public void SendLeaveRoom() 
	{
		smartFox.Send(new JoinRoomRequest("Lobby", null, smartFox.LastJoinedRoom.Id));
		UnsubscribeDelegates();
		Application.LoadLevel("loginScreen");
	}
	
	public void SendHackRequest(String fromGateway, String toGateway) 
	{
		if(lost)
		{
			DisplayWindow("ACTIONSDISABLED");
			return;
		}
		ISFSObject data = new SFSObject();
		data.PutUtfString("gatewayFrom", fromGateway);
		data.PutUtfString("gatewayTo", toGateway);
		data.PutBool("neutralize", false);
		smartFox.Send(new ExtensionRequest("hack", data, smartFox.LastJoinedRoom));
		
	}
	
	public void SendNeutralizeRequest(String fromGateway, String toGateway) {
		if(lost)
		{
			DisplayWindow("ACTIONSDISABLED");
			return;
		}
		ISFSObject data = new SFSObject();
		data.PutUtfString("gatewayFrom", fromGateway);
		data.PutUtfString("gatewayTo", toGateway);
		data.PutBool("neutralize", true);
		smartFox.Send(new ExtensionRequest("hack", data, smartFox.LastJoinedRoom));
	}

	public void ColorSetup()
	{
		int i = 0;
		List<User> userList = smartFox.LastJoinedRoom.UserList;
		foreach (User user in userList)
		{
			playerColors.Add(user.Name,colors[i]);
			i++;
		}
		playerColors.Add ("Neutral", Color.gray);
	}
	
	public bool installSoftware(string name, string gateway)
	{
		if(lost)
		{
			DisplayWindow("ACTIONSDISABLED");
			return false;
		}
		ISFSObject data = new SFSObject();
		data.PutUtfString("software", name);
		data.PutUtfString("gateway", gateway);	
		smartFox.Send(new ExtensionRequest("install", data, smartFox.LastJoinedRoom));
		
		return installSuccess;
	}
	
	public string getCurrentPlayer()
	{
		return currentPlayer;
	}
	
	private void InstantiateWorld(ISFSObject data)
	{
		String[] keys = data.GetKeys();
				foreach(String currentKey in keys)
				{
					ISFSObject currentObject = data.GetSFSObject(currentKey);
					GameObject currentGw = Instantiate(GatewayPrefab) as GameObject;
					currentGw.transform.name = currentObject.GetUtfString("STATE");
					Vector3 temp = new Vector3((float)currentObject.GetInt("X"),(float)currentObject.GetInt("Y"),1);
					
					currentGw.transform.position = temp;
					Gateway gw = currentGw.GetComponent<Gateway>();
					
					gw.Setup(currentObject);
					
					gw.GetComponent<OTSprite>().tintColor = playerColors[gw.getOwner()];
					stopParticle(gw);
			
					if(gw.getOwner() == smartFox.MySelf.Name)
					{
						startParticle(gw);
						StartCoroutine(stopParticle(gw, 3.0f));
					}
			
					if (!GameObject.Find(currentObject.GetUtfString("REGION")))
					{
						GameObject region = new GameObject();
						region.transform.name=currentObject.GetUtfString("REGION");
						region.transform.parent=spots.transform;
						gw.transform.parent=region.transform;
					}
					else
					{
						GameObject region=GameObject.Find(currentObject.GetUtfString("REGION"));
						gw.transform.parent=region.transform;
					}
					
					
					switch(currentObject.GetUtfString("TYPE"))
					{
						case "Educational":
							gw.GetComponent<OTSprite>().image=edu;
						break;
						case "Government":
							gw.GetComponent<OTSprite>().image=gov;
						break;
						case "Scientific":
							gw.GetComponent<OTSprite>().image=sci;
						break;
						case "Military":
							gw.GetComponent<OTSprite>().image=mil;
						break;
						case "Bank":
							gw.GetComponent<OTSprite>().image=ban;
						break;
						default:
							gw.GetComponent<OTSprite>().image=bas;
						break;
					}
					
				}
	}
	
	private void ShopSetup(ISFSObject data)
	{
		this.shop = data;
	}
	
	public ISFSObject getShop()
	{
		return this.shop;
	}
	
	private void UpdateWorldSetup(ISFSObject data)
	{
		String[] keys = data.GetKeys();
		foreach(String currentKey in keys)
		{
			ISFSObject currentObject = data.GetSFSObject(currentKey);
			GameObject currentGw = GameObject.Find(currentObject.GetUtfString("STATE"));
			Gateway gw = currentGw.GetComponent<Gateway>();
			gw.Setup(currentObject);
			gw.GetComponent<OTSprite>().tintColor = playerColors[gw.getOwner()];
			gw.GetComponent<otMouseInput>().setPrevColor(playerColors[gw.getOwner()]);
		}
		ExtensionRequest objectiveRequest = new ExtensionRequest("getObjectives", new SFSObject(), smartFox.LastJoinedRoom);
		smartFox.Send(objectiveRequest);
		AskPolicePosition();
	}
	
	private void UpdateObjective(ISFSObject data)
	{
		GameObject obj = GameObject.Find("objectives");
		printObjectives objectives = obj.GetComponent<printObjectives>();
		objectives.ObjectivesUpdate(data);
		AskPolicePosition();
	}
	
	public void AskPolicePosition()
	{
		ExtensionRequest policePositionRequest = new ExtensionRequest("policePosition", new SFSObject(), smartFox.LastJoinedRoom);
		smartFox.Send(policePositionRequest);
	}
	
	private void startParticle(Gateway gw)
	{
		gw.particleSystem.Play();
	}
	
	private IEnumerator stopParticle(Gateway gw, float time)
	{
		yield return new WaitForSeconds(time);
		gw.particleSystem.Stop();
	}
	
	private void stopParticle(Gateway gw)
	{
		gw.particleSystem.Stop();
	}
	
	private void getStat()
	{
		ExtensionRequest getStats = new ExtensionRequest("playerInfo", new SFSObject(), smartFox.LastJoinedRoom);
		smartFox.Send(getStats);
	}
	
	private void tracePath(ISFSObject data)
	{
		if(lost)
		{
			DisplayWindow("ACTIONSDISABLED");
			return;
		}
		GameObject.Find("hackInfo").GetComponent<hackInfo>().isHacking = true;
		ISFSArray path = data.GetSFSArray("hackingPath");
		int i = 0;
		while(i < path.Size()-1)
		{
			String startC = (String) path.GetElementAt(i);
			String[] coord_1 = startC.Split(':');
			String targetC =(String) path.GetElementAt(i+1);
			String[] coord_2 = targetC.Split(':');
			
			
			
			Vector3 p0 = new Vector3(int.Parse(coord_1[0]), int.Parse(coord_1[1]), 5);
			Vector3 p1 = new Vector3(int.Parse(coord_2[0]), int.Parse(coord_2[1]), 5);
			Vector3 direction = (Vector3)((p1 - p0).normalized);
			float distance = Vector3.Distance(p1,p0);
			
			ray = Instantiate(rayPrefab) as GameObject;
			ray.transform.position = new Vector2((p1.x+p0.x)/2,(p1.y+p0.y)/2);
					    
			rayLenght = distance;
			
			ray.transform.localScale = new Vector2(rayLenght,1);

			float inclinazioneRay = Mathf.Atan((p1.y-p0.y)/(p1.x-p0.x))*Mathf.Rad2Deg;
			rotation.eulerAngles = new Vector3(0,0,inclinazioneRay);
			ray.transform.rotation=rotation;
			
			
			
			i++;
		}
	}
	
	public bool isSoftwareInstalled()
	{
		return this.installSuccess;
	}
	
	public void resetInstalledSuccess()
	{
		this.installSuccess = false;
	}
	
	public void buyItem(string software)
	{
		if(lost)
		{
			DisplayWindow("ACTIONSDISABLED");
			return;
		}
		ISFSObject data = new SFSObject();
		data.PutUtfString("software", software);
		ExtensionRequest buyItem = new ExtensionRequest("buy", data, smartFox.LastJoinedRoom);
		smartFox.Send(buyItem);
	}
	
	public void DisplayWindow(string action)
	{
		GameObject.Find("messageContainer").transform.position = new Vector3(-200F, 64F, -80F);
		switch(action)
		{
			case "SUCCESS":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "Item bought with success";
			break;
			case "YOUWON":
				lost = true;
				StartCoroutine(endGame());
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "You win";
			break;
			case "INSTALLED":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "Software installed";
			break;
			case "NOPATH":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "No hacking path\navailable";
			break;
			case "HACKDISABLED":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "Your hack is disabled\nOR one of the selected nGateway\nis disabled for some seconds";
			break;
			case "NOTOWNER":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "You dont own\nthat nGateway";
			break;
			case "ITEMNOTOWNED":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "You dont own\nthat item";
			break;
			case "INVENTORYFULL":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "Your inventory is full";
			break;
			case "NOTENOUGHMONEY":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "You dont have\nenough money";
			break;
			case "NOTCUMULATIVE":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "You cant install\nthat item on that\nGateway since it's\nalready installed";
			break;
			case "LOST":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "You lost";
				lost = true;
				StartCoroutine(endGame());
			break;
			case "ACTIONSDISABLED":
				GameObject.Find("messageWindow").GetComponent<OTTextSprite>().text = "All the actions\nhave been disabled";
			break;
		}
	}
	
	private IEnumerator endGame()
	{
		yield return new WaitForSeconds(5f);
		ExtensionRequest endGameRequest = new ExtensionRequest("endGame", new SFSObject(), smartFox.LastJoinedRoom);
		smartFox.Send(endGameRequest);
			
	}
	
	private void DisplayStats(ISFSObject data)
	{
		GameObject.Find("referencePanel").GetComponent<referencePanel>().deactivateMailPanel();
		GameObject.Find("referencePanel").GetComponent<referencePanel>().deactivateShopPanel();
		string text = "";
		for(int i = 0; i < data.Size(); i++){
		ISFSArray p = data.GetSFSArray("player"+i);
		
			p.GetBool(3);//won
			string victory = p.GetBool(3)?"VICTORY":"DEFEAT";
			text += p.GetUtfString(0)+":"+victory+"\nConquered Gateway: "+p.GetInt(1)+"\nEarned Money:"+p.GetInt(2)+"\n\n";
		}
		
		GameObject.Find("blackPanel").GetComponent<OTFilledSprite>().depth = -200;
		GameObject.Find("endStats").GetComponent<OTTextSprite>().text = text;
	}
}
