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
using Sfs2X.Entities.Invitation;
using Sfs2X.Requests;
using Sfs2X.Requests.Game;
using Sfs2X.Logging;
using Sfs2X.Entities.Data;
using Sfs2X.Util;


public class LoginLobbys : MonoBehaviour {
	private const string LASTIP = "LAST_IP_USED";
	private const string LASTUSERNAME = "LAST_USER_NAME_USED";
	private const string LASTPASSWORD = "LAST_PASSWORD_USED";
	private const string MOTD = "Welcome, type /h or /help for chat commands";
	
	private SmartFox smartFox;
	private string zone = "World1";
	private string serverIp = "";
	private int serverPort = 9933;
	private string username = "";
	private string password = "";
	
	private bool isLoggedIn;
	private bool creatingGame;
	private bool tutorial;
	private bool showGameLobby;
	private bool loginError = false;
	private string loginErrorMessage = "";
	private bool gameCreationError = false;
	private string createGameErrorMessage = "";
	private string errorMessage = "";
	private bool showLoadingScreen = false;
	
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
	private string[] roomNameStrings;
	private string[] roomFullStrings;
	
	private int screenWidth;
	private int screenHeight;
	private int windowWidth;
	private int windowHeight;
	
	private int step = 1;
	
	private const string NOGAMENAME = "Insert Game Name!";
	private const string SHORTORLONGNAME = "Game Name length should be between 3 and 10";
	
	public Texture2D icon_available, icon_away,	icon_blocked, icon_occupied, icon_offline;
	
	private ArrayList messages = new ArrayList();
	private List<Buddy> buddies = new List<Buddy>();
	GUIContent[] buddyContents;
	
	private Invitation currentInvitation;
	
	// Use this for initialization
	void Start () 
	{
		bool debug = true;
		if (SmartFoxConnection.IsInitialized)
		{
			smartFox = SmartFoxConnection.Connection;
			AddEventListeners();
			isLoggedIn = true;
			//SetupGeneralLobby();
		}
		else
		{
			isLoggedIn = false;
			smartFox = new SmartFox(debug);
		}
		
		smartFox.AddLogListener(LogLevel.INFO, OnDebugMessage);
		
		SetErrorMessages();
		
		serverIp = PlayerPrefs.GetString(LASTIP, "");
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
					if(!creatingGame && !showGameLobby && !tutorial)
					{
						displayWindow(11); //GeneralLobby
					}
					else if(creatingGame)
					{
						displayWindow(12, new Rect((int)(screenWidth/3.6), (int)(screenHeight/4),windowWidth, windowHeight)); //Game Creation
					}
					else if(tutorial)
					{
						displayWindow(14); //Tutorial
					}
					else
					{
						displayWindow(13); //Game Lobby
					}
				} 
				else  //Loading Screen
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
		smartFox.AddEventListener(SFSEvent.INVITATION, onInvitationReceived);
		smartFox.AddEventListener(SFSEvent.INVITATION_REPLY, OnInvitationReply);
		smartFox.AddEventListener(SFSEvent.INVITATION_REPLY_ERROR, onInvitationReplyError);
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
				PlayerPrefs.SetString(LASTIP, serverIp);
				PlayerPrefs.SetString(LASTUSERNAME, username);
				PlayerPrefs.SetString(LASTPASSWORD, password);
				
				
				isLoggedIn = true;
				
				//Startup UDP
				smartFox.InitUDP(serverIp, serverPort);
				
				SetupGeneralLobby();
				
				smartFox.Send(new InitBuddyListRequest());		
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
		tutorial = false;
		showGameLobby = false;
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
		//Message of the day setup
		lock(messagesLocker)
			chatMessages.Add(MOTD);
		Room room = (Room)evt.Params["room"];
		currentActiveRoom = room;
		SetupGameList();
		// If we joined a game room, then we either created it (and auto joined) or manually selected a game to join
		if (room.IsGame) {
			showLoadingScreen = true;
			Debug.Log("Joined game room " + room.Name);
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
			Debug.Log("Public message in:"+evt.Params["room"]);
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
							chatMessages.Add("[SASHA]: " + sender.Name + " added you to buddy list");
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
	 * Handle a invitation reply 
	 */
	public void OnInvitationReply(BaseEvent evt)
	{
		currentInvitation = (Invitation) evt.Params["invitation"];
		User invitee = (User)evt.Params["invitee"];
		if ((InvitationReply)evt.Params["reply"] == InvitationReply.REFUSE)
		{
			lock(messagesLocker) 
				chatMessages.Add("[SASHA]: " + invitee.Name + " has refused the invitation");
		}
	}
	
	/*
	 * Handle a invitation recived
	 */
	public void onInvitationReceived(BaseEvent evt)
	{
		currentInvitation = (Invitation) evt.Params["invitation"];
		lock(messagesLocker) 
			chatMessages.Add(currentInvitation.Inviter.Name + " invited you to a game. Type /accept to join");
	}
	
	/*
	 * Handle invitation errors
	 */
	public void onInvitationReplyError(BaseEvent evt){
		Debug.Log("Failed to reply to invitation due to the following problem: " + evt.Params["errorMessage"]);
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
				smartFox.Send (new JoinRoomRequest(dataObject.GetUtfString("roomName"),dataObject.GetUtfString("password")));
			break;
			
		case "startGame":
			smartFox.Send(new JoinRoomRequest(dataObject.GetUtfString("roomName"),dataObject.GetUtfString("password")));
			UnregisterSFSSceneCallbacks();
			PlayerPrefs.SetInt("playerNumber", currentActiveRoom.UserCount);
			Application.LoadLevel("worldBoard2");
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
			messages.Add((Buddy)evt.Params["buddy"] + " removed" );
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
			messages.Add((Buddy)evt.Params["buddy"] + " " + message );
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
	}
	
	/*
	 * Initializes all buddies and your buddy variables
	 */
	private void OnBuddyListInit(BaseEvent evt)
	{
		// Populate user's list of buddies
		OnBuddyListUpdate(evt);		
		
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
		Debug.Log(("Buddy variables update from: " + (Buddy)evt.Params["buddy"]));
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
	
	/*
	 * Calculate MD5 of input string
	 */
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
	
	/*
	 * Replace Default GUI.skin with a custom one
	 */
	private void ReplaceGUIStyle()
	{
		GUI.skin = gSkin;
	}
	
	/*
	 * General lobby initial setup
	 */
	private void SetupGeneralLobby() {
		SetupGameList();
	}
	
	/*
	 * Game list setup fired at each update
	 */
	private void SetupGameList() {
		
		List<string> rooms = new List<string> ();
		List<string> roomsFull = new List<string> ();
		
		List<Room> allRooms = smartFox.RoomManager.GetRoomList();
		
		foreach (Room room in allRooms) {
			// Only show game rooms
			if (room.Name == "Lobby") {
				continue;
			}
			
			if(room.IsHidden)
				continue;
			
			Debug.Log("Room id: " + room.Id + " has name: " + room.Name);
			
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
	
	/*
	 * Display the window specified by windowID
	 */
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
		case 14:
			WinFunction = TutorialWindow;
			break;
		}
		
		GUI.Window(windowID,rectPos,WinFunction,"");
	}
	
	/*
	 * Display the window specified by windowID at rectPos
	 */
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
		case 14:
			WinFunction = TutorialWindow;
			break;
		}
		
		GUI.Window(windowID,rectPos,WinFunction,"");
	}
	
	/*
	 * Display the login window
	 */
	private void LoginWindow(int windowID)
	{
		int textXPosition = (int) (windowWidth/8);
		int fieldXPosition = (int) (windowWidth/2.7);
		int fieldYBasePosition = (int) (windowHeight/10);
		int fieldWidth = (int) (windowWidth/2.5);
		int fieldHeight = 20;
		int labelHeight = 100;
		int boxHeight = (fieldYBasePosition*6+windowHeight/12) -fieldYBasePosition;
		
		GUI.Box(new Rect(textXPosition-20, fieldYBasePosition-20 , fieldXPosition+fieldWidth-textXPosition+60, boxHeight), "","smallBox");
		
		GUI.Label(new Rect(textXPosition, fieldYBasePosition, 100, labelHeight), "Username: ");
		username = GUI.TextField(new Rect(fieldXPosition, fieldYBasePosition, fieldWidth, fieldHeight), username, 25);
		
		GUI.Label(new Rect(textXPosition, fieldYBasePosition*2, 100, labelHeight), "Password: ");
		password = GUI.PasswordField(new Rect(fieldXPosition, fieldYBasePosition*2,fieldWidth, fieldHeight), password,'*', 10);
		
		GUI.Label(new Rect(textXPosition, fieldYBasePosition*3, 100, labelHeight), "Server: ");
		serverIp = GUI.TextField(new Rect(fieldXPosition, fieldYBasePosition*3, fieldWidth, fieldHeight), serverIp, 25);
		
		GUI.Label(new Rect(textXPosition, fieldYBasePosition*4, 100, labelHeight), "Port: ");
		serverPort = int.Parse(GUI.TextField(new Rect(fieldXPosition, fieldYBasePosition*4, fieldWidth, fieldHeight), serverPort.ToString(), 4));
		
		GUI.Label(new Rect(10, 240, 100, 100), loginErrorMessage);
		
		if (GUI.Button(new Rect(textXPosition, fieldYBasePosition*5, fieldWidth/2, windowHeight / 12f), "Login")  || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
		{
			AddEventListeners();
			smartFox.Connect(serverIp, serverPort);
		}
		if (GUI.Button(new Rect(fieldXPosition + fieldWidth/2, fieldYBasePosition*5, fieldWidth/2, windowHeight / 12f), "Quit"))
			CloseApplication();
	}
	
	/*
	 * Display the general lobby window
	 */
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
		
		//GUI.Label (new Rect(chatBoxLeft+1,chatBoxTop+chatBoxHeight+25, chatBoxWidth-110,20),"[/help][/h] shows chat commands");
		
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
			smartFox.Send(new LogoutRequest());
		
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
	
		
		if(GUI.Button(new Rect(gameListLeft+gameListWidth/2-100, buddyListTop+buddyListHeight+50, 200, 22), "Tutorial")){
			tutorial = true;
		}
		
		if(GUI.Button(new Rect(gameListLeft+gameListWidth/2-100, gameListTop+gameListHeight+buddyListHeight+9, 200, 22), "Create Game")){
			creatingGame = true;
		}
	}
	
	/*
	 * Display the game creation window
	 */
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
	
	/*
	 * Display the tutorial window
	 */
	private void TutorialWindow(int windowID)
	{
			
		if(GUI.Button(new Rect(0,0,screenWidth, screenHeight), ""))
		{
			step++;
			displayWindow(14);
		}
		if (step == 1)
			GUI.Label(new Rect(0, 0, screenWidth, screenHeight), "", "tut1");
		if(step == 2)
			GUI.Label(new Rect(0, 0, screenWidth, screenHeight), "", "tut2");
		if(step == 3)
			GUI.Label(new Rect(0, 0, screenWidth, screenHeight), "", "tut3");
		if(step == 4)
			GUI.Label(new Rect(0, 0, screenWidth, screenHeight), "", "tut4");
		if(step == 5)
		{
			step = 1;
			tutorial = false;
		}
	}
	
	/*
	 * Display the game lobby window
	 */
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
		
		//GUI.Label (new Rect(chatBoxLeft+1,chatBoxTop+chatBoxHeight+25, chatBoxWidth-110,20),"[/help][/h] shows chat commands");
		
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
			smartFox.Send(new LogoutRequest());
		
		if (GUI.Button(new Rect(userListLeft+2*userListWidth/3, buddyListTop+buddyListHeight+50, (userListWidth)/3, 22), "Start Game")){
			ISFSObject gameParams = new SFSObject();
			gameParams.PutInt("roomId", currentActiveRoom.Id);
			gameParams.PutBool("isGame", false);
			smartFox.Send(new ExtensionRequest("startGame",gameParams));
			
			showLoadingScreen = true;
			Debug.Log("Started Game " + currentActiveRoom.Name);
		}
		
		if (GUI.Button(new Rect(userListLeft+2*userListWidth/3, buddyListTop+buddyListHeight+4, (userListWidth)/3, 22), "Quit"))
			CloseApplication();
	}
	
	/*
	 * Close the game
	 */
	private void CloseApplication() 
	{
		UnregisterSFSSceneCallbacks();
		Application.Quit();
	}
	
	/*
	 * Check params and create new game lobby
	 */
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
			param.PutBool("isPrivate", gamePrivate);
			param.PutUtfString("password", gamePassword);
			smartFox.Send(new ExtensionRequest("createGameLobby",param));
			
			gameName = "";
			creatingGame = false;
			tutorial = false;
			showLoadingScreen = false;
			showGameLobby = true;
		}
	}
	
	/*
	 * Custom error messages
	 */
	private void SetErrorMessages(){
		SFSErrorCodes.SetErrorMessage(2, "Username is not recognized");
		SFSErrorCodes.SetErrorMessage(3, "Wrong password");
		SFSErrorCodes.SetErrorMessage(6, "User already logged");
	}
	
	/*
	 * Reset chat history
	 */
	private void ResetChatHistory(){
		chatMessages = null;
		chatMessages = new ArrayList();
	}
	
	/*
	 * Exit from GameCreationWindow()
	 */
	private void ExitCreateGameLobby(){
		creatingGame = false;
		tutorial = false;
		gameCreationError = false;
		showLoadingScreen = false;
		showGameLobby = false;
		SetCustomErrorText("");
	}
	
	/*
	 * Aux function to set error text
	 */
	private void SetCustomErrorText(string s){
		createGameErrorMessage = s;
	}
	
	/*
	 * Exit from GameLobbyWindow()
	 */
	private void ExitGameLobby(){
		smartFox.Send(new JoinRoomRequest("Lobby"));
		creatingGame = false;
		tutorial = false;
		gameCreationError = false;
		showLoadingScreen = false;
		showGameLobby = false;
		SetCustomErrorText("");
		roomSelection = -1;
	}
	
	/*
	 * Process chat messages/commands
	 * Available commands:
	 * /w [or /whisper] <username> <text>
	 * /add <username>
	 * /rm [or /remove] <username>
	 * /inv [or /invite] <username>
	 * /accept
	 * /refuse
	 * /j [or /join] <gameName> [<gamePassword>]
	 * /h [or /help]
	 */   
	public void processChatMessage(string theMessage){
		
		string[] splittedMessage = theMessage.Split(' ');
		int parametersLength = splittedMessage.Length;
		ISFSObject parameters = new SFSObject();
		string buddyUsername, gameName, gamePassword;
		if (theMessage.Length == 0)
			return;
		char[] chars = splittedMessage[0].ToCharArray();
		bool isCommand = chars[0] == '/'?true:false;
		Room gameRoom;
		
		if(isCommand){
			switch (splittedMessage[0]){
				
				/*
				 * whisper a user
				 * /whisper <username> <text>
				 */
			case "/whisper":
			case "/w": //private message
				
				if(parametersLength == 1 || splittedMessage[1]=="")//missing username and message
				{
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: missing username and message \n usage: /w <username> <text>");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				
				if(parametersLength == 2 || splittedMessage[2]=="")//missing message
				{
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: missing message \n usage: /w <username> <text>");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				
				User recipient = smartFox.UserManager.GetUserByName(splittedMessage[1]);
				theMessage = "";
				if(recipient == null){
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: user " + splittedMessage[1] + " is not online");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				if(recipient.Name == smartFox.MySelf.Name){
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: you can't whisper yourself");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				for(int j = 2; j<splittedMessage.Length;j++)
					theMessage += " "+splittedMessage[j];
				parameters.PutUtfString("recipientName", recipient.Name);
				smartFox.Send(new PrivateMessageRequest(theMessage,recipient.Id,parameters));
				break;
				
				/*
				 * add to buddy list
				 * /add <username>
				 */
			case "/add": //add Buddy
				
				if(parametersLength == 1)//missing username and message
				{
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: missing username \n usage: /add <username>");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				
				buddyUsername = splittedMessage[1];
				if(buddyUsername == smartFox.MySelf.Name){
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: you can't add yourself to your buddy list");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				if(smartFox.UserManager.ContainsUserName(buddyUsername))
				{
					if(!smartFox.BuddyManager.ContainsBuddy(buddyUsername))
					{
						smartFox.Send(new AddBuddyRequest(buddyUsername));
						lock(messagesLocker){
							chatMessages.Add("[SASHA]: " + buddyUsername + " added to buddy list");
							chatScrollPosition.y = Mathf.Infinity;
						}
					}
				}else{
					lock (messagesLocker){
						chatMessages.Add("[SASHA]: " + buddyUsername + " is not online");
						chatScrollPosition.y = Mathf.Infinity;
					}
				}
				break;
				
				/*
				 * remove from buddy list
				 * /remove <username>
				 */
			case "/remove":
			case "/rm": //remove Buddy
				if(parametersLength == 1) //missing username and message
				{
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: missing username \n usage: [/rm][/remove] <username>");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				
				buddyUsername = splittedMessage[1];
				if(smartFox.BuddyManager.ContainsBuddy(buddyUsername)){
					smartFox.Send (new RemoveBuddyRequest(buddyUsername));
					lock(messagesLocker){
						chatMessages.Add("[SASHA]: " + buddyUsername + " removed from buddy list");
						chatScrollPosition.y = Mathf.Infinity;
					}
				}else{
					lock(messagesLocker){
						chatMessages.Add("[SASHA]: " + buddyUsername + " not present in your buddy list");
						chatScrollPosition.y = Mathf.Infinity;
					}
				}
				break;
				
				/*
				 * invite to current Game
				 * /invite <username>
				 */
			case "/invite":
			case "/inv":
				if(parametersLength == 1) //missing username and message
				{
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: missing username \n usage: [/inv][/invite] <username>");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				
				buddyUsername = splittedMessage[1];
				User user1 = smartFox.UserManager.GetUserByName(buddyUsername);
				if(user1.Name == smartFox.MySelf.Name){
					lock(messagesLocker)
						chatMessages.Add ("[SASHA]: you can't add yourself to current game");
					break;
				}
				if(smartFox.UserManager.ContainsUser(user1)){
					List<object> invitedUsers = new List<object>();
					invitedUsers.Add(user1);
					parameters.PutUtfString("gameName", smartFox.LastJoinedRoom.Name);
					smartFox.Send(new InviteUsersRequest(invitedUsers, 20, parameters));
					smartFox.Send(new PublicMessageRequest(smartFox.MySelf.Name + " invited " + buddyUsername + " to current game"));
				}else{
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: " + buddyUsername + " is not online");
						chatScrollPosition.y = Mathf.Infinity;
					}
				}
				break;
				
				/*
				 * join an existing Game
				 * /join <gameName> [<gamePassword>]
				 */
			case "/join":
			case "/j": 
				if(parametersLength < 2)
				{
					lock(messagesLocker){
						chatMessages.Add ("[SASHA]: missing parameters \n usage: [/j][/join] <gameName> [<gamePassword>]");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				gameName = splittedMessage[1];
				if(parametersLength > 2) //if there is a password
					gamePassword = splittedMessage[2];
				else
					gamePassword = "";
				
				if(smartFox.RoomManager.ContainsRoom(gameName))
				{
					//if gameLobby exist join it
					smartFox.Send (new JoinRoomRequest(gameName,gamePassword));
					creatingGame = false;
					tutorial = false;
					showLoadingScreen = false;
					showGameLobby = true;
				}
				else
				{
					//if not, create a new gameLobby
					parameters.PutUtfString("name", gameName);
					parameters.PutBool("isGame", false);
					parameters.PutUtfString("password", gamePassword);
					if(gamePassword!="")
						parameters.PutBool("isPrivate", true);
					else
						parameters.PutBool("isPrivate", false);
					smartFox.Send(new ExtensionRequest("createGameLobby",parameters));
					creatingGame = false;
					tutorial = false;
					showLoadingScreen = false;
					showGameLobby = true;
				}
				break;
				
				/*
				 * accept current invite
				 * /accept
				 */
			case "/accept": //accept an invite to a game
				if(currentInvitation == null){
					lock(messagesLocker){
						chatMessages.Add("[SASHA]: You don't have any game invitation to accept");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				parameters = (SFSObject)currentInvitation.Params;
				gameName = (string) parameters.GetUtfString("gameName");
				gameRoom = smartFox.RoomManager.GetRoomByName(gameName);
				smartFox.Send (new InvitationReplyRequest(currentInvitation,InvitationReply.ACCEPT));
				if(gameRoom!=null && !gameRoom.IsHidden){
					smartFox.Send (new JoinRoomRequest(gameName));
				}else if(gameRoom.IsHidden){
					parameters.PutUtfString("gameName", gameName);
					smartFox.Send(new ExtensionRequest("acceptInvite",parameters));
				}	
				creatingGame = false;
				tutorial = false;
				showLoadingScreen = false;
				showGameLobby = true;
				break;
				
				/*
				 * refuse current invite
				 * /refuse
				 */
			case "/refuse": //accept an invite to a game
				if(currentInvitation == null){
					lock(messagesLocker){
						chatMessages.Add("[SASHA]: You don't have any game invitation to refuse");
						chatScrollPosition.y = Mathf.Infinity;
					}
					break;
				}
				parameters = (SFSObject)currentInvitation.Params;
				smartFox.Send (new InvitationReplyRequest(currentInvitation,InvitationReply.REFUSE));
				currentInvitation = null;
				break;
				
				/*
				 * leave current gameLobby
				 * /leave
				 */
			case "/leave":
				if(currentActiveRoom.Name != "Lobby")
					ExitGameLobby();
				else
				lock(messagesLocker){
					chatMessages.Add("[SASHA]: You can't leave general lobby");
					chatScrollPosition.y = Mathf.Infinity;
				}
				break;
				
				/*
				 * logout from server
				 * /logout
				 */
			case "/logout":
				smartFox.Send(new LogoutRequest());
				break;
				
				/*
				 * quit the application
				 * /quit
				 */
			case "/quit":
				CloseApplication();
				break;
				
			case "/help":
			case "/h":
				lock(messagesLocker){
					chatMessages.Add("--------------------------HELP--------------------------");
					chatMessages.Add("[/w][/whisper] <username> <text> : send a PM to username");
					chatMessages.Add("[/add] <username> : add username to buddy list");
					chatMessages.Add("[/rm][/remove] <username> : remove username from buddy list");
					chatMessages.Add("[/inv][/invite] <username> : invite username to current game");
					chatMessages.Add("[/accept] : accept current invite request");
					chatMessages.Add("[/refuse] : refuse current invite request");
					chatMessages.Add("[/j][/join] <gameName> [<gamePassword>]: join/create gameName with an \n\t\t\t\t\t\t optional password");
					chatMessages.Add("--------------------------------------------------------");
					chatScrollPosition.y = Mathf.Infinity;
				}
				//chatMessages.Add("[/h][/help] : shows this messages");
				
				break;
			default: //command not found
				lock(messagesLocker){
					chatMessages.Add("[SASHA]: can't find command "+splittedMessage[0]);
					chatScrollPosition.y = Mathf.Infinity;
				}
				break;
			}
		}else{
			//public message
			smartFox.Send(new PublicMessageRequest(theMessage, null , currentActiveRoom));	
		}
	}
}
