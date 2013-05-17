package sfs2x.extensions.projectsasha.login;

import com.smartfoxserver.v2.api.CreateRoomSettings;
import com.smartfoxserver.v2.entities.SFSRoomRemoveMode;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.exceptions.SFSCreateRoomException;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;


public class CreateGameLobby extends BaseClientRequestHandler{

	@Override
	public void handleClientRequest(User sender, ISFSObject params){
		createGameLobby(sender, params);
	}
	
	private void createGameLobby(User sender, ISFSObject params){

		CreateRoomSettings crs = new CreateRoomSettings();
		String name = params.getUtfString("name");
		String password = "";
		boolean isPrivate = false;
		boolean isGame = params.getBool("isGame");
		if(params.getBool("isPrivate")){
			 password = params.getUtfString("password");
			 isPrivate = params.getBool("isPrivate");
		}
		
		crs.setAutoRemoveMode(SFSRoomRemoveMode.WHEN_EMPTY);
		crs.setName(name);
		crs.setPassword(password);
		crs.setHidden(isPrivate);
		crs.setGroupId("default");
		crs.setGame(isGame);
		crs.setMaxVariablesAllowed(100);
		crs.setMaxSpectators(0);
		crs.setMaxUsers(5);
		crs.setDynamic(true);
		
		      
		ISFSObject reback = SFSObject.newInstance();
		
		try {
			getApi().createRoom(sender.getZone(),crs,sender);
		    reback.putBool("success", true);
		    reback.putUtfString("roomName", name);
		    reback.putUtfString("password", password);
		} catch (SFSCreateRoomException e) {
			e.printStackTrace();
		    reback.putBool("success", false);
		    reback.putUtfString("roomName", name);
		}finally{
			send("createGameLobby", reback, sender);
		}
	}

}

