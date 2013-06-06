package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


public class Firewall extends Software
{
	
	
	public Firewall() 
	{
		super(GameConsts.FIREWALL_NAME, 1);
		setCumulative(GameConsts.FIREWALL_CUMULATIVE);
		setType(GameConsts.FIREWALL);
		setDescription(GameConsts.FIREWALL_DESCRIPTION);
		setCost(GameConsts.FIREWALL_COST,1);
	}
	
	
	public Firewall(int version) 
	{
		super(GameConsts.FIREWALL_NAME, version);
		setCumulative(GameConsts.FIREWALL_CUMULATIVE);
		setType(GameConsts.FIREWALL);
		setDescription(GameConsts.FIREWALL_DESCRIPTION);
		setCost(GameConsts.FIREWALL_COST,version);
	}
	
	@Override
	public int getDefenceLevel()
	{
		return GameConsts.FIREWALL_DEFENCE_LEVEL * version;
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.FIREWALL_MAX_LEVEL)
			this.version += 1;
		else
			return;
	}	
	
	
}
