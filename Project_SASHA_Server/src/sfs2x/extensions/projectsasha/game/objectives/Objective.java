package sfs2x.extensions.projectsasha.game.objectives;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;

public class Objective 
{
	protected String name;
	protected String description;
	protected int requiredGateways;
	protected String gatewayType;
	protected GameWorld world;
	
	
	//CONSTRUCTORS
	
	public Objective(GameWorld world, String name, String description, int requiredGateways, String gatewayType)
	{
		this.world = world;
		this.name = name;
		this.description = description;
		this.requiredGateways = requiredGateways;
		this.gatewayType = gatewayType;
	}
	
	public String getGatewayType()
	{
		return this.gatewayType;
	}
	
	public String getName()
	{
		return this.name;
	}
	
	public int getRequiredGateways()
	{
		return this.requiredGateways;
	}
	
	public boolean isObjectiveReached(Objective o, Player p)
	{
		int conqueredGateway = p.getConqueredGateway(world, o.getGatewayType());
		if(getRequiredGateways() == conqueredGateway)
			return true;
		else 
			return false;
	}
}
