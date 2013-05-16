using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Cryptography;
using System.Text;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Managers;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using Sfs2X.Logging;
using Sfs2X.Entities.Data;
using Sfs2X.Util;


public class LoginLobbys : MonoBehaviour {
	private const string LASTUSERNAME = "LAST_USER_NAME_USED";
	private const string LASTPASSWORD = "LAST_PASSWORD_USED";
	private const string MOTD = "Welcome";
	
	private SmartFox smartFox;
	private string zone = "World1";
	private string serverName = "127.0.0.1";
	private int serverPort = 9933;
	private string username = "";
    private string password = "";
	
	private bool isLoggedIn;
	private bool creatingGame;
	private bool showGameLobby;
	private bool loginError = false;
	private string loginErrorMessage = "";
	private bool gameCreationError = false;
	private string createGameErrorMessage = "";
	private string errorMessage = "";
	private bool showLoadingScreen = false;
	private bool myOnline;
	
	private string gameName = "";
	private bool gamePrivate;
	private string gamePassword = "";
	
	private string newMessage = "";
	private ArrayList chatMessages = new ArrayList();
	private System.Object messagesLocker = new System.Object();
	public GUISkin gSkin;
	
	private Room currentActiveRoom;
				
	private Vector2 gameScrollPosition, userScrollPosition, chatScrollPosition, buddiesScrollPosition;
	private int roomSelection = -1;
	private int userSelection = -1;
	private string[] roomNameStrings;
	private string[] roomFullStrings;
	
	private int screenWidth;
	private int screenHeight;
	private int windowWidth;
	private int windowHeight;
	
	private const string NOGAMENAME = "Insert Game Name!";
	private const string SHORTORLONGNAME = "Game Name length should be between 3 and 10";
	
	public Texture2D icon_available, icon_away,	icon_blocked, icon_occupied, icon_offline;
	
	private ArrayList messages = new ArrayList();
	private List<Buddy> buddies = new List<Buddy>();
	GUIContent[] buddyContents;
	private bool isBuddyListInited;
	
	// Use this for initialization
	void Start () 
	{
	bool debug = true;
		if (SmartFoxConnection.IsInitialized)
		{
			smartFox = SmartFoxConnection.Connection;
			AddEventListeners();
			//SetupGeneralLobby();
		}
		else
		{
			isLoggedIn = false;
			smartFox = new SmartFox(debug);
		}

		smartFox.AddLogListener(LogLevel.INFO, OnDebugMessage);
		
		isBuddyListInited = false; 
		
		SetErrorMessages();
		
		username = PlayerPrefs.GetString(LASTUSERNAME, "");
		password = PlayerPrefs.GetString(LASTPASSWORD, "");
		
	}
	
	void FixedUpdate() 
	{
		smartFox.ProcessEvents();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		if (smartFox == null) return;
		ReplaceGUIStyle();	
		
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		GUI.Label(new Rect(0, 0, screenWidth, screenHeight), "", "background");
		GUI.Label(new Rect((int)(screenWidth / 3.8), 0, screenWidth / 2, screenHeight / 6), "", "loginTitle");

		windowWidth=(int)(screenWidth/2.1);
		windowHeight=(int)(screenHeight/2);

        if (loginError)
        {
            GUI.Box(new Rect(screenWidth / 2 - (screenWidth / 4), screenHeight / 2 - (int)(screenHeight / 6), screenWidth / 2f, screenHeight / 3), "", "interface_bg_right");
			if(errorMessage.Contains("persistente"))
			{
				errorMessage = "Server offline";
			}

            GUI.Label(new Rect(screenWidth / 2 - (screenWidth / 5), screenHeight / 2 - (int)(screenHeight / 10f), screenWidth / 2, screenHeight / 3), errorMessage);
            if (GUI.Button(new Rect(screenWidth / 2 + (screenWidth / 4.9f), screenHeight / 2 - (int)(screenHeight / 6), (int)(screenWidth / 24), (int)(screenWidth / 24)), "  X", "close"))
            {
                loginError = false;
                errorMessage = "";
            }

        }
        else
		{
            // Login
		    if (!isLoggedIn) 
			{
				displayWindow(10, new Rect((int)(screenWidth/3.6), (int)(screenHeight/4),windowWidth, windowHeight)); // LoginWindow
		    }
			else if (currentActiveRoom!=null) 
			{
		        if (!showLoadingScreen) {
					if(!creatingGame && !showGameLobby)
					{
						displayWindow(11); //GeneralLobby
					}
					else if(creatingGame)
					{
						displayWindow(12, new Rect((int)(screenWidth/3.6), (int)(screenHeight/4),windowWidth, windowHeight)); //Game Creation
					}
					else
					{
						displayWindow(13); //Game Lobby
					}
                } 
				else 
				{
                    GUI.Box(new Rect(screenWidth/ 2 - screenWidth/ 6, (int)(screenHeight/ 2) - screenHeight/ 6, (int)(screenWidth/ 3), (int)(screenHeight/ 3)), "", "box");
                    GUI.Label(new Rect(screenWidth/ 2 - screenWidth/ 6, (int)(screenHeight/ 2) - screenHeight/ 6, (int)(screenWidth/ 3), (int)(screenHeight/ 3)), "Please wait! Loading...", "loading");
                }
            }
		}
	}
	
	private void AddEventListeners() 
	{
		smartFox.RemoveAllEventListeners();
		smartFox.AddEventListener(SFSEvent.CONNECTION, OnConnection);
		smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener(SFSEvent.DEBUG_MESSAGE, OnDebugMessage);
		smartFox.AddEventListener(SFSEvent.LOGIN, OnLogin);
		smartFox.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
		smartFox.AddEventListener(SFSEvent.LOGOUT, OnLogout);
		smartFox.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
		smartFox.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
		smartFox.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserLeaveRoom);
		smartFox.AddEventListener(SFSEvent.ROOM_ADD, OnRoomAdded);
		smartFox.AddEventListener(SFSEvent.ROOM_CREATION_ERROR, OnCreateRoomError);
		smartFox.AddEventListener(SFSEvent.USER_COUNT_CHANGE, OnUserCountChange);
		smartFox.AddEventListener(SFSEvent.ROOM_REMOVE, OnRoomDeleted);
		smartFox.AddEventListener(SFSEvent.PUBLIC_MESSAGE, OnPublicMessage);
		smartFox.AddEventListener(SFSEvent.PRIVATE_MESSAGE, OnPrivateMessage);
		smartFox.AddEventListener(SFSEvent.UDP_INIT, OnUdpInit);
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
		//smartFox.AddLogListener(LogLevel.DEBUG, OnDebugMessage);	
		
		// Callbacks for buddy events
		smartFox.AddEventListener(SFSBuddyEvent.BUDDY_LIST_INIT, OnBuddyListInit);
		smartFox.AddEventListener(SFSBuddyEvent.BUDDY_ERROR, OnBuddyError);
		smartFox.AddEventListener(SFSBuddyEvent.BUDDY_ONLINE_STATE_UPDATE, OnBuddyListUpdate);
		smartFox.AddEventListener(SFSBuddyEvent.BUDDY_VARIABLES_UPDATE, OnBuddyListUpdate);
		smartFox.AddEventListener(SFSBuddyEvent.BUDDY_ADD, OnBuddyAdded);
		smartFox.AddEventListener(SFSBuddyEvent.BUDDY_REMOVE, OnBuddyRemoved);
		smartFox.AddEventListener(SFSBuddyEvent.BUDDY_BLOCK, OnBuddyBlocked);
		smartFox.AddEventListener(SFSBuddyEvent.BUDDY_MESSAGE, OnBuddyMessage);
		//smartFox.AddEventListener(SFSBuddyEvent.BUDDY_VARIABLES_UPDATE, OnBuddyVarsUpdate);
		
	}
	
	private void UnregisterSFSSceneCallbacks() 
	{
		// This should be called when switching scenes, so callbacks from the backend do not trigger code in this scene
		smartFox.RemoveAllEventListeners();
	}
	
	/**********************/
	/* Listeners Handlers */
	/**********************/
	
	/*
	 * Handler Server connection
	 */
	public void OnConnection(BaseEvent evt) 
	{
        bool success = (bool)evt.Params["success"];
        string error = (string)evt.Params["errorMessage"];
        errorMessage = (string)evt.Params["errorMessage"];
        if (errorMessage != "")
            loginError = true;

        Debug.Log("On Connection callback got: " + success + " (error : <" + error + ">)");

        if (success)
        {
            SmartFoxConnection.Connection = smartFox;
            loginError = false;
            Debug.Log("Sending login request");
            string hash = CalculateMD5(password).ToLower();
            smartFox.Send(new LoginRequest(username, hash, zone));
        }
    }
	
	/*
	 * Handle disconnection
	 */
	public void OnConnectionLost(BaseEvent evt) {
		Debug.Log("OnConnectionLost");
		isLoggedIn = false;
		currentActiveRoom = null;
		UnregisterSFSSceneCallbacks();
	}
	
	public void OnDebugMessage(BaseEvent evt) 
	{
		string message = (string)evt.Params["message"];
		Debug.Log("[SFS DEBUG] " + message);
	}
	
	/*
	 * Handle login event
	 */
	public void OnLogin(BaseEvent evt) {
		try {
			if (evt.Params.ContainsKey("success") && !(bool)evt.Params["success"]) {
				loginErrorMessage = (string)evt.Params["errorMessage"];
				Debug.Log("Login error: "+loginErrorMessage);
			}
			else 
			{
				Debug.Log("Logged in successfully");
				PlayerPrefs.SetString(LASTUSERNAME, username);
				PlayerPrefs.SetString(LASTPASSWORD, password);
				
				
				isLoggedIn = true;
				
				//Startup UDP
				smartFox.InitUDP(serverName, serverPort);
				
				SetupGeneralLobby();
				
				smartFox.Send(new InitBuddyListRequest());
				
				//Message of the day setup
				chatMessages.Add(MOTD);

			}
		}
		catch (Exception ex) 
		{
			Debug.Log("Exception handling login request: "+ex.Message+" "+ex.StackTrace);
		}
	}
	
	/*
	 * Handle an error during login
	 */
	public void OnLoginError(BaseEvent evt) {
       Debug.Log("Login error: " + (string)evt.Params["errorMessage"]);
       errorMessage = (string)evt.Params["errorMessage"];
       loginError = true;
       if (smartFox.IsConnected)
           smartFox.Disconnect();
   }
	
	/* 
	 * Handle logout event
	 */
	public void OnLogout(BaseEvent evt) {
		Debug.Log("OnLogout");
		isLoggedIn = false;	
		creatingGame = false;
		showGameLobby = false;
		isBuddyListInited = false;
		currentActiveRoom = null;
		smartFox.LastJoinedRoom = null;
		smartFox.BuddyManager.Inited = false;
		ResetChatHistory();
		smartFox.RemoveAllEventListeners();
		smartFox.Disconnect();

	}
	
	/*
	 * Handler a join room event
	 */
	public void OnJoinRoom(BaseEvent evt) 
	{
		ResetChatHistory();
		Room room = (Room)evt.Params["room"];
		currentActiveRoom = room;
		SetupGameList();
		// If we joined a game room, then we either created it (and auto joined) or manually selected a game to join
		if (room.IsGame) {
			showLoadingScreen = true;
			Debug.Log ("Joined game room " + room.Name);
			UnregisterSFSSceneCallbacks();
			Application.LoadLevel("GameLevel");
		}
	}
	
	/*
	 * Handle a new user that just entered the current room
	 */
	public void OnUserEnterRoom(BaseEvent evt) 
	{
		User user = (User)evt.Params["user"];
		lock (messagesLocker) {
			chatMessages.Add(user.Name + " joined room");
		}
	}

	/*
	 * Handle a user who left the room
	 */
	private void OnUserLeaveRoom(BaseEvent evt) 
	{
		User user = (User)evt.Params["user"];
		if(user.Name != smartFox.MySelf.Name)
			lock (messagesLocker)
				chatMessages.Add(user.Name + " left room");
	}
	
	/*
	 * Handle a new room in the room list
	 */
	public void OnRoomAdded(BaseEvent evt) 
	{
		Room room = (Room)evt.Params["room"];
		Debug.Log("Room added with name: "+room.Name);
		SetupGameList();
	}
	
	/*
	 * Handle an error while creating a room
	 */
	public void OnCreateRoomError(BaseEvent evt) 
	{
		string error = (string)evt.Params["errorMessage"];
		Debug.Log("Room creation error; the following error occurred: " + error);
	}
	
	/*
	 * Handle a count change in one room of the zone
	 */
	public void OnUserCountChange(BaseEvent evt) 
	{
		Debug.Log("User count change");
		OnBuddyListUpdate(evt);
		SetupGameList();
	}

	/*
	 * Handle a room that was removed
	 */
	public void OnRoomDeleted(BaseEvent evt) 
	{
		Debug.Log("Room deleted");
		SetupGameList();
	}
	
	/*
	 * Handle a public message
	 */
	private void OnPublicMessage(BaseEvent evt) 
	{
		User sender = (User)evt.Params["sender"];
		try {
			string message = (string)evt.Params["message"];
			Debug.Log ("Public message in:"+evt.Params["room"]);
			// Debug.Log(evt.Params);
			// We use lock here to ensure cross-thread safety on the messages collection 
			Debug.Log(evt.Params["room"]);
			lock (messagesLocker)
					chatMessages.Add("> " + sender.Name + ": " + message);
				
			
			chatScrollPosition.y = Mathf.Infinity;
			Debug.Log("> " + sender.Name + ": " + message);
			
		}
		catch (Exception ex) {
			Debug.Log("Exception handling public message: "+ex.Message+ex.StackTrace);
		}
	}
	
	/*
	 * Handle a private message 
	 */
	private void OnPrivateMessage(BaseEvent evt) 
	{
		User sender = (User)evt.Params["sender"];
		ISFSObject data = (SFSObject)evt.Params["data"];
		
		try {
			string message = (string)evt.Params["message"];
			{
				if(smartFox.MySelf == sender && message!="addToBuddy")
				{
					lock (messagesLocker)
						chatMessages.Add("> To " + data.GetUtfString("recipientName") + ": " + message);
				}
				else
				{
					if(message == "addToBuddy")
					{
						lock (messagesLocker)
							chatMessages.Add("[Server]: " + sender.Name + " added you to buddy list");
					}
					else
					{
						lock (messagesLocker)
							chatMessages.Add("> From " + sender.Name + ": " + message);
					}
				}
			}
				
			chatScrollPosition.y = Mathf.Infinity;
			if(message!="addToBuddy")
				Debug.Log("> PM From " + sender.Name + " to " + data.GetUtfString("recipientName") + ": " + message);
			
		}
		catch (Exception ex) 
		{
			Debug.Log("Exception handling private message: "+ex.Message+ex.StackTrace);
		}
	}
	
	/*
	 * Handle UDP Initialization
	 */
	public void OnUdpInit(BaseEvent evt) 
	{
		if (evt.Params.ContainsKey("success") && !(bool)evt.Params["success"]) {
			loginErrorMessage = (string)evt.Params["errorMessage"];
			Debug.Log("UDP error: "+loginErrorMessage);
		} else {
			Debug.Log("UDP ok");
			SetupGeneralLobby();
		}
	}
	
	/*
	* Handle response from server side
	*/
	public void OnExtensionResponse(BaseEvent evt)
	{
		string cmd = (string)evt.Params["cmd"];
    	SFSObject dataObject = (SFSObject)evt.Params["params"];
		
		switch (cmd) {
			case "createGameLobby":
				bool success = dataObject.GetBool("success");
				Debug.Log("Response for: "+cmd+" - returned:"+ success);
				if(success)
					smartFox.Send (new JoinRoomRequest(dataObject.GetUtfString("roomName")));
			break;
			
			case "startGame":
				Debug.Log ("Start Game Request");
			break;
		}
		
			
	}
	
	/*
	 * Runs when you have added a buddy succesfully	
	 */
	private void OnBuddyAdded(BaseEvent evt) 
	{

		Buddy buddy = (Buddy)evt.Params["buddy"];
				Debug.Log("Buddy request to:"+buddy.Name);
			
		lock(messagesLocker) 
		{
	    	messages.Add( buddy + " added" );
		}
		//sends a message to that buddy
		smartFox.Send(new PrivateMessageRequest("addToBuddy", buddy.Id));
		OnBuddyListUpdate(evt);
	}
	
	/*
	 * Runs when you have removed a buddy successfully
	 */
	private void OnBuddyRemoved(BaseEvent evt) 
	{
		lock(messagesLocker) 
		{
	    	messages.Add( (Buddy)evt.Params["buddy"] + " removed" );
		}
		OnBuddyListUpdate(evt);
	}  
	
	/*
	 * Runs when you have blocked a buddy successfully
	 */
	private void OnBuddyBlocked(BaseEvent evt) 
	{
		Buddy buddy = (Buddy)evt.Params["buddy"];
		string message = (buddy.IsBlocked ? " blocked" : "unblocked");
		
		lock(messagesLocker) 
		{
		    messages.Add( (Buddy)evt.Params["buddy"] + " " + message );
		}
		smartFox.Send(new PrivateMessageRequest(message + " you", buddy.Id));
		
		OnBuddyListUpdate(evt);
	}
	
	/*
	 * Anytime there is an error, it is logged in the debug console
	 */
	private void OnBuddyError(BaseEvent evt)
	{	
		Debug.Log("The following error occurred in the buddy list system: " + (string)evt.Params["errorMessage"]);
	}
		
	/*
	 * Keep the buddy list up to date
	 */
	private void OnBuddyListUpdate(BaseEvent evt)
	{
		//while updating, set this to false to keep it from displaying
		//isBuddyListInited = false;
		buddies.Clear();
		buddies = smartFox.BuddyManager.BuddyList;

		buddyContents = new GUIContent[buddies.Count];
	
		for(int i = 0; i < buddies.Count; i++)
		{
			Buddy buddy = buddies[i];
			//Stores the content for a buddy
			GUIContent buddyContent = new GUIContent();
			
			//Determines which icon best represents this buddy's state			
			if(!buddy.IsOnline) 
			{
				buddyContent.image = icon_offline;
			}			
			else if(buddy.IsBlocked) 
			{
				buddyContent.image = icon_blocked;
			}			
			else switch(buddy.State) 
			{
				case "Available":
					buddyContent.image = icon_available;					
				break;
				
				case "Away":
					buddyContent.image = icon_away;
				break;
				
				case "Occupied":
					buddyContent.image = icon_occupied;
				break;
			}
			//Show the nickname if it's not blank
			//Otherwise, show the buddy's user name
			buddyContent.text = (buddy.NickName != null && buddy.NickName != "" ? buddy.NickName : buddy.Name);
			buddyContent.text = buddyContent.text+": "+(buddy.IsOnline?"online":"offline");
			
			//Append this buddy's content to the end of the list
			buddyContents.SetValue(buddyContent, i);
		}
		//Now it's complete, so set this to true to display it
		isBuddyListInited = true;
	}
	
	/*
	 * Initializes all buddies and your buddy variables
	 */
	private void OnBuddyListInit(BaseEvent evt)
	{
		// Populate user's list of buddies
		OnBuddyListUpdate(evt);		
		
		//Your details can now be displayed
		myOnline = true;
	}
	
	/*
	 * Check if the sender is you or
	 * someone else, then it labels the
	 * message as to or from.
	 */
	private void OnBuddyMessage(BaseEvent evt)
	{
		Buddy buddy;
		Buddy sender;
		Boolean isItMe = (bool)evt.Params["isItMe"];
		string message = (string) evt.Params["message"];
		string buddyName;
		
		sender = (Buddy)evt.Params["buddy"];
		if (isItMe)
		{
			ISFSObject playerData = (SFSObject)evt.Params["data"];
			buddyName = playerData.GetUtfString("recipient");
			buddy = smartFox.BuddyManager.GetBuddyByName(buddyName);
		}
		else 
		{			
			buddy = sender;
			buddyName = "";
		}
		if (buddy != null) 
		{
			//Store the message according to the sender's name
			message = (isItMe ? "To " + buddyName : "From " + buddy.Name) + ": " + message;
			lock(messagesLocker) 
			{
	    		messages.Add( message );
			}
			//This forces the chat to show the most recent message
			chatScrollPosition.y = Mathf.Infinity;
		}
	}
	
	/*
	 * Runs when a buddy variable is updated
	 */
	private void OnBuddyVarsUpdate(BaseEvent evt) 
	{
		Debug.Log (("Buddy variables update from: " + (Buddy)evt.Params["buddy"]));
	}
	
	public void UpdateBuddyVars(SFSBuddyVariable buddyVar) 
	{
		List<BuddyVariable> myVars = new List<BuddyVariable>();
    	myVars.Add(buddyVar);
    	smartFox.Send(new SetBuddyVariablesRequest(myVars));
	}
	
	/**********************/
	/*  Auxiliar Methods  */
	/**********************/
	private string CalculateMD5(string input) {
        // step 1, calculate MD5 hash from input
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }
	
	private void ReplaceGUIStyle()
	{
		GUI.skin = gSkin;
	}
	
	private void SetupGeneralLobby() {
		SetupGameList();
		
	}
	
	private void SetupGameList () {
        
		List<string> rooms = new List<string> ();
		List<string> roomsFull = new List<string> ();
		
		List<Room> allRooms = smartFox.RoomManager.GetRoomList();
		
		foreach (Room room in allRooms) {
			// Only show game rooms
			if (room.Name == "Lobby") {
				continue;
			}
			
			Debug.Log ("Room id: " + room.Id + " has name: " + room.Name);
			
			rooms.Add(room.Name);
			roomsFull.Add(room.Name + " (" + room.UserCount + "/" + room.MaxUsers + ")");
		}
		
		roomNameStrings = rooms.ToArray();
		roomFullStrings = roomsFull.ToArray();
		
		if (smartFox.LastJoinedRoom == null) {
			smartFox.Send(new JoinRoomRequest("Lobby"));
		} else {
			currentActiveRoom = smartFox.LastJoinedRoom;
		}
	}
	
	private void displayWindow(int windowID){
		GUI.WindowFunction WinFunction = null;
		Rect rectPos = new Rect(0, 0, screenWidth, screenHeight);
		switch(windowID){
			case 10:
				WinFunction = LoginWindow;
			break;
			case 11:
				WinFunction = GeneralLobbyWindow;
			break;
			case 12:
				WinFunction = GameCreationWindow;
			break;
			case 13:
				WinFunction = GameLobbyWindow;
			break;
		}
		
		GUI.Window(windowID,rectPos,WinFunction,"");
	}
	
	private void displayWindow(int windowID, Rect rectPos){
		GUI.WindowFunction WinFunction = null;
		switch(windowID){
			case 10:
				WinFunction = LoginWindow;
			break;
			case 11:
				WinFunction = GeneralLobbyWindow;
			break;
			case 12:
				WinFunction = GameCreationWindow;
			break;
			case 13:
				WinFunction = GameLobbyWindow;
			break;
		}
		
		GUI.Window(windowID,rectPos,WinFunction,"");
	}
	
	private void LoginWindow(int windowID)
	{
		int textXPosition = (int) (windowWidth/8);
		int fieldXPosition = (int) (windowWidth/2.7);
		int fieldYBasePosition = (int) (windowHeight/10);
		int fieldWidth = (int) (windowWidth/2.5);
		int fieldHeight = 20;
		int labelHeight = 100;
		
		GUI.Box(new Rect(textXPosition-20, fieldYBasePosition-20 , fieldXPosition+fieldWidth-textXPosition+40, labelHeight*2), "","smallBox");
		
		GUI.Label(new Rect(textXPosition, fieldYBasePosition, 100, labelHeight), "Username: ");
		username = GUI.TextField(new Rect(fieldXPosition, fieldYBasePosition, fieldWidth, fieldHeight), username, 25);
			
		GUI.Label(new Rect(textXPosition, fieldYBasePosition*2, 100, labelHeight), "Password: ");
		password = GUI.PasswordField(new Rect(fieldXPosition, fieldYBasePosition*2,fieldWidth, fieldHeight), password,'*', 10);
		
		GUI.Label(new Rect(textXPosition, fieldYBasePosition*3, 100, labelHeight), "Server: ");
		serverName = GUI.TextField(new Rect(fieldXPosition, fieldYBasePosition*3, fieldWidth, fieldHeight), serverName, 25);
			
		GUI.Label(new Rect(textXPosition, fieldYBasePosition*4, 100, labelHeight), "Port: ");
		serverPort = int.Parse(GUI.TextField(new Rect(fieldXPosition, fieldYBasePosition*4, fieldWidth, fieldHeight), serverPort.ToString(), 4));

		GUI.Label(new Rect(10, 240, 100, 100), loginErrorMessage);

		if (GUI.Button(new Rect(textXPosition, fieldYBasePosition*5, fieldWidth/2, windowHeight / 12f), "Login")  || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
		{
			AddEventListeners();
			smartFox.Connect(serverName, serverPort);
		}
		if (GUI.Button(new Rect(fieldXPosition + fieldWidth/2, fieldYBasePosition*5, fieldWidth/2, windowHeight / 12f), "Quit"))
			CloseApplication();
			
		
	}

	private void GeneralLobbyWindow(int windowID)
	{
		
		int chatBoxLeft = 10;
		int chatBoxTop = (int)(screenHeight/ 5);
		int chatBoxWidth = (int)(screenWidth/ 2);
		int chatBoxHeight = (int)(screenHeight/ 1.5);
		
		int userListLeft = chatBoxLeft+chatBoxWidth+5;
		int userListTop = chatBoxTop;
		int userListHeight = chatBoxHeight;
		int userListWidth = chatBoxWidth/2;
		
		int gameListLeft = userListLeft+userListWidth+5;
		int gameListTop = chatBoxTop;
		int gameListHeight = chatBoxHeight/2-5;
		int gameListWidth = chatBoxWidth/2-30;
		
		int buddyListLeft = userListLeft+userListWidth+5;
		int buddyListTop = chatBoxTop+chatBoxHeight/2;
		int buddyListHeight = chatBoxHeight/2;
		int buddyListWidth = chatBoxWidth/2-30;

		//**************//
		// Chat history //
		//**************//
		// ChatBox
		GUI.Box(new Rect(chatBoxLeft, chatBoxTop, chatBoxWidth, chatBoxHeight), "Chat","box");
	
        GUILayout.BeginArea(new Rect(chatBoxLeft+5, chatBoxTop, chatBoxWidth - 20, chatBoxHeight));
		GUILayout.Space(screenHeight / 24);
		
		//Side Scroller
		GUILayoutOption[] param = { GUILayout.Width(chatBoxWidth - 20), GUILayout.Height(chatBoxHeight - 25) };
		chatScrollPosition = GUILayout.BeginScrollView(chatScrollPosition, false, true, param);
		
		// Populate chat with messages
		GUILayout.BeginVertical();
		// We use lock here to ensure cross-thread safety on the messages collection 
		lock (messagesLocker)
			foreach (string message in chatMessages)
				GUILayout.Label(message);
		
		GUILayout.EndScrollView();
		GUILayout.EndVertical();
		GUILayout.EndArea();

		// Send message
        newMessage = GUI.TextField(new Rect(chatBoxLeft+1, chatBoxTop+chatBoxHeight+5, chatBoxWidth-110, 20), newMessage, 50);
        if (GUI.Button(new Rect(chatBoxLeft + chatBoxWidth - 105, chatBoxTop+chatBoxHeight+4, 105, 22), "Send") || (Event.current.type == EventType.keyDown && Event.current.character == '\n')){
			processChatMessage(newMessage);
			newMessage = "";
		}
		
		GUI.Label (new Rect(chatBoxLeft+1,chatBoxTop+chatBoxHeight+25, chatBoxWidth-110,20),"Private Message: /w username text");
		GUI.Label (new Rect(chatBoxLeft+1,chatBoxTop+chatBoxHeight+40, chatBoxWidth-110,25),"Add to buddy list: /add username");
		GUI.Label (new Rect(chatBoxLeft+1,chatBoxTop+chatBoxHeight+55, chatBoxWidth-110,25),"Remove from buddy list: /remove username");
		
		//**************//
		//   User list  //
		//**************//
        GUI.Box(new Rect(userListLeft, userListTop, userListWidth, userListHeight), "Users", "box");
        GUILayout.BeginArea(new Rect(userListLeft+5, userListTop, userListWidth-10,userListHeight-25));
		
		GUILayout.Space(screenHeight / 24);
		
		//Side Scroller
		GUILayoutOption[] paramUserList = { GUILayout.Width(chatBoxWidth - 20), GUILayout.Height(chatBoxHeight - 25) };
		userScrollPosition = GUILayout.BeginScrollView(userScrollPosition, false, true, paramUserList);
		GUILayout.BeginVertical ();
			
		List<User> userList = currentActiveRoom.UserList;

		foreach (User user in userList)
			GUILayout.Label(user.Name);
		 
		GUILayout.EndVertical ();
		GUILayout.EndScrollView ();
		GUILayout.EndArea ();
		
		if (GUI.Button(new Rect(userListLeft, userListTop+userListHeight+4, (userListWidth-5)/2, 22), "Logout"))
			smartFox.Send( new LogoutRequest());
					
		if (GUI.Button(new Rect(3+userListLeft+userListWidth/2, userListTop+userListHeight+4, (userListWidth-5)/2, 22), "Quit"))
			CloseApplication();
		
		
		//****************//
		// Game room list //
		//****************//
		GUI.Box(new Rect(gameListLeft, gameListTop, gameListWidth, gameListHeight), "Join Game", "box");
		GUILayout.BeginArea(new Rect(gameListLeft+5, gameListTop, gameListWidth-20, gameListHeight-25));
		
		GUILayout.Space(screenHeight / 24);
		
		if (smartFox.RoomList.Count != 1) {
			// We always have 1 non-game room - Main Lobby
			GUILayoutOption[] paramGameList = { GUILayout.Width(gameListWidth - 20), GUILayout.Height(gameListHeight - 25) };
			gameScrollPosition = GUILayout.BeginScrollView(gameScrollPosition, false, true, paramGameList);
			
			
			roomSelection = GUILayout.SelectionGrid(roomSelection, roomFullStrings, 1,"RoomListButton");
			if (roomSelection >= 0 && roomNameStrings[roomSelection] != currentActiveRoom.Name) 
			{
				smartFox.Send(new JoinRoomRequest(roomNameStrings[roomSelection]));
				showGameLobby = true;
			}
			
			GUILayout.EndScrollView();

		} else
			GUILayout.Label("No games available");
            
		GUILayout.EndArea();
		
		//**************//
		//  Buddy list  //
		//**************//
		
		GUI.Box(new Rect(buddyListLeft, buddyListTop, buddyListWidth, buddyListHeight), "Buddy List", "box");
		GUILayout.BeginArea(new Rect(buddyListLeft+5, buddyListTop, buddyListWidth-20, buddyListHeight-25));
		
		GUILayout.Space(screenHeight / 24);
		
		if (buddies.Count > 0) {
			GUILayoutOption[] paramBuddyList = { GUILayout.Width(buddyListWidth - 20), GUILayout.Height(buddyListHeight - 25) };
			buddiesScrollPosition = GUILayout.BeginScrollView(buddiesScrollPosition, false, true, paramBuddyList);
			//int buddiesNumber = buddies.Count;

			int j=0;
			foreach(Buddy buddy in buddies)
			{
				string online = buddy.IsOnline?" Online":" Offline";
				
				GUI.Label(new Rect(0, j*20, buddyListWidth-20, 20),buddy.Name+":"+online);
				j++;
			}
			
			GUILayout.EndScrollView();

		} else
			GUILayout.Label("No buddies");
            
		GUILayout.EndArea();
					
        if(GUI.Button(new Rect(gameListLeft+gameListWidth/2-100, gameListTop+gameListHeight+buddyListHeight+9, 200, 22), "Create Game")){
			creatingGame = true;
		}
	}
	
	private void GameCreationWindow(int windowID)
	{
		int textXPosition = (int) (windowWidth/8);
		int textWidth = 100;
		int fieldXPosition = (int) (windowWidth/2.7);
		int fieldYBasePosition = (int) (windowHeight/10);
		int fieldWidth = (int) (windowWidth/2.5);
		int fieldHeight = 20;
		GUI.Box(new Rect(0, 0, windowWidth, windowHeight), "","box");
        GUI.Label(new Rect((windowWidth/2)-45, 0, 100, 30), "Create New Game","log_label");
		
		GUI.Label(new Rect(textXPosition, fieldYBasePosition, textWidth, 100), "Game Name: ");
		gameName = GUI.TextField(new Rect(fieldXPosition, fieldYBasePosition, fieldWidth, fieldHeight), gameName, 25);
			
		GUI.Label(new Rect(textXPosition, fieldYBasePosition*2, textWidth, 100), "Is private?: ");
		gamePrivate = GUI.Toggle(new Rect(fieldXPosition, fieldYBasePosition*2, fieldWidth, fieldHeight), gamePrivate, "");
		if(gamePrivate){
			GUI.Label(new Rect(textXPosition, fieldYBasePosition*3, textWidth, 100), "Password: ");
			gamePassword = GUI.TextField(new Rect(fieldXPosition, fieldYBasePosition*3,fieldWidth, fieldHeight), gamePassword);
		}
		
		GUI.Label(new Rect(textXPosition, fieldYBasePosition*4, (fieldWidth+fieldXPosition)-textXPosition, 100), createGameErrorMessage, "errorLabel");

		if (GUI.Button(new Rect(textXPosition, fieldYBasePosition*5, fieldWidth/2, windowHeight / 12f), "Create Game"))
		{
			CreateNewGameRoom();
		}
		if (GUI.Button(new Rect(fieldXPosition + fieldWidth/2, fieldYBasePosition*5, fieldWidth/2, windowHeight / 12f), "Back"))
			ExitCreateGameLobby();
	}
	
	private void GameLobbyWindow(int windowID)
	{
		
		int chatBoxLeft = 10;
		int chatBoxTop = (int)(screenHeight/ 5);
		int chatBoxWidth = (int)(screenWidth/ 1.5);
		int chatBoxHeight = (int)(screenHeight/ 1.5);
		
		int userListLeft = chatBoxLeft+chatBoxWidth+5;
		int userListTop = chatBoxTop;
		int userListHeight = chatBoxHeight/2-5;
		int userListWidth = (int)(screenWidth/3)-30;
		
		int buddyListLeft = userListLeft;
		int buddyListTop = userListTop+userListHeight+5;
		int buddyListHeight = chatBoxHeight/2;
		int buddyListWidth = chatBoxWidth/2-30;
		
		//**************//
		// Chat history //
		//**************//
		// ChatBox
		GUI.Box(new Rect(chatBoxLeft, chatBoxTop, chatBoxWidth, chatBoxHeight), "Chat","box");
	
        GUILayout.BeginArea(new Rect(chatBoxLeft+5, chatBoxTop, chatBoxWidth - 20, chatBoxHeight));
		GUILayout.Space(screenHeight / 24);
		
		//Side Scroller
		GUILayoutOption[] param = { GUILayout.Width(chatBoxWidth - 20), GUILayout.Height(chatBoxHeight - 25) };
		chatScrollPosition = GUILayout.BeginScrollView(chatScrollPosition, false, true, param);
		
		// Populate chat with messages
		GUILayout.BeginVertical();
		// We use lock here to ensure cross-thread safety on the messages collection 
		lock (messagesLocker)
			foreach (string message in chatMessages)
				GUILayout.Label(message);
		
		GUILayout.EndScrollView();
		GUILayout.EndVertical();
		GUILayout.EndArea();

		// Send message
        newMessage = GUI.TextField(new Rect(chatBoxLeft+1, chatBoxTop+chatBoxHeight+5, chatBoxWidth-110, 20), newMessage, 50);
        if (GUI.Button(new Rect(chatBoxLeft + chatBoxWidth - 105, chatBoxTop+chatBoxHeight+4, 105, 22), "Send") || (Event.current.type == EventType.keyDown && Event.current.character == '\n')){
			processChatMessage(newMessage);
			newMessage = "";
		}
		
		GUI.Label (new Rect(chatBoxLeft+1,chatBoxTop+chatBoxHeight+25, chatBoxWidth-110,20),"Private Message: /w username text");
		GUI.Label (new Rect(chatBoxLeft+1,chatBoxTop+chatBoxHeight+40, chatBoxWidth-110,25),"Add to buddy list: /add username");
		GUI.Label (new Rect(chatBoxLeft+1,chatBoxTop+chatBoxHeight+55, chatBoxWidth-110,25),"Remove from buddy list: /remove username");
		
		//**************//
		//   User list  //
		//**************//
        GUI.Box(new Rect(userListLeft, userListTop, userListWidth, userListHeight), "Users", "box");
        GUILayout.BeginArea(new Rect(userListLeft+5, userListTop, userListWidth-20,userListHeight-25));
		
		GUILayout.Space(screenHeight / 24);
		
		//Side Scroller
		GUILayoutOption[] paramUserList = { GUILayout.Width(chatBoxWidth - 20), GUILayout.Height(chatBoxHeight - 25) };
		userScrollPosition = GUILayout.BeginScrollView(userScrollPosition, false, true, paramUserList);
		GUILayout.BeginVertical ();
			
		List<User> userList = currentActiveRoom.UserList;
			
		foreach (User user in userList)
		{
			GUILayout.Label(user.Name);
		}

		GUILayout.EndVertical ();
		GUILayout.EndScrollView ();
		GUILayout.EndArea ();
		
		//**************//
		//  Buddy list  //
		//**************//
		
		GUI.Box(new Rect(buddyListLeft, buddyListTop, buddyListWidth, buddyListHeight), "Buddy List", "box");
		GUILayout.BeginArea(new Rect(buddyListLeft+5, buddyListTop, buddyListWidth-20, buddyListHeight-25));
		
		GUILayout.Space(screenHeight / 24);
		
		if (buddies.Count > 0) {
			GUILayoutOption[] paramBuddyList = { GUILayout.Width(buddyListWidth - 20), GUILayout.Height(buddyListHeight - 25) };
			buddiesScrollPosition = GUILayout.BeginScrollView(buddiesScrollPosition, false, true, paramBuddyList);
			//int buddiesNumber = buddies.Count;

			int j=0;
			foreach(Buddy buddy in buddies)
			{
				string online = buddy.IsOnline?" Online":" Offline";
				
				GUI.Label(new Rect(0, j*20, buddyListWidth-40, 20),buddy.Name+":"+online);
				j++;
			}
			
			GUILayout.EndScrollView();

		} else
			GUILayout.Label("No buddies");
            
		GUILayout.EndArea();
		
		
		if (GUI.Button(new Rect(userListLeft, buddyListTop+buddyListHeight+4, (userListWidth-10)/3, 22), "Back"))
			ExitGameLobby();
		
		if (GUI.Button(new Rect(userListLeft+userListWidth/3, buddyListTop+buddyListHeight+4, (userListWidth-10)/3, 22), "Logout"))
			smartFox.Send( new LogoutRequest());
		
		if (GUI.Button(new Rect(userListLeft+2*userListWidth/3, buddyListTop+buddyListHeight+50, (userListWidth)/3, 22), "Start Game")){
			ISFSObject gameParams = new SFSObject();
         	gameParams.PutInt("roomId", currentActiveRoom.Id);
			gameParams.PutBool("isGame", false);
			smartFox.Send(new ExtensionRequest("startGame",gameParams));
		}
					
		if (GUI.Button(new Rect(userListLeft+2*userListWidth/3, buddyListTop+buddyListHeight+4, (userListWidth)/3, 22), "Quit"))
			CloseApplication();
	}
	
	private void CloseApplication() 
	{
		UnregisterSFSSceneCallbacks();
		Application.Quit();
	}
	
	private void CreateNewGameRoom()
   	{
		gameCreationError = false;
		
		if(gameName == ""){
			SetCustomErrorText(NOGAMENAME);
			gameCreationError = true;
		}
		
		if(gameName.Length<3 || gameName.Length > 10){
			SetCustomErrorText(SHORTORLONGNAME);
			gameCreationError = true;
		}
   
		if(!gameCreationError){
			ISFSObject param = new SFSObject();
         	param.PutUtfString("name", gameName);
			param.PutBool("isGame", false);
			
			smartFox.Send(new ExtensionRequest("createGameLobby",param));
			
			gameName = "";
			creatingGame = false;
			showLoadingScreen = false;
			showGameLobby = true;
		}
   }
	
	private void SetErrorMessages(){
		SFSErrorCodes.SetErrorMessage(2, "Username is not recognized");
		SFSErrorCodes.SetErrorMessage(3, "Wrong password");
		SFSErrorCodes.SetErrorMessage(6, "User already logged");
	}
	
	private void ResetChatHistory(){
		chatMessages = null;
		chatMessages = new ArrayList();
	}
	
	private void ExitCreateGameLobby(){
		creatingGame = false;
		gameCreationError = false;
		showLoadingScreen = false;
		showGameLobby = false;
		SetCustomErrorText("");
	}
	
	private void SetCustomErrorText(String s){
		createGameErrorMessage = s;
	}
	
	private void ExitGameLobby(){
		smartFox.Send(new JoinRoomRequest("Lobby"));
		creatingGame = false;
		gameCreationError = false;
		showLoadingScreen = false;
		showGameLobby = false;
		SetCustomErrorText("");
		roomSelection = -1;
	}
	
	public void processChatMessage(string theMessage){
		
		string[] splittedMessage = theMessage.Split(' ');
		ISFSObject parameters = new SFSObject();
		string buddyUsername;
			
		switch (splittedMessage[0]){
			case "/w": //private message
				User recipient = smartFox.UserManager.GetUserByName(splittedMessage[1]);
				theMessage = "";
				if(recipient.Name == smartFox.MySelf.Name){
					lock(messagesLocker)
						chatMessages.Add ("[Server]: you can't whisper yourself");
					break;
				}
				for(int j = 2; j<splittedMessage.Length;j++)
					theMessage += " "+splittedMessage[j];
				//ISFSObject parameters = new SFSObject();
				parameters.PutUtfString("recipientName", recipient.Name);
				smartFox.Send(new PrivateMessageRequest(theMessage,recipient.Id,parameters));
			break;
			
			case "/add": //add Buddy
				buddyUsername = splittedMessage[1];
				User buddyToAdd;
				if(buddyUsername == smartFox.MySelf.Name){
					lock(messagesLocker)
						chatMessages.Add ("[Server]: you can't add yourself to your buddy list");
					break;
				}
				if(smartFox.UserManager.ContainsUserName(buddyUsername))
				{
					if(!smartFox.BuddyManager.ContainsBuddy(buddyUsername))
					{
						smartFox.Send(new AddBuddyRequest(buddyUsername));
						lock(messagesLocker)
							chatMessages.Add("[Server]: " + buddyUsername + " added to buddy list");
					}
				}else{
					lock (messagesLocker)
						chatMessages.Add("[Server]: " + buddyUsername + " is not online");
				}
			break;
			
			case "/remove": //remove Buddy
				buddyUsername = splittedMessage[1];
				if(smartFox.BuddyManager.ContainsBuddy(buddyUsername)){
					smartFox.Send (new RemoveBuddyRequest(buddyUsername));
					lock(messagesLocker)
						chatMessages.Add("[Server]: " + buddyUsername + " removed from buddy list");
				}else{
					lock(messagesLocker)
						chatMessages.Add("[Server]: " + buddyUsername + " not present in your buddy list");
				}
			break;
			
			case "/invite": //invite to Game
				//TODO
				//	/invite username
			break;
			
			case "/join": //join an existing Game
				//TODO
				// /join gameName password
			break;
			
			default: //public message
				smartFox.Send(new PublicMessageRequest(theMessage, null , currentActiveRoom));	
			break;
		}
	}
}
