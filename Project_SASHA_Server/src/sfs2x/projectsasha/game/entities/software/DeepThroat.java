package sfs2x.projectsasha.game.entities.software;

import sfs2x.projectsasha.game.GameConsts;
import sfs2x.projectsasha.game.entities.gateways.Gateway;
import sfs2x.projectsasha.game.ia.Police;


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
	public void runTriggeredAction(Gateway from, Gateway to){}

	public void runTriggeredAction(Gateway from, Gateway to, Police police)
	{
		police.teleportPolice(from);
		to.uninstallSoftware(this.type, to.getOwner());
	}

}
