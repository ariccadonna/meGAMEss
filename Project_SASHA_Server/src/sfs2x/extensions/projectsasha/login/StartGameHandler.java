package sfs2x.extensions.projectsasha.login;
import java.util.List;

import com.smartfoxserver.v2.api.CreateRoomSettings;
import com.smartfoxserver.v2.api.CreateRoomSettings.RoomExtensionSettings;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.SFSRoomRemoveMode;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.exceptions.SFSCreateRoomException;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class StartGameHandler extends BaseClientRequestHandler{
	@Override
	public void handleClientRequest(User sender, ISFSObject params){
		Integer roomId = params.getInt("roomId");
		Room room = sender.getZone().getRoomById(roomId);
		Room clonedRoom = null;
		List <User> userList;
		
		ISFSObject reback = SFSObject.newInstance();
		reback.putBool("success", true);
		
		userList = room.getUserList();
		
		CreateRoomSettings crs = new CreateRoomSettings();
		crs.setAutoRemoveMode(SFSRoomRemoveMode.WHEN_EMPTY);
		crs.setName(room.getName());
		crs.setPassword(room.getPassword());
		crs.setHidden(room.isHidden());
		crs.setGroupId("default");
		crs.setMaxVariablesAllowed(100);
		crs.setMaxSpectators(0);
		crs.setMaxUsers(5);
		crs.setGame(true);
		crs.setDynamic(true);
		crs.setExtension(new RoomExtensionSettings("project_SASHA", "sfs2x.projectsasha.game.GameExtension"));

		
		for (User u : userList) {
				room.removeUser(u);
		}
	
		getApi().removeRoom(room);

		try {clonedRoom = getApi().createRoom(sender.getZone(),crs,userList.get(0));} 
		catch (SFSCreateRoomException e) {e.printStackTrace();}
		 reback.putUtfString("roomName", clonedRoom.getName());
		 reback.putUtfString("password", clonedRoom.getPassword());
		 /*
		for (User u: userList){
			try {
				clonedRoom.addUser(u);
			}
			catch (SFSJoinRoomException e){e.printStackTrace();}
		}*/
		
		send("startGame", reback, userList);
	}
}
