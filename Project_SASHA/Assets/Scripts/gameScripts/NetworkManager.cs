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
	GameObject spots;
	private int playerNumbers;
	//private int playerNumbers=1;
	private Dictionary<string, Color> playerColors = new Dictionary<string,Color>();
	private Color[] colors = { Color.red, Color.green, Color.cyan, Color.magenta, Color.yellow };
	private string currentPlayer;
	
	
	private static NetworkManager instance;	
	public GameObject GatewayPrefab = null;
	
	private System.Object messagesLocker = new System.Object();
	private SmartFox smartFox;

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
		
		SubscribeDelegates();
		
		//DICHIARAZIONE CARTELLA SPOT
		spots = new GameObject();
		spots.transform.name = "spots";
		
		currentPlayer=smartFox.MySelf.Name;
		
	}
	
	void Update()
	{
	
		if(smartFox.LastJoinedRoom.UserCount == playerNumbers && !running)
		//if(smartFox.LastJoinedRoom.UserCount == 1 && !running)
		{
			SendWorldSetupRequest();
			ColorSetup();
			running = true;
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

    public void SendInfoRequest(String gatewayName) 
	{
        Room room = smartFox.LastJoinedRoom;
        ISFSObject data = new SFSObject();
        data.PutUtfString("selctedGateway", gatewayName);
        ExtensionRequest request = new ExtensionRequest("gatewayInfo", data, room);
        smartFox.Send(request);
    }
	
	public void SendWorldSetupRequest()
	{
		Room room = smartFox.LastJoinedRoom;
		ExtensionRequest worldSetupRequest = new ExtensionRequest("getWorldSetup", new SFSObject(), room);
		smartFox.Send(worldSetupRequest);

		ExtensionRequest objectiveRequest = new ExtensionRequest("getObjectives", new SFSObject(), room);
		smartFox.Send(objectiveRequest);
	}
	
	/*public void TimeSyncRequest()
	{
		Room room = smartFox.LastJoinedRoom;
		ExtensionRequest request = new ExtensionRequest("getTime", new SFSObject(), room);
		smartFox.Send(request);
	}*/
	
	private void OnConnectionLost(BaseEvent evt) 
	{
		print ("disconnected for this reason: "+ evt.Params["reason"]);
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
				smartFox.Send(new ExtensionRequest("sync", new SFSObject(), smartFox.LastJoinedRoom));
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
				String[] keys = data.GetKeys();
				foreach(String currentKey in keys)
				{
					ISFSObject currentObject = data.GetSFSObject(currentKey);
					String objName = currentKey;
					String type = currentObject.GetUtfString("TYPE");
					int conqueredGateway = currentObject.GetInt ("SPOTCONQUERED");
					int requiredGateway = currentObject.GetInt ("SPOTREQUIRED");
				}
			}
			else if (cmd == "getWorldSetup")
			{
				InstantiateWorld(data);
			}
		}
		catch (Exception e) {
			Debug.Log("Exception handling response: "+e.Message+" >>> "+e.StackTrace);
		}
	}
	
	/*void OnGUI()
	{
		 if (GUI.Button(new Rect(0,30,100,100), "hack"))
            {
				Room room = smartFox.LastJoinedRoom;
				ISFSObject data = new SFSObject();
					data.PutUtfString("gatewayFrom", "north west territory");
					data.PutUtfString("gatewayTo", "greenland");
				data.PutBool("neutralize", false);
				smartFox.Send(new ExtensionRequest("hack", data, room));
            }
	}*/
	
	private void OnUserLeaveRoom(BaseEvent evt) 
	{
		User user = (User)evt.Params["user"];
		Room room = (Room)evt.Params["room"];
        Debug.Log("User " + user.Name + " left room " + room);
	}
	
	void OnApplicationQuit() 
	{
		UnsubscribeDelegates();
		smartFox.Disconnect();
	}

	
	public void SendLeaveRoom() 
	{
		UnsubscribeDelegates();
		smartFox.Send(new JoinRoomRequest("Lobby", null, smartFox.LastJoinedRoom.Id));
		Application.LoadLevel("loginScreen");
	}
	
	public void SendHackRequest(String fromGateway, String toGateway) 
	{
		ISFSObject data = new SFSObject();
		data.PutUtfString("gatewayFrom", fromGateway);
		data.PutUtfString("gatewayTo", toGateway);
		data.PutBool("neutralize", false);
		
		smartFox.Send(new ExtensionRequest("hack", data, smartFox.LastJoinedRoom));
	}
	public void SendNeutralizeRequest(String fromGateway, String toGateway) {
		
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
					//Vector3 temp = new Vector3((float)currentObject.GetInt("X")+21,(float)currentObject.GetInt("Y")-26,1);
					Vector3 temp = new Vector3((float)currentObject.GetInt("X"),(float)currentObject.GetInt("Y"),1);
					
					currentGw.transform.position = temp;
					Gateway gw = currentGw.GetComponent<Gateway>();
					
					gw.Setup(currentObject);
					
					gw.GetComponent<OTSprite>().tintColor = playerColors[gw.getOwner()];
						
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
	}
}
