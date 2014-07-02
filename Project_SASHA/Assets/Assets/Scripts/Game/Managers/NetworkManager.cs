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
    
    private Manager mg = null;
    
    public GameObject rayPrefab; 
    public GameObject policePrefab = null;
    public GameObject glowParticlePrefab = null;
    public GameObject GatewayPrefab = null;
    public GameObject spotsRoot = null;
    public GameObject UI = null;
    
    private GameObject ray;
    private float rayLenght;
    private float rayRotation;
    private float xt,xs,yt,ys;
    private Quaternion rotation;
    
    private float prevTime, statTime;
    private float policeUpdateTime = 9.0f;
    private float statUpdatetime = 1.0f;
    
    
    private int playerNumbers;
    
    private Dictionary<string, Color> playerColors = new Dictionary<string,Color>();
    private Color[] colors = { Color.red, Color.green, Color.cyan, Color.magenta, Color.yellow };
    private string currentPlayer;
    public string fromGw, toGw;
    private GameObject currentPolice;
    
    private System.Object messagesLocker = new System.Object();
    private SmartFox smartFox;
    
    public bool lost = false;
    public bool gameEnded = false;
    public bool displayWindowIsOpen = false;
    
    private ISFSObject shop;
    
    public void setStart(String start)
    {
        this.fromGw = start;
    }
    
    public void setEnd(String end)
    {
        this.toGw = end;
    }
    
    public String getStart()
    {
        return this.fromGw;
    }
    
    public String getEnd()
    {
        return this.toGw;
    }
    
    void Start() 
    {
        mg = GameObject.Find("Manager").GetComponent<Manager>();
        playerNumbers = PlayerPrefs.GetInt("playerNumber");
        smartFox = SmartFoxConnection.Connection;
        if (smartFox == null) {
            Application.LoadLevel("loginScreen");
            return;
        }
        
        prevTime = statTime = Time.time;
        
        currentPolice = Instantiate(policePrefab) as GameObject;
        currentPolice.transform.name = "Police";
        
        SubscribeDelegates();
        
        this.currentPlayer=smartFox.MySelf.Name;
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
            askPolicePosition();
        }
        
        if(Time.time - statTime > statUpdatetime)
        {
            statTime = Time.time;
            refreshWorldRequest();
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
        
        ExtensionRequest getInventorySetup = new ExtensionRequest("inventorySetup", new SFSObject(), smartFox.LastJoinedRoom);
        smartFox.Send(getInventorySetup);
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
            switch (cmd)            
            {
                case "policeInfo":
                    PoliceTextInfo(data);
                    break;
                case "hack":
                    Hack(data);
                    break;
                case "syncWorld": 
                    UpdateWorldSetup(data);
                    break;
                case "refreshGtw":
                    gatewayUpdate(data);
                    break;
                case "getObjectives":
                    UpdateObjective(data);
                    break;
                case "getWorldSetup":
                    InstantiateWorld(data);
                    break;
                case "policePosition":
                    UpdatePolicePosition(data);
                    break;
                case "playerInfo":
                    UpdatePlayerInfo(data);
                    break;
                case "install":
                    InstallItem(data);
                    break;
                case "inventorySetup":
                    SetupInventory(data);
                    break;
                case "shopInfo":
                    ShopSetup(data);
                    break;
                case "buy":
                    gameObject.GetComponent<InventoryManager>().UpdatePlayerInventory(data.GetSFSArray("inventory"));
                    mg.getNotificationManager().DisplayWindow("SUCCESSBUY");
                    break;
                case "news":
                    mg.getNotificationManager().DisplayNews(data.GetUtfString("EVENT"));
                    break;
                case "error":
                    mg.getNotificationManager().DisplayWindow(data.GetUtfString("error"));
                    break;
                case "lost":
                    mg.getNotificationManager().DisplayWindow("LOST");
                    break;
                case "endGameInfo":
                    DisplayEndingStats(data);
                    break;
                case "hackTimer":
                    showTimer(data);
                    break;
                case "refresh":
                    smartFox.Send(new ExtensionRequest("syncWorld", new SFSObject(), smartFox.LastJoinedRoom));
                    break;
                case "path":
                    tracePath(data);
                    break;
            }
            
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
            mg.getNotificationManager().DisplayWindow("YOUWON");
        Debug.Log("User " + user.Name + " left room " + room);
    }
    
    public void CloseApplication() 
    {
        UnsubscribeDelegates();
        smartFox.Disconnect();
        Application.Quit();
    }
    
    public void SendLeaveRoom() 
    {
        smartFox.Send(new JoinRoomRequest("Lobby", null, smartFox.LastJoinedRoom.Id));
        UnsubscribeDelegates();
        Application.LoadLevel("loginScreen");
    }
    
    public void SendHackRequest(String fromGateway, String toGateway, bool neutralize) 
    {
        if(lost)
        {
            mg.getNotificationManager().DisplayWindow("ACTIONSDISABLED");
            return;
        }
        ISFSObject data = new SFSObject();
        data.PutUtfString("gatewayFrom", fromGateway);
        data.PutUtfString("gatewayTo", toGateway);
        data.PutBool("neutralize", neutralize);
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
        return this.currentPlayer;
    }
    
    public void gatewayUpdate(ISFSObject data)
    {
        Gateway gw = GameObject.Find(data.GetUtfString("STATE").ToLower()).GetComponent<Gateway>();
        gw.Update(data);
        gameObject.GetComponent<TextManager>().printGwInfo(gw);
    }

    private void PoliceTextInfo(ISFSObject data)
    {
        string action = data.GetUtfString("ACTION");
        mg.getNotificationManager().DisplayNews(action);
    }
    
    private void InstantiateWorld(ISFSObject data)
    {
        String[] keys = data.GetKeys();
        
        foreach(String currentKey in keys)
        {
            ISFSObject currentObject = data.GetSFSObject(currentKey);
            GameObject currentGw = Instantiate(GatewayPrefab) as GameObject;
            currentGw.transform.name = currentObject.GetUtfString("STATE");
            Gateway gw = currentGw.GetComponent<Gateway>();
            
            gameObject.GetComponent<Manager>().stopParticle(gw);
            
            GameObject region=GameObject.Find(currentObject.GetUtfString("REGION"));
            gw.transform.parent=region.transform;
            gw.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<ResourcesManager>().getGwImage(currentObject.GetUtfString("TYPE"));
            gw.Setup(currentObject);
            gw.GetComponent<SpriteRenderer>().color = playerColors[gw.getOwner()];
            
            if(gw.getOwner() == smartFox.MySelf.Name)
            {
                gameObject.GetComponent<Manager>().startParticle(gw);
                StartCoroutine( gameObject.GetComponent<Manager>().stopParticle(gw, 6.0f));
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
            gw.Update(currentObject);
            gw.GetComponent<SpriteRenderer>().color = playerColors[gw.getOwner()];
        }
        ExtensionRequest objectiveRequest = new ExtensionRequest("getObjectives", new SFSObject(), smartFox.LastJoinedRoom);
        smartFox.Send(objectiveRequest);
    }
    
    private void UpdatePolicePosition(ISFSObject data)
    {
        
        float police_x = (float)data.GetFloat("police_x");
        float police_y = (float)data.GetFloat("police_y");
        currentPolice.transform.position = new Vector3(police_x * mg.getScale().x, (police_y * mg.getScale().y + 0.2F * mg.getScale().y), 2F);
    }
    
    private void SetupInventory(ISFSObject data)
    {
        gameObject.GetComponent<InventoryManager>().UpdatePlayerInventory(data.GetSFSArray("inventory"));
    }
    
    private void UpdatePlayerInfo(ISFSObject data)
    {
        gameObject.GetComponent<TextManager>().UpdatePlayerInfo(data, playerColors [smartFox.MySelf.Name]);
    }
    
    private void refreshWorldRequest()
    {
        smartFox.Send(new ExtensionRequest("syncWorld", new SFSObject(), smartFox.LastJoinedRoom));
        smartFox.Send( new ExtensionRequest("playerInfo", new SFSObject(), smartFox.LastJoinedRoom));
    }
    
    private void UpdateObjective(ISFSObject data)
    {
        foreach(string currentKey in data.GetKeys())
        {
            ISFSObject currentObject = data.GetSFSObject(currentKey);
            GameObject currentObj = GameObject.Find(currentKey);
            currentObj.GetComponent<UILabel>().text = currentObject.GetInt("SPOTCONQUERED").ToString();
        }
        askPolicePosition();
    }
    
    public void askPolicePosition()
    {
        ExtensionRequest policePositionRequest = new ExtensionRequest("policePosition", new SFSObject(), smartFox.LastJoinedRoom);
        smartFox.Send(policePositionRequest);
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
            mg.getNotificationManager().DisplayWindow("ACTIONSDISABLED");
            return;
        }
        
        ISFSArray path = data.GetSFSArray("hackingPath");
        int i = 0;
        
        while(i < path.Size()-1)
        {
            String startC = (String) path.GetElementAt(i);
            String targetC =(String) path.GetElementAt(i+1);
            
            String[] coord_1 = startC.Split(':');
            String[] coord_2 = targetC.Split(':');
            
            Vector3 p0 = new Vector3(float.Parse(coord_1[0]), float.Parse(coord_1[1]), 1F);
            Vector3 p1 = new Vector3(float.Parse(coord_2[0]), float.Parse(coord_2[1]), 1F);
            
            float inclinazioneRay = Mathf.Atan((p1.y-p0.y)/(p1.x-p0.x))*Mathf.Rad2Deg;
            rotation.eulerAngles = new Vector3(0,0,inclinazioneRay);
            
            float distance = Vector3.Distance(p1,p0);
            rayLenght = distance;
            Vector3 d0 = new Vector3(-5.8f, 2.2f, 1f);
            Vector3 d1 = new Vector3(5.3f, 2.5f, 1f);
            if(!((p0 == d0 && p1 == d1)|| (p1 == d0 && p0 == d1)))
            {
                ray = Instantiate(rayPrefab) as GameObject;
                ray.transform.parent = GameObject.Find("UI").transform;
                ray.transform.localPosition = new Vector3((p1.x+p0.x)/2,(p1.y+p0.y)/2, 5F);
                ray.transform.localScale = new Vector2(rayLenght, 0.7F);
                ray.transform.rotation=rotation;
            }
            else
            {
                ray = Instantiate(rayPrefab) as GameObject;
                ray.transform.parent = GameObject.Find("UI").transform;
                ray.transform.localPosition = new Vector3(6.03F, 2.5F, 5F);
                ray.transform.localScale = new Vector2(1.18F, 0.7F);
                ray.transform.rotation=rotation;
                
                ray = Instantiate(rayPrefab) as GameObject;
                ray.transform.parent = GameObject.Find("UI").transform;
                ray.transform.localPosition = new Vector3(-6.25F, 2.18F, 5F);
                ray.transform.localScale = new Vector2(0.8F, 0.7F);
                ray.transform.rotation=rotation;
            }
            
            i++;
        }
    }

    
	public void showTimer(ISFSObject data)
	{
		gameObject.GetComponent<HackSecondsManager>().setAction(data.GetUtfString("ACTION"));
		gameObject.GetComponent<HackSecondsManager>().setSeconds(data.GetInt("seconds"));
		
	}
    
    public void buyItem(string software)
    {
        if(lost)
        {
            mg.getNotificationManager().DisplayWindow("ACTIONSDISABLED");
            return;
        }
        ISFSObject data = new SFSObject();
        data.PutUtfString("software", software);
        ExtensionRequest buyItem = new ExtensionRequest("buy", data, smartFox.LastJoinedRoom);
        smartFox.Send(buyItem);
    }
    


    
    public IEnumerator endGame()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("sending endgame");
        ExtensionRequest endGameRequest = new ExtensionRequest("endGame", new SFSObject(), smartFox.LastJoinedRoom);
        smartFox.Send(endGameRequest);
        
    }
    
    private void DisplayEndingStats(ISFSObject data)
    {
        GameObject.Find("BlackPanel").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("EndGameCamera").GetComponent<Camera>().enabled = true;
        gameEnded = true;
        Debug.Log("recieved endgame");
        string text = "";
        for(int i = 0; i < data.Size(); i++){
            ISFSArray p = data.GetSFSArray("player"+i);
            
            p.GetBool(3);//won
            string victory = p.GetBool(3)?"VICTORY":"DEFEAT";
            text += p.GetUtfString(0)+": "+victory+"\nConquered Gateway: "+p.GetInt(1)+"\nEarned Money: "+p.GetInt(2)+"\n\n";
        }

        GameObject.Find("endStats").GetComponent<UILabel>().text = text;
    }
    
    public void leaveGame()
    {
        smartFox.Send(new LeaveRoomRequest());
        Application.LoadLevel("loginScreen");
    }
    
    public Color getColor(string plr)
    {
        return this.playerColors[plr];
    }
    
    private void InstallItem(ISFSObject data)
    {
        if(data.GetBool("success"))
            mg.getNotificationManager().DisplayWindow("INSTALLED");

        gameObject.GetComponent<InventoryManager>().UpdatePlayerInventory(data.GetSFSArray("inventory"));
    
        ISFSObject data2 = new SFSObject();
        Gateway gtw = gameObject.GetComponent<TextManager>().gtw;
        data2.PutUtfString("selctedGateway", gtw.getState());
        smartFox.Send(new  ExtensionRequest("refreshGtw", data2, smartFox.LastJoinedRoom));

    }
    
    private void Hack(ISFSObject data)
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("ray"))
            Destroy(g);
        if (data.GetBool ("success")) 
        {
            ISFSObject data2 = new SFSObject();
            Gateway gtw = gameObject.GetComponent<TextManager>().gtw;
            data2.PutUtfString("selctedGateway", gtw.getState());
            smartFox.Send(new  ExtensionRequest("refreshGtw", data2, smartFox.LastJoinedRoom));
        }
        if(data.GetBool("victoryReached") == true)
            mg.getNotificationManager().DisplayWindow("YOUWON");
    }


    public void installSoftware(string name, string gateway)
    {
        if(lost)
        {
            mg.getNotificationManager().DisplayWindow("ACTIONSDISABLED");
            return;
        }
        ISFSObject data = new SFSObject();
        data.PutUtfString("software", name);
        data.PutUtfString("gateway", gateway);  
        smartFox.Send(new ExtensionRequest("install", data, smartFox.LastJoinedRoom));

    }
}