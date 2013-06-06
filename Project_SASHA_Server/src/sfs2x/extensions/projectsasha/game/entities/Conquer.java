package sfs2x.extensions.projectsasha.game.entities;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;

public class Conquer extends Quests {
	
	public Conquer(String description, String title, String goal)
	{
		super(description,title,goal);
		setType(GameConsts.QUEST_TYPE1);
	}

	//SETTER
		
	public void setReward(Gateway g)
	{
		this.reward = g.getDefenceLevel()*GameConsts.QUEST1_REWARD;
	}
}
