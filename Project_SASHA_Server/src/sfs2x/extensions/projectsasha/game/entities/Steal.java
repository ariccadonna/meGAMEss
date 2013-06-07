package sfs2x.extensions.projectsasha.game.entities;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.entities.software.Software;

public class Steal extends Quests {
	
	public Steal(String description, String title, Gateway goal)
	{
		super(description,title,goal.getName());
		setType(GameConsts.QUEST_TYPE2);
		setRewardMoney(goal);
		rewardItem = null;
	}
	
	public Steal(String description, String title, String goal, Software s)
	{
		super(description,title,goal);
		setType(GameConsts.QUEST_TYPE2);
		setRewardItem(s);
		rewardMoney = 0;
	}
	
	//SETTER
		
	public void setRewardMoney(Gateway g)
	{
		this.rewardMoney = g.getDefenceLevel()*GameConsts.QUEST2_REWARDMONEY;
	}

	public String getEnemy(Gateway g)
	{
		return g.getOwner().getName();
	}
}
