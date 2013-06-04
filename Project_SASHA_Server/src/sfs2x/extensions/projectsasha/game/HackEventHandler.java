package sfs2x.extensions.projectsasha.game;
import java.util.ArrayList;
import java.util.List;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;
import sfs2x.extensions.projectsasha.game.entities.gateways.*;
import sfs2x.extensions.projectsasha.game.entities.software.*;
import sfs2x.extensions.projectsasha.game.utils.RoomHelper;


import com.smartfoxserver.v2.entities.User;

import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class HackEventHandler extends BaseClientRequestHandler
{
	public void handleClientRequest(User sender, ISFSObject params)
	{
		boolean neutralize;
		boolean success = false;
		int attackRelevance;
		List<Gateway> hackingPath;
		GameWorld world = RoomHelper.getWorld(this);
		Player p = RoomHelper.getPlayer(this, sender.getName());
		
		
		Gateway from = world.gateways.get(params.getUtfString("gatewayFrom"));
		Gateway to = world.gateways.get(params.getUtfString("gatewayTo"));
		
		neutralize = params.getBool("neutralize");
		
		if(from.getOwner().canHack() == true)
		{
			if(to.getOwner()!=null && to.getOwner()!=p)
			{
				attackRelevance = this.getAttackRelevance(from, to);
				hackingPath = from.tracePath(to, attackRelevance);
				
				if(neutralize == true)
				{
					success = this.neutralize(world, from, to);
					trace("Neutralization request from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
					
					if(success == true)
					{
						// to è da neutralizzare per 60 secondi
					}
				}
				else	//il gateway deve essere conquistato
				{	
					success = this.hack(world, from, to, GameConsts.CONQUER_TIME_TRESHOLD);
					trace("Hack request from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
				}
				from.setTrace(hackingPath, attackRelevance);
			}
			else
			{
				success = this.hack(world, from, to);
				trace("Hack request from " + p.getUserName() + ": from " + from.getState()+" to " + to.getState() + ": " +  (success?"SUCCESS":"FAIL"));
			}
		}
		else
		{
			//hack is disabled, we should return an error
			trace("Hack request from " + p.getUserName() + ": FAILED since the hack is disabled for this player");
			success = false;
		}
		
		ISFSObject reback = SFSObject.newInstance();
		reback.putBool("success", success);
		send("hack", reback, sender);
		
	}
	
	public boolean hack(GameWorld world, Gateway from, Gateway to)
	{
		return hack(world, from, to, 0);
	}
	
	public boolean hack(GameWorld world, Gateway from, Gateway to, int extraTime)
	{
		boolean ret = false;
		long startTime, endTime;
		int difference = this.powerDifference(from, to);
		
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
			startTime = System.currentTimeMillis();
			endTime = startTime+(waitTime*1000);
			from.getOwner().setCanHack(false);//disable hack for the following seconds

			while(System.currentTimeMillis() != endTime)
			{
				/*BUSY WAIT*/
				//currentTick = System.currentTimeMillis()-starTime;
				//	if(currentTick%1000==0)
				//		send a countdown to the player for remaining hack time??
			}

			from.getOwner().setCanHack(true);
			to.setOwner(from.getOwner());
			
			ret = true;
		}
		else
		{
			startTime = System.currentTimeMillis();
			endTime = startTime+(GameConsts.FAILTIME*1000);
			from.getOwner().setCanHack(false); //disable hack for the following seconds
			
			while(System.currentTimeMillis() != endTime)
			{
				/*BUSY WAIT*/
				//currentTick = System.currentTimeMillis()-starTime;
				//	if(currentTick%1000==0)
				//		send a countdown to the player??
			}
			
			from.getOwner().setCanHack(true);
			ret = false;
		}
		return ret;
	}
	
	public boolean neutralize(GameWorld world, Gateway from, Gateway to)
	{
		boolean ret = false;
		long startTime, endTime;
		int difference = this.powerDifference(from, to);
		
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
			int waitTime = this.hackTime(world, from, to);
			startTime = System.currentTimeMillis();
			endTime = startTime+(waitTime*1000);
			from.getOwner().setCanHack(false);//disable hack for the following seconds

			while(System.currentTimeMillis() != endTime)
			{
				/*BUSY WAIT*/
				//currentTick = System.currentTimeMillis()-starTime;
				//	if(currentTick%1000==0)
				//		send a countdown to the player for remaining hack time??
			}

			from.getOwner().setCanHack(true);
			
			ret = true;
		}
		else
		{
			startTime = System.currentTimeMillis();
			endTime = startTime+(GameConsts.FAILTIME*1000);
			from.getOwner().setCanHack(false); //disable hack for the following seconds
			
			while(System.currentTimeMillis() != endTime)
			{
				/*BUSY WAIT*/
				//currentTick = System.currentTimeMillis()-starTime;
				//	if(currentTick%1000==0)
				//		send a countdown to the player??
			}
			
			from.getOwner().setCanHack(true);
			ret = false;
		}
		return ret;
	}
	
	public int powerDifference(Gateway from, Gateway to)
	{
		return from.getAttackLevel()-to.getDefenceLevel();
	}
	
	public int getAttackRelevance(Gateway from, Gateway to)
	{
		return ((from.getAttackLevel() + to.getDefenceLevel()) / 200) *10;
	}
	
	
	public int hackTime(GameWorld world, Gateway from, Gateway to)
	{
		int bonus;
		int diff = this.powerDifference(from, to)+
				GameConsts.MIL_BONUS_MULTIPLIER*from.getOwner().getConqueredGateway(world, GameConsts.MIL_GATEWAY)-
				GameConsts.GOV_BONUS_MULTIPLIER * to.getOwner().getConqueredGateway(world, GameConsts.GOV_GATEWAY);
		
		int[] timeToLeave = {
						15,15,15,15,15,15,15,15,15, 	// 1-9
						30,30,30,30,30,30,30,30,30,30,	//10-19
						30,30,30,30,30,30,30,30,30,30,	//20-29
						40,40,40,40,40,40,40,40,40,40,	//30-39
						40,40,40,40,40,40,40,40,40,40,	//40-49
						50,50,50,50,50,50,50,50,50,50,	//50-59
						50,50,50,50,50,50,50,50,50,50,	//60-69
						60,60,60,60,60,60,60,60,60,60,	//70-79
						60,60,60,60,60,					//80-84
						80,80,80,80,80,80,80,80,80,80	//85-94
						};
		
		bonus = GameConsts.SCI_BONUS_MULTIPLIER*from.getOwner().getConqueredGateway(world, GameConsts.SCI_GATEWAY);
		
		return 120 - timeToLeave[diff-1] - bonus;
	}
}
