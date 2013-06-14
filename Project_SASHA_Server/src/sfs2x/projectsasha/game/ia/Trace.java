package sfs2x.projectsasha.game.ia;

import sfs2x.projectsasha.game.entities.gateways.Gateway;

public class Trace
{
	private static int lastTraceID = 2000;
	
	public Gateway startingAttack;
	public int attackID, relevance;
	public String player;

	
	public Trace(Gateway startingAttack,  int relevance, String player)
	{
		this.startingAttack = startingAttack;
		this.attackID = getNewID();
		this.relevance = relevance;
		this.player = player;
	}
	
	private static synchronized int getNewID()
	{
		return lastTraceID++;
	}
}
