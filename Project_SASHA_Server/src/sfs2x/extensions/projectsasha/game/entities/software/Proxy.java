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
		{
			if(GameConsts.DEBUG)
				System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			if(GameConsts.DEBUG)
				System.out.println(this.name+" is already at maximum level (V"+GameConsts.PROXY_MAX_LEVEL+")");
		}
	}	
	
	public int getRange()
	{
		return GameConsts.PROXY_ATTACK_LEVEL * this.version;
	}
	
	@Override
	public String toString(){
		String toString = this.getName() + " V"+this.getVersion();
		toString += " - D:"+this.getDefenceLevel()+" A:"+ this.getAttackLevel()+"\n * \t\t";
		return toString;
	}	
}
