package sfs2x.extensions.projectsasha.game;

import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class PolicePositionHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){
		
		Gateway policePosition = RoomHelper.getPolicePosition(this);
		
		ISFSObject reback = SFSObject.newInstance();
		reback.putInt("police_x", policePosition.getX());
		reback.putInt("police_y", policePosition.getY());

		send("policePosition", reback, RoomHelper.getCurrentRoom(this).getUserList());
	}
}
