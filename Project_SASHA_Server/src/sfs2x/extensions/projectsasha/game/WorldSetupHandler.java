package sfs2x.extensions.projectsasha.game;

import java.util.Iterator;
import java.util.List;
import java.util.Set;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class WorldSetupHandler extends BaseClientRequestHandler
{
	public void handleClientRequest(User sender, ISFSObject params)
	{
		
		GameWorld world = RoomHelper.getWorld(this);
		
		List<User> userList = RoomHelper.getCurrentRoom(this).getUserList();
		int i=0;
		for(User u : userList)
		{
			Player p = new Player(u);
			RoomHelper.putPlayer(this, p);
			world.gateways.get(GameConsts.STARTING_SPOTS[i]).setOwner(p);	
			i++;
		}
		
		
		String str, JSONString;
		Gateway currentGW = null;
		
		Set<String> set = world.gateways.keySet();
		Iterator<String> itr = set.iterator();
		
		//JSON composition
		JSONString  = "{";
		while (itr.hasNext())
		{
			str = itr.next();
			currentGW = world.gateways.get(str);
			String GWOwner = currentGW.getOwner()!=null?currentGW.getOwner().getName():"Neutral";
			JSONString += "\""+str+"\":{"+
						"\"STATE\":\""+str+"\","+
						"\"X\":"+currentGW.getX()+","+
						"\"Y\":"+currentGW.getY()+","+
						"\"NAME\":\""+currentGW.getName()+"\","+
						"\"OWNER\":\""+GWOwner+"\","+
						"\"ATK\":"+currentGW.getAttackLevel()+","+
						"\"DEF\":"+currentGW.getDefenceLevel()+","+
						"\"TYPE\":\""+currentGW.getClass().getSimpleName()+"\","+
						"\"SW\": ["+
						"\""+currentGW.getInstalledSoftware(0)+"\","+
						"\""+currentGW.getInstalledSoftware(1)+"\","+
						"\""+currentGW.getInstalledSoftware(2)+"\""+
						"]"+
						"},";
			
		}
		JSONString = JSONString.substring(0, JSONString.length()-1);
		JSONString+="}";
				
		SFSObject reback = SFSObject.newFromJsonData(JSONString);
		
		trace("Sending world setup info");
		
		send("getWorldSetup", reback, sender);
	}
}
