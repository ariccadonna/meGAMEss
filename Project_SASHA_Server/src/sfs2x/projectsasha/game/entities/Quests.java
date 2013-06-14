package sfs2x.projectsasha.game.entities;

import sfs2x.projectsasha.game.entities.software.Software;



public abstract class Quests
{
	protected String descritption;
	protected String type;
	protected String title;
	protected String goal;
	protected int rewardMoney;
	protected Software rewardItem; 
	
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
		

	public int getRewardMoney()
	{
		return this.rewardMoney;
	}
	
	public Software getRewardItem()
	{
		return this.rewardItem;
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

	public void setRewardItem(Software s)
	{
		this.rewardItem = s;
		s.setLock(true);
	}
	







}
