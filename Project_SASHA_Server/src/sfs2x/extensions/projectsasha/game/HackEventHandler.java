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
		boolean neutralize = params.getBool("neutralize");
		boolean success = false;
		
		if(to.getOwner()!=null)
		{
			if(neutralize == true)
			{
				success = this.hack(world, from, to);
				trace("Hack requesto from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
				
				if(success == true)
				{
					// to è da neutralizzare per 60 secondi
				}
			}
			else	//il gateway deve essere conquistato
			{	
				int soglia = 30;
				success = this.hack(world, from, to, soglia);
				trace("Hack requesto from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
			}
				
		}
		else
		{
			time = this.hackTime(world, from, to);
			for(int i=time;i>=0;i--);
			success = this.hack(world, from, to);
			trace("Hack requesto from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
		}
		ISFSObject reback = SFSObject.newInstance();
		reback.putBool("success", success);
		send("hack", reback, sender);
		
	}
	
	public boolean hack(GameWorld world, Gateway from, Gateway to){
		return hack(world, from, to, 0);
	}
	
	public boolean hack(GameWorld world, Gateway from, Gateway to, int extraTime){
		boolean ret = false;
		int difference = this.difference(from, to);
		
		Software[] attackerSw = from.getInstalledSoftwares();
		Software[] defenderSw = to.getInstalledSoftwares();
		
		for(Software sw: attackerSw)
			if(sw!=null)
				switch(sw.getType())
				{
					case GameConsts.DICTIONARY:
						sw.runTriggeredAction(from, to);
						break;
					default:
						break;
				}
		
		for(Software sw: defenderSw)
			if(sw!=null)
				switch(sw.getType())
				{
					case GameConsts.IDS:
						if(to.getOwner()!=null)
							sw.runTriggeredAction(from, to);
						break;
					case GameConsts.VIRUS:
						sw.runTriggeredAction(from, to);
						break;
					case GameConsts.DEEPTHROAT:
						sw.runTriggeredAction(from, to);
					default:
						break;
				}
		
		if(difference > 0)
		{
			int waitTime = this.hackTime(world, from, to) + extraTime;
			long currentTime = System.currentTimeMillis();
			long freeTime = currentTime+(waitTime*1000);
			while(System.currentTimeMillis() != freeTime){/*busy wait*/}
			to.setOwner(from.getOwner());
			ret = true;
		}
		else
		{
			ret = false;
		}
		return ret;
	}
	
	public int difference(Gateway from, Gateway to)
	{
		int diff = to.getAttackLevel()-from.getDefenceLevel();
		return diff;
	}
	
	public int hackTime(GameWorld world, Gateway from, Gateway to)
	{
		int[] datogliere = {15,15,15,15,15,15,15,15,15,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,40,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,60,60,60,60,60,60,60,60,60,60,60,60,60,60,60,80,80,80,80,80,80,80,80,80,80};
		int diff,time,bonus;
		time = 120;
		diff = datogliere[this.difference(from, to)];
		bonus = 10*from.getOwner().getConqueredGateway(world, GameConsts.SCI_GATEWAY);
		time-= diff - bonus;
		return time;
	}
}
