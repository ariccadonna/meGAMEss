package sfs2x.extensions.projectsasha.game;

import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.software.Software;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSArray;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class PlayerInfoHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){
		
		Player p = RoomHelper.getPlayer(this, sender.getName());
		int money = p.getMoney();
		long time = RoomHelper.getTimer(this);
		
		ISFSObject reback = SFSObject.newInstance();
		reback.putUtfString("name", p.getName());
		reback.putInt("money", money);
		reback.putLong("time", time);
		
		Software[] inv = p.getInventory();
		SFSArray returnInventory = new SFSArray();
		
		for(int i=0; i<GameConsts.INVENTORY_SIZE; i++)
		{
			if(inv[i] == null)
				returnInventory.addUtfString("");
			else
				returnInventory.addUtfString(inv[i].toString());
		}
			
		
		reback.putSFSArray("inventory", returnInventory);
		
		send("playerInfo", reback, sender);
	}
}
