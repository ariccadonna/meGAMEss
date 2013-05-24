package sfs2x.extensions.projectsasha.game.entities;

import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.entities.gateways.*;

public class Region 
{
	public Gateway[] gateways;
	public String name;

	public Region(String name, Gateway[] gateways)
	{
		this.name = name;
		this.gateways = gateways;
	}
}
