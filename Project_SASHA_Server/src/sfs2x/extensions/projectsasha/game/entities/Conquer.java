package sfs2x.extensions.projectsasha.game.entities;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.entities.software.Software;

public class Conquer extends Quests {
	
	public Conquer(String description, String title, Gateway goal)
	{
		super(description,title,goal.getName());
		setType(GameConsts.QUEST_TYPE1);
		setRewardMoney(goal);
		rewardItem = null;
	}

	public Conquer(String description, String title, String goal, Software s)
	{
		super(description,title,goal);
		setType(GameConsts.QUEST_TYPE1);
		setRewardItem(s);
		rewardMoney = 0;

	}
	//SETTER
		
	public void setRewardMoney(Gateway g)
	{
		this.rewardMoney = g.getDefenceLevel()*GameConsts.QUEST1_REWARDMONEY;
	}

	
}
