package sfs2x.extensions.projectsasha.game.ia;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Random;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;

public class Police 
{
	private Trace currTrace;
	private Gateway prevGateway;
	private Gateway currGateway;
	private Random rand;
	private String[] gatewayStates;
	private GameWorld currentWorld;
	
	public Police(GameWorld currentWorld)
	{
		this.rand = new Random();
		this.currentWorld = currentWorld;
		this.gatewayStates = new String[this.currentWorld.gateways.size()];
		this.currentWorld.gateways.keySet().toArray(this.gatewayStates);
		this.currGateway = currentWorld.gateways.get(this.gatewayStates[this.rand.nextInt(42)]);
	}
	
	
	public Gateway getBiasedGateway()
	{
		List <Gateway> gateways = new ArrayList<Gateway>();

		for(Gateway g : currGateway.getNeighboors())
		{
			if(prevGateway == null || !g.equals(prevGateway))
			{
				for(Trace t: g.getTraces())
				{
					if(t.relevance == currTrace.relevance)
					{
						gateways.add(g);
						break;
					}
				}
			}
		}
		if(gateways.size() == 0)
			return null;

		Gateway rightGateway = null;
		
		for(Gateway g : gateways)
		{
			for(Trace t: g.getTraces())
			{
				if(t.attackID == currTrace.attackID)
				{
					rightGateway = g;
					break;
				}
			}
		}
		if(rand.nextFloat() <= 1/gateways.size())
		{
			return rightGateway;
		}
		else
		{
			gateways.remove(rightGateway);
			return gateways.get((int)(rand.nextFloat()*gateways.size()));
		}
	}
	
	public boolean findNewTrace()
	{
		int currMaxRelevance = 0;

		for (Trace t: this.currGateway.getTraces())
		{
			if(t.relevance >= GameConsts.DEFAULT_POLICE_RELEVANCE_THRS && t.relevance > currMaxRelevance)
			{
				currMaxRelevance = t.relevance;
				this.currTrace = t;
			}
		}
		
		return currMaxRelevance > 0 ? true : false;
		
	}
	
	public void followNextTrace()
	{
		if(this.currTrace != null || this.findNewTrace())
		{
			this.prevGateway = this.currGateway;
			this.currGateway = this.getBiasedGateway();
			System.out.println(this.currGateway != null ? "Following trace in gw: "+this.currGateway.getState() : "No trace found"); //REMOVE ME
			
			if(this.currGateway != null && this.currGateway.hasStartedAttack(currTrace.attackID))
			{
					System.out.println(this.currGateway.getState()+" arrested"); //REMOVE ME
					this.arrestPlayer(currTrace.player);
					this.currTrace = null;
					this.prevGateway = null;
					this.currGateway = currentWorld.gateways.get(this.gatewayStates[this.rand.nextInt(42)]);
				
			}
			else if(this.currGateway == null)
			{
				System.out.println("Trace lost"); //REMOVE ME
				this.currTrace = null;
			}
		}
		else
		{	
			Gateway[] currGateways = this.currGateway.getNeighboors();
			List<Gateway> neighboorGateways = new ArrayList<Gateway>();
			
			for(Gateway g: currGateways)
			{
				if(g != this.prevGateway)
					neighboorGateways.add(g);
			}
			this.currTrace = null;
			this.prevGateway = this.currGateway;
			
			int neighboorsSize = neighboorGateways.size();
			this.currGateway = neighboorGateways.get(this.rand.nextInt(neighboorsSize));
			System.out.println("WANDERING: Moving to "+this.currGateway.getState()); //REMOVE ME
		}
		
	}


	private void arrestPlayer(String player) 
	{
		// TODO Auto-generated method stub
		this.currGateway.confiscate();
		//TODO decrement player's stateCount, if 0 kick him outta session
	}
	

}
