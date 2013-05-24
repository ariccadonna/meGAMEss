package sfs2x.extensions.projectsasha.game.ia;

import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;

public class Trace
{
	public Gateway startingAttack;
	public int attackID, relevance;
	public String player;

	
	public Trace(Gateway startingAttack, int attackID, int relevance, String player)
	{
		this.startingAttack = startingAttack;
		this.attackID = attackID;
		this.relevance = relevance;
		this.player = player;
	}
}
