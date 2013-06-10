package sfs2x.extensions.projectsasha.game.ia;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Random;
import java.util.Set;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;


public class Police 
{
	private Random rand;
	private Trace currTrace;
	private Gateway prevGateway;
	private Gateway currGateway;
	private String[] gatewayStates;
	private GameWorld currentWorld;
	private boolean isFollowingTrace;
	private Set<Gateway> analyzedGateways;
	
   //CONSTRUCTOR
	public Police(GameWorld currentWorld)
	{
		this.rand = new Random();
		this.currTrace = null;
		this.prevGateway = null;
		this.currentWorld = currentWorld;
		this.gatewayStates = new String[this.currentWorld.gateways.size()];
		this.currentWorld.gateways.keySet().toArray(this.gatewayStates);
		this.isFollowingTrace = false;
		this.currGateway = currentWorld.gateways.get(this.gatewayStates[this.rand.nextInt(42)]);
		this.analyzedGateways = new HashSet<Gateway>();
	}
	
   //Main method called each Tick
	public void followNextTrace()
	{
		//INSERIRE QUI LE INFO CHE PARTONO DA CURRENT_GATEWAY PER LA POLIZIA CLIENT
		if(this.isFollowingTrace || this.findNewTrace())
		{			
			if(this.currGateway.hasStartedAttack(currTrace))
			{
				System.out.println(this.currGateway.getState()+" arrested"); //REMOVE ME
				this.confiscateGateway(currTrace.player);
			}
			else 
			{
				this.currGateway = this.getBiasedGateway();
				System.out.println("Following trace #"+(currTrace != null ? currTrace.attackID : "null") +" in gw: "+this.currGateway.getState()); //REMOVE ME
			}
		}
		else
		{	
			List<Gateway> neighboorGateways = new ArrayList<Gateway>();
			
			for(Gateway g: this.currGateway.getNeighboors())
			{
				if(g != this.prevGateway)
					neighboorGateways.add(g);
			}
			this.prevGateway = this.currGateway;
			this.currGateway = neighboorGateways.get(this.rand.nextInt(neighboorGateways.size()));
		
			System.out.println("WANDERING: Moving to "+this.currGateway.getState()+" ("+this.currGateway.getName()+")"); //REMOVE ME
		}	
	}

   //Tries to find a new trace in the current gateway, returns TRUE on success and assignes currTrace a new trace, FALSE if else
   private boolean findNewTrace()
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
		
		if(currMaxRelevance > 0)
		{
			this.isFollowingTrace = true;
			return this.isFollowingTrace;
		}
		else
		{
			this.isFollowingTrace = false;
			return this.isFollowingTrace;
		}
	}
   
   //Main Police decision making algorithm, tries to find the next gateway to analyze
	private Gateway getBiasedGateway()
	{
   	List <Gateway> gateways = new ArrayList<Gateway>();
      
      //Keeps track of the visited gateways to avoid loops
		analyzedGateways.add(this.currGateway);

		//Adding all the gateways which contain a relevance level equal to the one considered, except the previous gateway and the ones already considered
		for(Gateway g : currGateway.getNeighboors())
		{
			if(g != prevGateway && !this.analyzedGateways.contains(g))
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
		
		//Looking for the gateway which actually contains the right trace considering the attack id
		Gateway rightGateway = null;
		for(Gateway g : gateways)
		{			
			if(rightGateway != null)
				break;
			
			for(Trace t: g.getTraces())
			{
				if(t.attackID == currTrace.attackID)
				{
					rightGateway = g;
					break;
				}
			}
		}
		
		//The police has been diverted, it will start the research from a new random gateway
		if(rightGateway == null)
		{
			System.out.println("Wrong way! This is not the hacker, it is the hacked one (teleporting)!");
			return teleportPolice();
		}
      
      //The more traces with the same relevance are found, the more the police can get confused and can follow a trace with the same relevance but different id
      this.prevGateway = this.currGateway;    
      
		if(rand.nextFloat() <= 1/gateways.size())
			return rightGateway;
		else
			return gateways.get(rand.nextInt(gateways.size()));
	}
	
	//Teleports the police to a new gateway and resets every information about the previous investigation
	private Gateway teleportPolice()
	{
		this.analyzedGateways.clear();
		this.isFollowingTrace = false;
		this.currTrace = null;
		this.prevGateway = null;
		return currentWorld.gateways.get(this.gatewayStates[this.rand.nextInt(42)]);		
	}
	
	//Confiscates the gateway and arrest the player if he has 0 territories
	private void confiscateGateway(String player) 
	{
		//Clearing the traces of the attacks started on the confiscated gateways
		for(Gateway g : this.currentWorld.gateways.values())
			for(Trace t: this.currGateway.getStartedAttacks())
				g.getTraces().remove(t);
		
		this.currGateway.confiscate();
		this.currGateway = teleportPolice();
		//TODO decrement player's stateCount, if 0 kick him outta session
	} 
	
	public Gateway getCurrentgateway()
	{
		return this.currGateway;
	}
}
