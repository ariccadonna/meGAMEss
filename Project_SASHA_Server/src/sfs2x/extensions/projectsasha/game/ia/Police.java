package sfs2x.extensions.projectsasha.game.ia;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;

public class Police 
{
	private Trace currTrace;
	private Gateway prevGateway;
	private Gateway currGateway;
	private Random rand;
	
	public Police()
	{
		this.rand = new Random();
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

}
