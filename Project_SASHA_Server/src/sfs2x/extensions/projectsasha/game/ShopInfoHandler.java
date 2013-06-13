package sfs2x.extensions.projectsasha.game;

import java.util.Iterator;

import sfs2x.extensions.projectsasha.game.entities.Shop;
import sfs2x.extensions.projectsasha.game.entities.software.Software;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class ShopInfoHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){
		
		Shop s = new Shop();
		String JSONString;
		Software currentSw = null;
		
		Iterator<Software> itr = s.getShop().iterator();
		//JSON composition
		JSONString  = "{";
		while (itr.hasNext()) {
			currentSw = itr.next();
			JSONString += "\""+currentSw.toString()+"\":{"+
						"\"PRICE\":"+currentSw.getCost()+","+
						"\"DESC\":\""+currentSw.getDescription()+"\""+
			"},";
		}
		JSONString = JSONString.substring(0, JSONString.length()-1);
		JSONString+="}";
				
		SFSObject reback = SFSObject.newFromJsonData(JSONString);
		trace(reback);
		trace(JSONString);
		trace("Sending shop setup info");
		
		send("shopInfo", reback, sender);
	}
}
