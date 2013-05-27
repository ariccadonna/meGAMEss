package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


public class LogCleaner extends Software
{
	
	
	public LogCleaner() 
	{
		super(GameConsts.LOGCLEANER_NAME, 1);
		setCumulative(GameConsts.LOGCLEANER_CUMULATIVE);
		setType(GameConsts.LOGCLEANER);
	}
	
	
	public LogCleaner(int version) 
	{
		super(GameConsts.LOGCLEANER_NAME, version);
		setCumulative(GameConsts.LOGCLEANER_CUMULATIVE);
		setType(GameConsts.LOGCLEANER);
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.LOGCLEANER_MAX_LEVEL)
		{
			if(GameConsts.DEBUG)
				System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			if(GameConsts.DEBUG)
				System.out.println(this.name+" is already at maximum level (V"+GameConsts.LOGCLEANER_MAX_LEVEL+")");
		}
	}	
	
	@Override
	public String toString(){
		String toString = this.getName() + " V"+this.getVersion();
		toString += " - D:"+this.getDefenceLevel()+" A:"+ this.getAttackLevel()+"\n * \t\t";
		return toString;
	}
	
	
}
