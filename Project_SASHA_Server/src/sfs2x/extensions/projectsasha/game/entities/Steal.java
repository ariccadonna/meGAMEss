package sfs2x.extensions.projectsasha.game.entities;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;

public class Steal extends Quests {
	
	public Steal(String player, String description, String title, String goal)
	{
		super(player,description,title,goal);
		setType(GameConsts.QUEST_TYPE2);
	}

	//SETTER
		
	public void setReward(Gateway g)
	{
		this.reward = g.getDefenceLevel()*GameConsts.QUEST2_REWARD;
	}

	public String getEnemy(Gateway g)
	{
		return g.getOwner().getName();
	}
}
