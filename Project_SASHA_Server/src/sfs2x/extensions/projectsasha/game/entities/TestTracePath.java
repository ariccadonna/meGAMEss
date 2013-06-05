package sfs2x.extensions.projectsasha.game.entities;

import java.util.List;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.User;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;

public class TestTracePath {
public long startingTime;
	
	/****************************************/
	public static GameWorld gw = new GameWorld(1);
	public static Player p1 = new Player(new User("MastErAldo"));
	public static Player p2 = new Player(new User("SpiaggeFredde"));
	public static Gateway g1 = gw.gateways.get("peru");
	public static Gateway g2 = gw.gateways.get("quebec");
	public static Gateway g3 = gw.gateways.get("ukraine");
	public static Gateway g4 = gw.gateways.get("western australia");
	public static List<Gateway> mypath;
	
	public static void main(String args[])
	{
		g1.setCostsSoFar("MastErAldo", 0);
		g1.setParentForPath("MastErAldo", null);
		g2.setCostsSoFar("MastErAldo", 0);
		g2.setParentForPath("MastErAldo", null);
		g3.setCostsSoFar("MastErAldo", 0);
		g3.setParentForPath("MastErAldo", null);
		g4.setCostsSoFar("MastErAldo", 0);
		g4.setParentForPath("MastErAldo", null);
		g1.setOwner(p1);
		g4.setOwner(p2);
		g2.setOwner(p1);
		g3.setOwner(p1);
		
		
		g1.installSoftware(GameConsts.PROXY, p1);
		g2.installSoftware(GameConsts.PROXY, p1);
		g3.installSoftware(GameConsts.PROXY, p1);
		
		mypath = g1.tracePath(g4, 5);
		//g1.setTrace(mypath, 5);
		
		System.out.println("PRIMA ESECUZIONE: ");
		if(mypath != null)
		{
			for(Gateway gw : mypath)
			{
				System.out.println(gw.getState());
			}
		}
		else System.out.println("PERCORSO NON ESISTENTE NELLA PRIMA ITERAZIONE");
		
		//g1.upgradeSoftware(GameConsts.PROXY, p1);
		//g2.upgradeSoftware(GameConsts.PROXY, p1);
		//g3.upgradeSoftware(GameConsts.PROXY, p1);
		
		mypath = g1.tracePath(g2, 5);

		g1.setTrace(mypath, 5);
		
		mypath = g2.tracePath(g3, 5);

		g2.setTrace(mypath, 5);

		mypath = g3.tracePath(g4, 5);

		g3.setTrace(mypath, 5);
		
		
		g1.upgradeSoftware(GameConsts.PROXY, p1);
		
		mypath = g1.tracePath(g4, 5);

		System.out.println("dkuvhssiduvhsou");

		g1.setTrace(mypath, 5);
		
		System.out.println("SECONDA ESECUZIONE: ");
		if(mypath != null)
		{
			for(Gateway gw : mypath)
			{
				System.out.println(gw.getState());
			}
		}
		else System.out.println("PERCORSO NON ESISTENTE NELLA SECONDA ITERAZIONE");
	}
	
	
	
	
	
	/****************************************/

}
