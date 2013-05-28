package sfs2x.extensions.projectsasha.game;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.gateways.*;
import sfs2x.extensions.projectsasha.game.entities.software.*;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;


import com.smartfoxserver.v2.entities.User;

import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class HackEventHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){

		Player p = new Player(sender);		
		
		trace("Request World info");
		GameWorld world = RoomHelper.getWorld(this);
		
		Gateway from = world.gateways.get(params.getUtfString("gatewayFrom"));
		Gateway to = world.gateways.get(params.getUtfString("gatewayTo"));

		boolean success = Gateway.hack(from, to);
		trace("got an hack requesto from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
		ISFSObject reback = SFSObject.newInstance();
		reback.putBool("success", success);
		send("hack", reback, sender);
		
	}
}
