package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;


public class Firewall extends Software
{
	
	
	public Firewall() 
	{
		super(GameConsts.FIREWALL_NAME, 1);
	}
	
	
	public Firewall(int version) 
	{
		super(GameConsts.FIREWALL_NAME, version);
	}
	
	@Override
	public int getDefenceLevel()
	{
		return GameConsts.FIREWALL_DEFENCE_LEVEL * version;
	}
	
	@Override
	public void upgrade()
	{
		if (this==null)
			return;
		if(this.version < GameConsts.FIREWALL_MAX_LEVEL)
		{
			System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
			this.version += 1;
		}
		else
		{
			System.out.println(this.name+" is already at maximum level (V"+GameConsts.FIREWALL_MAX_LEVEL+")");
		}
	}	
	
	@Override
	public String toString(){
		String firewallToString = this.getName() + " V"+this.getVersion();
		firewallToString += " - D:"+this.getDefenceLevel()+" A:"+ this.getAttackLevel()+"\n * \t\t";
		return firewallToString;
	}
	
	
}
