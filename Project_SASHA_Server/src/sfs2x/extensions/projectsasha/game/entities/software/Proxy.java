package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;

public class Proxy extends Software
{
	
	
	public Proxy() 
	{
		super(GameConsts.PROXY_NAME, 1);
		setCumulative(GameConsts.PROXY_CUMULATIVE);
		setType(GameConsts.PROXY);
		setDescription(GameConsts.PROXY_DESCRIPTION);
		setCost(GameConsts.PROXY_COST,1);
	}
	
	
	public Proxy(int version) 
	{
		super(GameConsts.PROXY_NAME, version);
		setCumulative(GameConsts.PROXY_CUMULATIVE);
		setType(GameConsts.PROXY);
		setDescription(GameConsts.PROXY_DESCRIPTION);
		setCost(GameConsts.PROXY_COST,version);
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.PROXY_MAX_LEVEL)
			this.version += 1;
		else
			return;
	}	
	
	public int getRange()
	{
		return this.getVersion()<3?GameConsts.PROXY_LEV_1_2_MAX_HOPS:GameConsts.PROXY_LEV_3_MAX_HOPS;
	}
}

