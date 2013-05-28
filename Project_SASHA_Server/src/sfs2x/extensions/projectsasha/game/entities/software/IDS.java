package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


public class IDS extends Software
{
	
	
	public IDS() 
	{
		super(GameConsts.IDS_NAME, 1);
		setCumulative(GameConsts.IDS_CUMULATIVE);
		setType(GameConsts.IDS);
	}
	
	
	public IDS(int version) 
	{
		super(GameConsts.IDS_NAME, version);
		setCumulative(GameConsts.IDS_CUMULATIVE);
		setType(GameConsts.IDS);
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.IDS_MAX_LEVEL)
		{
			if(GameConsts.DEBUG)
				System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			if(GameConsts.DEBUG)
				System.out.println(this.name+" is already at maximum level (V"+GameConsts.IDS_MAX_LEVEL+")");
		}
	}	

	public int getDetection(int version)
	{
		return GameConsts.IDS_DETECTION - (25 * this.version);
	}
	
	@Override
	public String toString(){
		String toString = this.getName() + " V"+this.getVersion();
		toString += " - D:"+this.getDefenceLevel()+" A:"+ this.getAttackLevel()+"\n * \t\t";
		return toString;
	}
}
