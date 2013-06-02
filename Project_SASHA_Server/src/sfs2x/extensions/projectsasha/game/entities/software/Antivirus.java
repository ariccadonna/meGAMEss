package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;


public class Antivirus extends Software
{
	
	
	public Antivirus() 
	{
		super(GameConsts.ANTIVIRUS_NAME, 1);
		setCumulative(GameConsts.ANTIVIRUS_CUMULATIVE);
		setType(GameConsts.ANTIVIRUS);
	}
	
	
	public Antivirus(int version) 
	{
		super(GameConsts.ANTIVIRUS_NAME, version);
		setCumulative(GameConsts.ANTIVIRUS_CUMULATIVE);
		setType(GameConsts.ANTIVIRUS);
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.ANTIVIRUS_MAX_LEVEL)
			this.version += 1;
		else
			return;
	}	
	
	@Override
	public void runTriggeredAction(Gateway from, Gateway to)
	{
		Software v = to.getInstalledSoftware(GameConsts.VIRUS);
		if(this.getVersion() == v.getVersion())
		{
			from.uninstallSoftware(GameConsts.ANTIVIRUS, from.getOwner());
			to.uninstallSoftware(GameConsts.VIRUS, to.getOwner());
		}
		else if(this.getVersion()>v.getVersion())
			to.uninstallSoftware(GameConsts.VIRUS, to.getOwner());
		else
			from.uninstallSoftware(GameConsts.ANTIVIRUS, from.getOwner());
		
	}
	
}
