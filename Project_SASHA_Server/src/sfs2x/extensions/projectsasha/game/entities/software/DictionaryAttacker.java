package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


public class DictionaryAttacker extends Software
{
	
	
	public DictionaryAttacker() 
	{
		super(GameConsts.DICTIONARY_NAME, 1);
		setCumulative(GameConsts.DICTIONARY_CUMULATIVE);
		setType(GameConsts.DICTIONARY);
	}
	
	
	public DictionaryAttacker(int version) 
	{
		super(GameConsts.DICTIONARY_NAME, version);
		setCumulative(GameConsts.DICTIONARY_CUMULATIVE);
		setType(GameConsts.DICTIONARY);
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.DICTIONARY_MAX_LEVEL)
		{
			if(GameConsts.DEBUG)
				System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			if(GameConsts.DEBUG)
				System.out.println(this.name+" is already at maximum level (V"+GameConsts.DICTIONARY_MAX_LEVEL+")");
		}
	}	
	
	@Override
	public String toString(){
		String toString = this.getName() + " V"+this.getVersion();
		toString += " - D:"+this.getDefenceLevel()+" A:"+ this.getAttackLevel()+"\n * \t\t";
		return toString;
	}
	
	
}
