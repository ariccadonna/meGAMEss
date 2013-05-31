
using System;
using System.Collections;
using UnityEngine;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Logging;


public class NetworkManager : MonoBehaviour {
	private bool running = false;

	private static NetworkManager instance;	
	public GameObject GatewayPrefab = null;
	
	private System.Object messagesLocker = new System.Object();
	private SmartFox smartFox;

	void Awake() {
		instance = this;
	}

	void Start() {
		smartFox = SmartFoxConnection.Connection;
		if (smartFox == null) {
			Application.LoadLevel("lobby");
			return;
		}

		SubscribeDelegates();
		SendWorldSetupRequest();

		running = true;

		
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
	
	private void UnsubscribeDelegates() {
		smartFox.RemoveAllEventListeners();
	}
	
	private void OnChatMessage(BaseEvent evt) {
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

    public void SendInfoRequest(String gatewayName) {
        Room room = smartFox.LastJoinedRoom;
        ISFSObject data = new SFSObject();
        data.PutUtfString("selctedGateway", gatewayName);
        ExtensionRequest request = new ExtensionRequest("gatewayInfo", data, room);
        smartFox.Send(request);
    }
	
	public void SendWorldSetupRequest()
	{
		Room room = smartFox.LastJoinedRoom;
		Debug.Log ("getWorldSetup request");
		ExtensionRequest request = new ExtensionRequest("getWorldSetup", new SFSObject(), room);
		smartFox.Send(request);
		Debug.Log ("getWorldSetup sent");
	}
		
	public void TimeSyncRequest() {
		Room room = smartFox.LastJoinedRoom;
		ExtensionRequest request = new ExtensionRequest("getTime", new SFSObject(), room);
		smartFox.Send(request);
	}
	
	private void OnConnectionLost(BaseEvent evt) {
		print ("disconnected for this reason: "+ evt.Params["reason"]);
        UnsubscribeDelegates();
		Screen.lockCursor = false;
		Screen.showCursor = true;
		SmartFoxConnection.Connection = null; 
		Application.LoadLevel("loginScreen");
	}
	
	private void OnExtensionResponse(BaseEvent evt) {
		try {
			string cmd = (string)evt.Params["cmd"];
			ISFSObject data = (ISFSObject)evt.Params["params"];
											
			if (cmd == "hack") 
			{
				//handle result
			}
			else if (cmd == "gatewayInfo") 
			{
				Debug.Log ("info request recived for"+data.GetUtfString("state"));	
				//handle result
			}
			else if (cmd == "getWorldSetup")
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
				}
			}
		}
		catch (Exception e) {
			Debug.Log("Exception handling response: "+e.Message+" >>> "+e.StackTrace);
		}
	}
	
	void OnGUI(){
		 if (GUI.Button(new Rect(100,100,100,100), "hack"))
            {
			Room room = smartFox.LastJoinedRoom;
			ISFSObject data = new SFSObject();
			data.PutUtfString("gatewayFrom", "iceland");
			data.PutUtfString("gatewayTo", "ukraine");
			smartFox.Send(new ExtensionRequest("hack", data, room));
            }
	}
	
	private void OnUserLeaveRoom(BaseEvent evt) {
		User user = (User)evt.Params["user"];
		Room room = (Room)evt.Params["room"];
        Debug.Log("User " + user.Name + " left room " + room);
	}
	
	void OnApplicationQuit() {
		UnsubscribeDelegates();
		smartFox.Disconnect();
	}


	public void SendLeaveRoom() {
		UnsubscribeDelegates();
		smartFox.Send(new JoinRoomRequest("Lobby", null, smartFox.LastJoinedRoom.Id));
		Application.LoadLevel("loginScreen");
	}
	
	public void SendHackRequest(String fromGateway, String toGateway) {
		ISFSObject data = new SFSObject();
		data.PutUtfString("from", fromGateway);
		data.PutUtfString("to", toGateway);
		
		smartFox.Send(new ExtensionRequest("hack", data, smartFox.LastJoinedRoom));
	}
}
