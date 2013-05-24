package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


public class BruteForcer extends Software
{
	
	
	public BruteForcer() 
	{
		super(GameConsts.BRUTEFORCER_NAME, 1);
	}
	
	public BruteForcer(int version) 
	{
		super(GameConsts.BRUTEFORCER_NAME, version);
	}
	
	@Override
	public int getAttackLevel()
	{
		return GameConsts.BRUTEFORCER_ATTACK_LEVEL * version;
	}
	
	@Override
	public void upgrade()
	{
		if(this.version < GameConsts.BRUTEFORCER_MAX_LEVEL)
		{
			System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			System.out.println(this.name+" is already at maximum level (V"+GameConsts.BRUTEFORCER_MAX_LEVEL+")");
		}
	}	
	
	@Override
	public String toString(){
		String firewallToString = this.getName() + " V"+this.getVersion();
		firewallToString += " - D:"+this.getDefenceLevel()+" A:"+ this.getAttackLevel()+"\n * \t\t";
		return firewallToString;
	}
}
