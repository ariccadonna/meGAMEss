package sfs2x.extensions.projectsasha.game;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.gateways.*;
import sfs2x.extensions.projectsasha.game.entities.software.*;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;
import sfs2x.extensions.projectsasha.game.utils.TimerHelper;


import com.smartfoxserver.v2.entities.User;

import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class HackEventHandler extends BaseClientRequestHandler{
	public void handleClientRequest(User sender, ISFSObject params){

		GameWorld world = RoomHelper.getWorld(this);
		Player p = new Player(sender);
		int time;
		
		
		Gateway from = world.gateways.get(params.getUtfString("gatewayFrom"));
		Gateway to = world.gateways.get(params.getUtfString("gatewayTo"));
		boolean success;
		if(to.getOwner()!=null)
		{
			if(/*gateway da neutralizzare*/)
			{
				time = from.hackTime(world, to);
				for(int i=time;i>=0;i--);
				success = Gateway.hack(from, to);
				trace("Hack requesto from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
				if(success)
					// to è da neutralizzare per 60 secondi
			}
			else	//il gateway vuole essere conquistato
			{	
				int soglia = 30;
				new TimerHelper(soglia,from,to);
				time = from.hackTime(world, to) + soglia;
				for(int i=time;i>=0;i--);
				success = Gateway.hack(from, to);
				trace("Hack requesto from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
			}
				
		}
		else
		{
		time = from.hackTime(world, to);
		for(int i=time;i>=0;i--);
		boolean success = Gateway.hack(from, to);
		trace("Hack requesto from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
		}
		ISFSObject reback = SFSObject.newInstance();
		reback.putBool("success", success);
		send("hack", reback, sender);
		
	}
}
