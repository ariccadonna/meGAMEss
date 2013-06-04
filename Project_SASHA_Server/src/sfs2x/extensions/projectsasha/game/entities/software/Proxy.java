package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;

public class Proxy extends Software
{
	
	
	public Proxy() 
	{
		super(GameConsts.PROXY_NAME, 1);
		setCumulative(GameConsts.PROXY_CUMULATIVE);
		setType(GameConsts.PROXY);
	}
	
	
	public Proxy(int version) 
	{
		super(GameConsts.PROXY_NAME, version);
		setCumulative(GameConsts.PROXY_CUMULATIVE);
		setType(GameConsts.PROXY);
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
		return GameConsts.PROXY_ATTACK_LEVEL * this.version;
	}
}

