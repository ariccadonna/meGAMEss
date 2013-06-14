package sfs2x.projectsasha.game.entities.software;

import sfs2x.projectsasha.game.GameConsts;


public class DictionaryAttacker extends Software
{
	
	
	public DictionaryAttacker() 
	{
		super(GameConsts.DICTIONARY_NAME, 1);
		setCumulative(GameConsts.DICTIONARY_CUMULATIVE);
		setType(GameConsts.DICTIONARY);
		setDescription(GameConsts.DICTIONARY_DESCRIPTION);
		setCost(GameConsts.DICTIONARY_COST,1);

	}
	
	
	public DictionaryAttacker(int version) 
	{
		super(GameConsts.DICTIONARY_NAME, version);
		setCumulative(GameConsts.DICTIONARY_CUMULATIVE);
		setType(GameConsts.DICTIONARY);
		setDescription(GameConsts.DICTIONARY_DESCRIPTION);
		setCost(GameConsts.DICTIONARY_COST,version);
	}
	
	@Override
	public int getAttackLevel()
	{
		return GameConsts.DICTIONARY_ATTACK_LEVEL * version;
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.DICTIONARY_MAX_LEVEL)
			this.version += 1;
		else
			return;
	}	
	
	
}
