package sfs2x.extensions.projectsasha.game;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.gateways.*;
import sfs2x.extensions.projectsasha.game.entities.software.Firewall;
import sfs2x.extensions.projectsasha.game.entities.software.Software;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;


import com.smartfoxserver.v2.entities.User;

import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class HackEventHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){

		/*Player p = new Player(sender);		
		GameWorld world = RoomHelper.getWorld(this);
		Gateway target_gateway = world.gateways.get("ukraine");
		Player gateway_owner = target_gateway.getOwner();
		
		Software s = new Firewall(1);
		target_gateway.installSoftware(s);
		
		if(gateway_owner == null){
			//conquer the gateway with formula
			//target_gateway.getBaseAttackLevel();
			target_gateway.setOwner(p);
		}else{
			//neutralize or conquer;
		}
		target_gateway.toString();
				
		*/
		
	}
}
