package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


public class Firewall extends Software
{
	
	
	public Firewall() 
	{
		super(GameConsts.FIREWALL_NAME, 1);
		setCumulative(GameConsts.FIREWALL_CUMULATIVE);
		setType(GameConsts.FIREWALL);
	}
	
	
	public Firewall(int version) 
	{
		super(GameConsts.FIREWALL_NAME, version);
		setCumulative(GameConsts.FIREWALL_CUMULATIVE);
		setType(GameConsts.FIREWALL);
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
		{
			if(GameConsts.DEBUG)
				System.out.println("Upgrading "+this.name+" in slot "+this.slot+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			if(GameConsts.DEBUG)
				System.out.println(this.name+" is already at maximum level (V"+GameConsts.FIREWALL_MAX_LEVEL+")");
		}
	}	
	
	@Override
	public String toString(){
		return this.getName() + " V"+this.getVersion();
	}
	
	
}
