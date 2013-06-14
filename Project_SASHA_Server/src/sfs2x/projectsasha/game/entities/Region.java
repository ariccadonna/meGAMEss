package sfs2x.projectsasha.game.entities;

import sfs2x.projectsasha.game.entities.gateways.Gateway;

public class Region 
{
	public Gateway[] gateways;
	public String name;

	public Region(String name, Gateway[] gateways)
	{
		this.name = name;
		this.gateways = gateways;
	}
	
	public String getRegionName()
	{
		return this.name;
	}
}
