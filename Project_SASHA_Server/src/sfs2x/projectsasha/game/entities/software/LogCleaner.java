package sfs2x.projectsasha.game.entities.software;

import sfs2x.projectsasha.game.GameConsts;


public class LogCleaner extends Software
{
	
	
	public LogCleaner() 
	{
		super(GameConsts.LOGCLEANER_NAME, 1);
		setCumulative(GameConsts.LOGCLEANER_CUMULATIVE);
		setType(GameConsts.LOGCLEANER);
		setDescription(GameConsts.LOGCLEANER_DESCRIPTION);
		setCost(GameConsts.LOGCLEANER_COST,1);
	}
	
	
	public LogCleaner(int version) 
	{
		super(GameConsts.LOGCLEANER_NAME, version);
		setCumulative(GameConsts.LOGCLEANER_CUMULATIVE);
		setType(GameConsts.LOGCLEANER);
		setDescription(GameConsts.LOGCLEANER_DESCRIPTION);
		setCost(GameConsts.LOGCLEANER_COST,version);
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.LOGCLEANER_MAX_LEVEL)
			this.version += 1;
		else
			return;
	}	
	
}
