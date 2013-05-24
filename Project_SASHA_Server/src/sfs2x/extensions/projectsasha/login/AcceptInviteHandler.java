package sfs2x.extensions.projectsasha.login;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.exceptions.SFSJoinRoomException;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class AcceptInviteHandler extends BaseClientRequestHandler
{
	@Override
	public void handleClientRequest(User sender, ISFSObject params)
	{
		String gameName = params.getUtfString("gameName");
		Room game = sender.getZone().getRoomByName(gameName);
		String gamePassword = game.getPassword();
		
		ISFSObject reback = SFSObject.newInstance();
		
		try{
			getApi().joinRoom(sender, game, gamePassword, false, sender.getLastJoinedRoom());
			reback.putBool("success", true);
			reback.putUtfString("gameName", gameName);
		}catch (SFSJoinRoomException e){
			e.printStackTrace();
		    reback.putBool("success", false);
		    reback.putUtfString("gameName", gameName);
		}finally{
			send("acceptInvite", reback, sender);
		}
	}
}
