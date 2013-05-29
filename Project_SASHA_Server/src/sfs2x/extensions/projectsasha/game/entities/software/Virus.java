package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


public class Virus extends Software
{
	
	
	public Virus() 
	{
		super(GameConsts.VIRUS_NAME, 1);
		setCumulative(GameConsts.VIRUS_CUMULATIVE);
		setType(GameConsts.VIRUS);
	}
	
	
	public Virus(int version) 
	{
		super(GameConsts.VIRUS_NAME, version);
		setCumulative(GameConsts.VIRUS_CUMULATIVE);
		setType(GameConsts.VIRUS);
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.VIRUS_MAX_LEVEL)
		{
			if(GameConsts.DEBUG)
				System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			if(GameConsts.DEBUG)
				System.out.println(this.name+" is already at maximum level (V"+GameConsts.VIRUS_MAX_LEVEL+")");
		}
	}	
	
	@Override
	public String toString(){
		return this.getName() + " V"+this.getVersion();
	}
	
	
}
