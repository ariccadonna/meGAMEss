package sfs2x.extensions.projectsasha.game;

import java.util.Enumeration;
import java.util.Iterator;
import java.util.Set;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.entities.software.Software;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class WorldSetupHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){
		
		GameWorld world = RoomHelper.getWorld(this);
		Player p = new Player(sender);
		//SFSObject reback = SFSObject.newInstance();
		String str, JSONString;
		Gateway currentGW = null;
		
		Set<String> set = world.gateways.keySet();
		Iterator<String> itr = set.iterator();
		
		//JSON composition
		JSONString  = "{";
		while (itr.hasNext()) {
			str = itr.next();
			currentGW = world.gateways.get(str);
			JSONString+="\"GATEWAY\":{";
			JSONString+="\"STATE\":\""+str+"\",";
			JSONString+="\"NAME\":\""+currentGW.getName()+"\",";

			if(currentGW.getOwner()!=null)
				JSONString+="\"OWNER\":\""+currentGW.getOwner().getName()+"\",";
			else
				JSONString+="\"OWNER\":\"none\",";
			
			JSONString+="\"ATK\":"+currentGW.getAttackLevel()+",";
			JSONString+="\"DEF\":"+currentGW.getDefenceLevel()+",";
			JSONString+="\"TYPE\":\""+currentGW.getClass().getSimpleName()+"\",";
			JSONString+="\"SW\": {";
			
			if(currentGW.getInstalledSoftware(0) != null)
				JSONString+="\"S0\":\""+currentGW.getInstalledSoftware(0)+"\",";
			else
				JSONString+="\"S0\":null,";
			if(currentGW.getInstalledSoftware(1) != null)
				JSONString+="\"S1\":\""+currentGW.getInstalledSoftware(1)+"\",";
			else
				JSONString+="\"S1\":null,";
			if(currentGW.getInstalledSoftware(2) != null)
				JSONString+="\"S2\":\""+currentGW.getInstalledSoftware(2)+"\"";
			else
				JSONString+="\"S2\":null";
			
			JSONString+="}";
			JSONString+="},";
			
		}
		JSONString = JSONString.substring(0, JSONString.length()-1);
		JSONString+="}";
				
		SFSObject reback = SFSObject.newFromJsonData(JSONString);
		trace(reback.getDump());
		trace("Sending world setup info");
		
		send("getWorldSetup", reback, sender);
	}
}
