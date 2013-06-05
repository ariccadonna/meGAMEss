package sfs2x.extensions.projectsasha.game.entities;

import sfs2x.extensions.projectsasha.game.GameConsts;

public class Conquista extends Quests {
	
	public Conquista(String player, String description, String title, String goal)
	{
		super(player,description,title,goal);
		setType(GameConsts.QUEST_TYPE1);
	}

	
}
