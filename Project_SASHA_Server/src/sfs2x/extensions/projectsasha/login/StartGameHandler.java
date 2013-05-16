package sfs2x.extensions.projectsasha.login;

import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;


public class StartGameHandler extends BaseClientRequestHandler{

	@Override
	public void handleClientRequest(User sender, ISFSObject params){
		//trace(params);
		//SetupGame(sender, params);
		Integer roomId = params.getInt("roomId");
		Room room = sender.getZone().getRoomById(roomId);
		
		ISFSObject reback = SFSObject.newInstance();
		reback.putBool("success", true);
		send("startGame", reback, room.getUserList());
	}

}

