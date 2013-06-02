package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


public class BruteForcer extends Software
{
	
	
	public BruteForcer() 
	{
		super(GameConsts.BRUTEFORCER_NAME, 1);
		setCumulative(GameConsts.BRUTEFORCER_CUMULATIVE);
		setType(GameConsts.BRUTEFORCER);
	}
	
	public BruteForcer(int version) 
	{
		super(GameConsts.BRUTEFORCER_NAME, version);
		setCumulative(GameConsts.BRUTEFORCER_CUMULATIVE);
		setType(GameConsts.BRUTEFORCER);
	}
	
	@Override
	public int getAttackLevel()
	{
		return GameConsts.BRUTEFORCER_ATTACK_LEVEL * version;
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.BRUTEFORCER_MAX_LEVEL)
			this.version += 1;
		else
			return;
	}	
}
