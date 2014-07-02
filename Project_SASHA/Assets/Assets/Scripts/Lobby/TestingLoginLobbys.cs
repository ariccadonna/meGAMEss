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


public class TestingLoginLobbys : MonoBehaviour {
	private SmartFox smartFox;
	private string zone = "World1";
	private string serverName = "127.0.0.1";
	private int serverPort = 9933;
	private string username = "";
	private string password = "";
	
	private bool creatingGame;
	private bool showGameLobby;
	private bool myOnline;
	
	// Use this for initialization
	void Start () 
	{
		PlayerPrefs.SetInt("playerNumber", 1);
		smartFox = new SmartFox(true);

		smartFox.AddLogListener(LogLevel.INFO, OnDebugMessage);
		
		username = "Test";
		password = "5f4dcc3b5aa765d61d8327deb882cf99";
		AddEventListeners();
		smartFox.Connect(serverName, serverPort);
	}
	
	void FixedUpdate() 
	{
		smartFox.ProcessEvents();
	}
	
	// Update is called once per frame
	void Update () 
	{
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
		
		
	}
	
	private void UnregisterSFSSceneCallbacks() 
	{
		smartFox.RemoveAllEventListeners();
	}
	
	/**********************/
	/* Listeners Handlers */
	/**********************/
	
	public void OnConnection(BaseEvent evt) 
	{
        bool success = (bool)evt.Params["success"];
        string error = (string)evt.Params["errorMessage"];

        Debug.Log("On Connection callback got: " + success + " (error : <" + error + ">)");

        if (success)
        {
            SmartFoxConnection.Connection = smartFox;
            Debug.Log("Sending login request");

            smartFox.Send(new LoginRequest(username, password, zone));
        }
    }
	
	public void OnConnectionLost(BaseEvent evt) {
		Debug.Log("OnConnectionLost");
		UnregisterSFSSceneCallbacks();
	}
	
	public void OnDebugMessage(BaseEvent evt) 
	{
		string message = (string)evt.Params["message"];
		Debug.Log("[SFS DEBUG] " + message);
	}
	
	public void OnUdpInit(BaseEvent evt) 
	{
		if (evt.Params.ContainsKey("success") && !(bool)evt.Params["success"]) {

			Debug.Log("UDP error");
		} else {
			Debug.Log("UDP ok");
		}
	}
	
	/*
	 * Handle login event
	 */
	public void OnLogin(BaseEvent evt) {
		try {
			if (evt.Params.ContainsKey("success") && !(bool)evt.Params["success"]) {
				Debug.Log("Login error");
			}
			else 
			{
				
				//Startup UDP
				smartFox.InitUDP(serverName, serverPort);
			 	RoomSettings rs = new RoomSettings("Test_room");
				smartFox.Send(new CreateRoomRequest(rs,true));
				
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
       if (smartFox.IsConnected)
           smartFox.Disconnect();
   }
	
	/* 
	 * Handle logout event
	 */
	public void OnLogout(BaseEvent evt) {
		smartFox.RemoveAllEventListeners();
		smartFox.Disconnect();

	}
	
	/*
	 * Handler a join room event
	 */
	public void OnJoinRoom(BaseEvent evt) 
	{
		Room room = (Room)evt.Params["room"];
		// If we joined a game room, then we either created it (and auto joined) or manually selected a game to join
		ISFSObject gameParams = new SFSObject();
        gameParams.PutInt("roomId", room.Id);
		gameParams.PutBool("isGame", false);
		smartFox.Send(new ExtensionRequest("startGame",gameParams));
	}
	
	/*
	 * Handle a new user that just entered the current room
	 */
	public void OnUserEnterRoom(BaseEvent evt) 
	{
	}

	/*
	 * Handle a user who left the room
	 */
	private void OnUserLeaveRoom(BaseEvent evt) 
	{
	}
	
	/*
	 * Handle a new room in the room list
	 */
	public void OnRoomAdded(BaseEvent evt) 
	{
	}
	
	/*
	 * Handle an error while creating a room
	 */
	public void OnCreateRoomError(BaseEvent evt) 
	{
	}
	
	/*
	 * Handle a count change in one room of the zone
	 */
	public void OnUserCountChange(BaseEvent evt) 
	{
	}

	/*
	 * Handle a room that was removed
	 */
	public void OnRoomDeleted(BaseEvent evt) 
	{
	}
	
	/*
	 * Handle a public message
	 */
	private void OnPublicMessage(BaseEvent evt) 
	{
	}
	
	/*
	 * Handle a private message 
	 */
	private void OnPrivateMessage(BaseEvent evt) 
	{
	}
	
	/*
	* Handle response from server side
	*/
	public void OnExtensionResponse(BaseEvent evt)
	{
		string cmd = (string)evt.Params["cmd"];
		switch (cmd) {

			case "startGame":
				smartFox.Send(new JoinRoomRequest("Test_room",""));
				UnregisterSFSSceneCallbacks();
				Application.LoadLevel("worldBoard2");
			break;
		}
		
			
	}
	
	/*
	 * Close the game
	 */
	private void CloseApplication() 
	{
		UnregisterSFSSceneCallbacks();
		Application.Quit();
	}
}
