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
	private Random rand;					//A random generator for random choices
	private Trace currTrace;				//The trace the police is currently following
	private Gateway prevGateway;			//The gateway just visited, used to avoid the police getting on an already visited gateway
	private Gateway currGateway;			//The gateway currently being analyzed
	private String[] gatewayStates;			//An array with the names of the 42 states
	private GameWorld currentWorld;			//A reference to the current gameworld of the match
	private boolean isFollowingTrace;		//Flag to see if the police is wandering or following a trace
	private Set<Gateway> analyzedGateways;	//A set of visited gateways during a police chasing to avoid loops
	
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
	
	
	//Current gateway getter, useful to notify the police position to the players
	public synchronized Gateway getCurrentgateway()
	{
		return this.currGateway;
	}
	
   //Main method called each Tick
	public void followNextTrace()
	{
		if(this.isFollowingTrace || this.findNewTrace())	//The police is following a trace or tries to get one from the current gateway
		{			
			if(this.currGateway.hasStartedAttack(currTrace))	//If the trace started from this gateway the police will confiscate it
			{
				this.confiscateGateway(currTrace.player);
				System.out.println("CONFISCATED: " + this.currGateway.getState()); //REMOVE ME
			}
			else	//The police is following a trace and moves to the next gateway following the attack ID or the relevance
			{
				this.currGateway = this.getBiasedGateway();
				System.out.println("TRACE FOLLOWING: id#"+(currTrace != null ? currTrace.attackID : "null") +" in gw: "+this.currGateway.getState()); //REMOVE ME
			}
		}
		else	//The police has no trace and is wandering trying to find a node that has a trace with the relevance >= the threshold
		{	
			this.currGateway = this.wander();			
			System.out.println("WANDERING: Moving to "+this.currGateway.getState()+" ("+this.currGateway.getName()+")"); //REMOVE ME
		}	
	}

   //Tries to find a new trace in the current gateway, returns TRUE on success and assignes currTrace a new trace, FALSE if else
   private boolean findNewTrace()
	{
		int currMaxRelevance = 0;

		for (Trace t: this.currGateway.getTraces())	//Tries to find a trace to follow in the current gateway
		{
			if(t.relevance >= GameConsts.DEFAULT_POLICE_RELEVANCE_THRS && t.relevance > currMaxRelevance)
			{
				currMaxRelevance = t.relevance;
				this.currTrace = t;
			}
		}
		
		if(currMaxRelevance > 0)	//If a trace has been found
		{
			this.isFollowingTrace = true;
			return this.isFollowingTrace;
		}
		else	//If no traces are found
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
			if(g != prevGateway && !this.analyzedGateways.contains(g))	//Finds all the neighboors that have a trace relevance equals to the one being followed
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
			
			for(Trace t: g.getTraces())		//Tries to find the right gateway to follow, the one with the same attack ID
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
			System.out.println("TRACE LOST: Wrong way! police got diverted or reached the attacked gateway");
			return teleportPolice();
		}
      
      //The more traces with the same relevance are found, the more the police can get confused and can follow a trace with the same relevance but different id
      this.prevGateway = this.currGateway;    
      
		if(rand.nextFloat() <= 1/gateways.size())				//Follow by ID
			return rightGateway;
		else
			return gateways.get(rand.nextInt(gateways.size()));	//Follow by relevance
	}
	
	//Wandering algorithm, goes to a neighboor excluding the previous one
	private Gateway wander()
	{
		List<Gateway> neighboorGateways = new ArrayList<Gateway>();
		
		for(Gateway g: this.currGateway.getNeighboors())	//Populates a list of the neighboors excluding the previous one
		{
			if(g != this.prevGateway)
				neighboorGateways.add(g);
		}
		this.prevGateway = this.currGateway;
		return neighboorGateways.get(this.rand.nextInt(neighboorGateways.size()));	//Choose a random neighboor gateway to go
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

	//Teleports the police to a new specified gateway and tries to set a new trace against the owner of the gateway
	public Gateway teleportPolice(Gateway newDestination)
	{		
		this.analyzedGateways.clear();
		this.isFollowingTrace = false;
		this.currTrace = null;
		
		for(Trace t : newDestination.getTraces())
			if(t.relevance >= GameConsts.DEFAULT_POLICE_RELEVANCE_THRS && t.player.equals(newDestination.getOwner().getName()))
			{
				this.currTrace = t;
				this.isFollowingTrace = true;
				break;
			}
		
		this.prevGateway = null;
		return newDestination;		
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
}
