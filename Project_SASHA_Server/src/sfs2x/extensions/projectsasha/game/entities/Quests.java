package sfs2x.extensions.projectsasha.game.entities;

import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;

public abstract class Quests
{
	protected String player;
	protected String descritption;
	protected String type;
	protected String title;
	protected String goal;
	
	//COSTRUCTOR
  
	public Quests(String player, String description, String title, String goal)
	{
		this.player = player;
		this.descritption = description;
		this.title = title;
		this.goal = goal;
	}

	//GETTERS
  
	public String getPlayer()
	{
		return this.player;
	}
	
	public String getDescription()
	{
		return this.descritption;
	}

	public String getTitle()
	{
		return this.title;
	}

	public String getGoal()
	{
		return this.goal;
	}
	
	public String getType()
	{
		return this.type;
	}
	
	//SETTER
	
	public void setType(String type)
	{
		this.type = type;
	}
	
	//METHOD
	
	public boolean achieve(Gateway target)
	{
		if(goal.equals(target.getName()) && (this.player.equals(target.getOwner().getName())))
			return true;
		return false;
	}

}






