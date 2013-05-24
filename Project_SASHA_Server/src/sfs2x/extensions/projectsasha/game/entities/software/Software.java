package sfs2x.extensions.projectsasha.game.entities.software;

public abstract class Software 
{
	protected int version;	//Software version
	protected String name;	//Software name (ex: firewall, proxy...)
	protected int slot;
	
	
	//CONSTRUCTORS
	
	public Software(String name, int version)
	{
		this.name = name;
		this.version = version;
	}
	
	
	//GETTERS
	
	public int getDefenceLevel()	//The defensive level (if not overridden returns 0)
	{
		return 0;
	}
	
	public int getAttackLevel()		//The offensive level (if not overridden returns 0)
	{
		return 0;
	}
	
	public int getVersion()
	{
		return version;
	}
	
	public String getName()
	{
		return name;
	}
	
	public int getSlot()
	{
		return slot;
	}
	
	
	//SETTERS
	
	public void setVersion(int version)
	{
		
		this.version = version;
	}	
	
	public void upgrade()
	{
		System.out.println("Upgrading "+this.name+" from V"+this.version+" to V"+(this.version+1));
		this.version += 1;
	}	
	
	public void downgrade()
	{
		if(this.version == 1)
			return;
		
		System.out.println("Downgrading "+this.name+" from V"+this.version+" to V"+(this.version-1));
		this.version -= 1;
	}	
	
	public void setSlot(int theSlot)
	{
		this.slot = theSlot;
	}
}
