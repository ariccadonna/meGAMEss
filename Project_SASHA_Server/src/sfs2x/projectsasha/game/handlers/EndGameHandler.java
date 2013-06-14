package sfs2x.projectsasha.game.handlers;

import java.util.Iterator;
import java.util.List;

import sfs2x.projectsasha.game.utils.RoomHelper;
import sfs2x.projectsasha.game.entities.GameWorld;
import sfs2x.projectsasha.game.entities.Player;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSArray;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class EndGameHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){
		
		GameWorld world = RoomHelper.getWorld(this);
		List<User> ul = RoomHelper.getCurrentRoom(this).getUserList();
		
		SFSObject reback = SFSObject.newInstance(); 
		
		Iterator<User> itr = ul.iterator();
		int i = 0;
		while (itr.hasNext())
		{
			User key = itr.next();
			boolean victorious = false;
			Player p = RoomHelper.getPlayer(this, key.getName());
			int conqueredGateway = p.getConqueredGateway(world);
			int money = p.getMoney();
			if(key == sender)
				victorious = true;
			SFSArray px = new SFSArray();
			px.addUtfString(p.getName());
			px.addInt(conqueredGateway);
			px.addInt(money);
			px.addBool(victorious);
			reback.putSFSArray("player"+i,px);
			i++;
		}

		send("endGameInfo", reback, RoomHelper.getCurrentRoom(this).getUserList());
	}
}
