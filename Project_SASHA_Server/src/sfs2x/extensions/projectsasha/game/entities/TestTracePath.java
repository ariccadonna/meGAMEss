package sfs2x.extensions.projectsasha.game.entities;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.List;

import sfs2x.extensions.projectsasha.game.GameConsts;
import sfs2x.extensions.projectsasha.game.User;
import sfs2x.extensions.projectsasha.game.entities.gateways.Gateway;
import sfs2x.extensions.projectsasha.game.entities.software.Proxy;
import sfs2x.extensions.projectsasha.game.entities.software.Software;
import sfs2x.extensions.projectsasha.game.ia.AIThread;

public class TestTracePath {
public long startingTime;
	
	/****************************************/
	public static GameWorld gw = new GameWorld(1);
	public static Player p1 = new Player(new User("MastErAldo"));
	public static Player p2 = new Player(new User("spiaggefredde"));
	public static Gateway g1 = gw.gateways.get("peru");
	public static Gateway g2 = gw.gateways.get("quebec");
	public static Gateway g3 = gw.gateways.get("ukraine");
	public static Gateway g4 = gw.gateways.get("western australia");
	public static Gateway g5 = gw.gateways.get("congo");
	public static Gateway g6 = gw.gateways.get("india");
	public static Gateway g7 = gw.gateways.get("japan");
	public static Gateway g8 = gw.gateways.get("iceland");
	public static Gateway g9 = gw.gateways.get("eastern australia");
	public static Gateway g10 = gw.gateways.get("china");
	public static Gateway g11 = gw.gateways.get("north africa");
	public static List<Gateway> mypath;
	
	public static void main(String args[])
	{
		p1.addMoney(999999);
		p2.addMoney(99999);
		Software proxy = new Proxy();
		p1.addInventory(proxy);
		Software otherproxy = new Proxy();
		p2.addInventory(otherproxy);
		
		g1.setCostsSoFar("MastErAldo", 0);
		g1.setCostsSoFar("spiaggefredde", 0);
		g1.setParentForPath("MastErAldo", null);
		g1.setParentForPath("spiaggefredde", null);
		g2.setCostsSoFar("MastErAldo", 0);
		g2.setCostsSoFar("spiaggefredde", 0);
		g2.setParentForPath("MastErAldo", null);
		g2.setParentForPath("spiaggefredde", null);
		g3.setCostsSoFar("MastErAldo", 0);
		g3.setCostsSoFar("spiaggefredde", 0);
		g3.setParentForPath("MastErAldo", null);
		g3.setParentForPath("spiaggefredde", null);
		g4.setCostsSoFar("MastErAldo", 0);
		g4.setCostsSoFar("spiaggefredde", 0);
		g4.setParentForPath("MastErAldo", null);
		g4.setParentForPath("spiaggefredde", null);
		g5.setCostsSoFar("spiaggefredde", 0);
		g5.setParentForPath("spiaggefredde", null);
		g6.setCostsSoFar("spiaggefredde", 0);
		g6.setParentForPath("spiaggefredde", null);
		g7.setCostsSoFar("spiaggefredde", 0);
		g7.setParentForPath("spiaggefredde", null);
		g8.setCostsSoFar("spiaggefredde", 0);
		g8.setParentForPath("spiaggefredde", null);
		g9.setCostsSoFar("spiaggefredde", 0);
		g9.setParentForPath("spiaggefredde", null);
		g10.setCostsSoFar("spiaggefredde", 0);
		g10.setParentForPath("spiaggefredde", null);
		g11.setCostsSoFar("spiaggefredde", 0);
		g11.setParentForPath("spiaggefredde", null);
		
		g1.setOwner(p1);
		g4.setOwner(p2);
		g2.setOwner(p1);
		g3.setOwner(p1);
		g5.setOwner(p2);
		g6.setOwner(p2);
		g7.setOwner(p2);
		g8.setOwner(p2);
		g9.setOwner(p1);
		g10.setOwner(p1);
		g11.setOwner(p2);
		
		g1.installSoftware(GameConsts.PROXY, p1);
		g2.installSoftware(GameConsts.PROXY, p1);
		g3.installSoftware(GameConsts.PROXY, p1);
		g5.installSoftware(GameConsts.PROXY, p2);
		g6.installSoftware(GameConsts.PROXY, p2);
		g7.installSoftware(GameConsts.PROXY, p2);
		g8.installSoftware(GameConsts.PROXY, p2);
		
		//Quebec attacks Japan
		mypath = g2.tracePath(g7, 5);
		g2.setTrace(mypath, 5);
		printPath(mypath, g2, g7, 5);

		System.out.println(g2.getState()+" upgraded its Proxy");
		g2.upgradeSoftware(GameConsts.PROXY, p1);
		//Quebec attacks Japan
		mypath = g2.tracePath(g7, 6);
		g2.setTrace(mypath, 6);
		printPath(mypath, g2, g7, 6);
		
		//Perù attacks Western Australia
		mypath = g1.tracePath(g4, 5);
		g1.setTrace(mypath, 5);
		printPath(mypath, g1, g4, 5);
		
		
		System.out.println(g5.getState()+" upgraded its Proxy");
		g5.upgradeSoftware(GameConsts.PROXY, p2);
		System.out.println(g8.getState()+" upgraded its Proxy");
		g8.upgradeSoftware(GameConsts.PROXY, p2);
		System.out.println(g2.getState()+" upgraded its Proxy");
		g2.upgradeSoftware(GameConsts.PROXY, p1);
				
		//Perù attacks Quebec
		mypath = g1.tracePath(g2, 5);
		g1.setTrace(mypath, 5);
		printPath(mypath, g1, g2, 5);
		
		//Quebec attacks Ukraine
		mypath = g2.tracePath(g3, 6);
		g2.setTrace(mypath, 6);
		printPath(mypath, g2, g3, 6);

		//Ukraine attacks Western Australia
		mypath = g3.tracePath(g4, 6);
		g3.setTrace(mypath, 6);
		printPath(mypath, g3, g4, 6);
		
		
		//Perù attacks North Africa
		mypath = g1.tracePath(g11, 7);
		g1.setTrace(mypath, 7);
		printPath(mypath, g1, g11, 7);
		
		
		System.out.println(g1.getState()+" upgraded its Proxy");
		//Perù upgrades Proxy
		g1.upgradeSoftware(GameConsts.PROXY, p1);
		
		//Perù attacks Western Australia
		mypath = g1.tracePath(g4, 6);
		g1.setTrace(mypath, 6);
		printPath(mypath, g1, g4, 6);
		
		//Congo attacks China
		mypath = g5.tracePath(g10, 7);
		g5.setTrace(mypath, 7);
		printPath(mypath, g5, g10, 7);
		//India attacks Eastern Australia
		mypath = g6.tracePath(g9, 4);
		g6.setTrace(mypath, 4);
		printPath(mypath, g6, g9, 4);
		

		//Iceland attacks Ukraine
		mypath = g8.tracePath(g3, 5);
		g8.setTrace(mypath, 5);
		printPath(mypath, g8, g3, 5);
		
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		System.out.println("Press Enter to start da police");
		try {
			br.readLine();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		//POLIZIA
		
		Thread ai = new AIThread(gw);
		ai.start();
		
		
	}
	
	
	
	public static void printPath(List<Gateway> path, Gateway g1, Gateway g2, int rel)
	{
		System.out.println("------------------------------------------");
		System.out.println(g1.getState()+" attacks "+g2.getState()+" with relevance:  "+rel);
		if(path != null)
		{
			for(Gateway gw : path)
			{
				System.out.println(gw.getState());
			}
		}
		else System.out.println("PUPPA!");
		System.out.println("------------------------------------------");
	}

}
