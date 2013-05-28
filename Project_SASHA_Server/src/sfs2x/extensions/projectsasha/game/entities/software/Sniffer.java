package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


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
		{
			if(GameConsts.DEBUG)
				System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			if(GameConsts.DEBUG)
				System.out.println(this.name+" is already at maximum level (V"+GameConsts.SNIFFER_MAX_LEVEL+")");
		}
	}	
	
	public int getItem()
	{
		return this.version;
	}
	
	@Override
	public String toString(){
		String toString = this.getName() + " V"+this.getVersion();
		toString += " - D:"+this.getDefenceLevel()+" A:"+ this.getAttackLevel()+"\n * \t\t";
		return toString;
	}	
}
