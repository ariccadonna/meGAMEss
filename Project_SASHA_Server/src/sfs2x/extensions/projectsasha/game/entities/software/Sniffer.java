package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;


public class Sniffer extends Software
{
	
	
	public Sniffer() 
	{
		super(GameConsts.SNIFFER_NAME, 1);
		setCumulative(GameConsts.SNIFFER_CUMULATIVE);
		setType(GameConsts.SNIFFER);
	}
	
	
	public Sniffer(int version) 
	{
		super(GameConsts.SNIFFER_NAME, version);
		setCumulative(GameConsts.SNIFFER_CUMULATIVE);
		setType(GameConsts.SNIFFER);
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.SNIFFER_MAX_LEVEL)
			this.version += 1;
		else
			return;
	}	
	
	public int getDetectedItems()
	{
		return this.version;
	}
	
	@Override
	public void runTriggeredAction(Gateway from, Gateway to)
	{
		for(int i=0; i<this.getVersion();i++)
		{
			// mostrare oggetto nemico
		}
			
	}
}
