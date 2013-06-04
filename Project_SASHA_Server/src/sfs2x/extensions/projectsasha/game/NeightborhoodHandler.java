package sfs2x.extensions.projectsasha.game;

import java.util.Iterator;
import java.util.Set;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class NeightborhoodHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){
		
		GameWorld world = RoomHelper.getWorld(this);
		
		int i=0;
		String str, JSONString;
		Gateway currentGW = null;
		Gateway[] neighboors = null;
		
		Set<String> set = world.gateways.keySet();
		Iterator<String> itr = set.iterator();
		
		//JSON composition
		JSONString  = "{";
		while (itr.hasNext()) {
			str = itr.next();
			neighboors = currentGW.getNeighboors(); 
			currentGW = world.gateways.get(str);
			JSONString += "\""+str+"\":{"+
						"\"STATE\":\""+str+"\","+
						"\"NB\": ["+
						"\""+currentGW.getNeighboors()+"\"";
					
			for (i=1; i!=currentGW.getNeighboors().length-1; i++)
			{
				JSONString +="\""+neighboors[i].getState()+"\",";
			}	
			JSONString +="\""+neighboors[currentGW.getNeighboors().length-1].getState()+"\"";
			JSONString +="]"+
			"},";
		}
		JSONString = JSONString.substring(0, JSONString.length()-1);
		JSONString+="}";
				
		SFSObject reback = SFSObject.newFromJsonData(JSONString);
		
		trace("Sending world setup info");
		
		send("getWorldSetup", reback, sender);
	}
}
