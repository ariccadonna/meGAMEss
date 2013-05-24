package sfs2x.extensions.projectsasha.game.ia;

import sfs2x.extensions.projectsasha.game.entities.software.*;

public class Test 
{
	public static void main(String args[])
	{
		Software s = new Firewall(1);
		
		System.out.println("defence: "+s.getDefenceLevel());
	}
}
