package sfs2x.extensions.projectsasha.game;


import java.util.Iterator;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.objectives.Objective;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class ObjectiveHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){
		
		String JSONString;
		Player p = RoomHelper.getPlayer(this, sender.getName());
		GameWorld world = RoomHelper.getWorld(this);
		Objective[] gameObjectives = RoomHelper.getObjectives(this);
		
		JSONString  = "{";
		
		for(Objective o : gameObjectives)
		{
			JSONString += "\""+o.getName()+"\":{"+
					"\"TYPE\":\""+o.getGatewayType()+"\","+
					"\"SPOTCONQUERED\":"+p.getConqueredGateway(world, o.getGatewayType())+","+
					"\"SPOTREQUIRED\":"+o.getRequiredGateways()+
					"},";
		}
		
		JSONString = JSONString.substring(0, JSONString.length()-1);
		JSONString += "}";

		SFSObject reback = SFSObject.newFromJsonData(JSONString);
		
		trace("Sending Objectives info");
		
		send("getObjectives", reback, sender);
	}
	
}
