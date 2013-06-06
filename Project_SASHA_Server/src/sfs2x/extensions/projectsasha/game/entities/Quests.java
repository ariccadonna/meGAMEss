package sfs2x.extensions.projectsasha.game.entities;

import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;



public abstract class Quests
{
	protected String descritption;
	protected String type;
	protected String title;
	protected String goal;
	protected int reward;
	
	//COSTRUCTOR
  
	public Quests(String description, String title, String goal)
	{
		this.descritption = description;
		this.title = title;
		this.goal = goal;
	}

	//GETTERS
  
	public String getDescription()
	{
		return this.descritption;
	}

	public String getTitle()
	{
		return this.title;
	}
		

	public int getReward()
	{
		return this.reward;
	}
	
	public String getGoal()
	{
		return this.goal;
	}
	
	//SETTER
	
	public void setType(String type)
	{
		this.type = type;
	}

	
	







}
