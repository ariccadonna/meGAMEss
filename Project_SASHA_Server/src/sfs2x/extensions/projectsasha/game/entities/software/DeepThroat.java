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
	}
	
	
	public DeepThroat(int version) 
	{
		super(GameConsts.DEEPTHROAT_NAME, version);
		setCumulative(GameConsts.DEEPTHROAT_CUMULATIVE);
		setType(GameConsts.DEEPTHROAT);
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.DEEPTHROAT_MAX_LEVEL)
		{
			if(GameConsts.DEBUG)
				System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			if(GameConsts.DEBUG)
				System.out.println(this.name+" is already at maximum level (V"+GameConsts.DEEPTHROAT_MAX_LEVEL+")");
		}
	}	
	
	@Override
	public String toString(){
		return this.getName() + " V"+this.getVersion();
	}
	
	@Override
	public void runTriggeredAction(Gateway from, Gateway to)
	{
		//avvisare la polizia
	}
	
}
