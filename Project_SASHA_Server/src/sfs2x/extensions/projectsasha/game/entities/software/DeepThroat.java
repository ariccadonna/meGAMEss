package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;


public class DeepThroat extends Software
{
	
	
	public DeepThroat() 
	{
		super(GameConsts.DEEPTHROAT_NAME, 1);
		setCumulative(GameConsts.DEEPTHROAT_CUMULATIVE);
		setType(GameConsts.DEEPTHROAT);
		setDescription(GameConsts.DEEPTHROAT_DESCRIPTION);
		setCost(GameConsts.DEEPTHROAT_COST,1);

	}
	
	
	public DeepThroat(int version) 
	{
		super(GameConsts.DEEPTHROAT_NAME, version);
		setCumulative(GameConsts.DEEPTHROAT_CUMULATIVE);
		setType(GameConsts.DEEPTHROAT);
		setDescription(GameConsts.DEEPTHROAT_DESCRIPTION);
		setCost(GameConsts.DEEPTHROAT_COST,version);

	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.DEEPTHROAT_MAX_LEVEL)
			this.version += 1;
		else
			return;
	}	
		
	@Override
	public void runTriggeredAction(Gateway from, Gateway to)
	{
		
	}
	
}
