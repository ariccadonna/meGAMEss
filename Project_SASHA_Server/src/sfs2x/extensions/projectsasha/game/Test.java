package sfs2x.extensions.projectsasha.game;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Iterator;
import java.util.Set;

import sfs2x.extensions.projectsasha.game.entities.GameWorld;
import sfs2x.extensions.projectsasha.game.entities.Player;

import sfs2x.extensions.projectsasha.game.entities.gateways.*;

public class Test 
{
	private static GameWorld world;
	private static User u, u2;
	private static Player p, p2;
	private static Gateway starting_gateway;
	private static Gateway starting_gateway_u2;
	//static MoneyThread moneyTh;
	private static Thread moneyThread;
	static long startingTime;

	public static void init(){
		
		
		world = new GameWorld(0);
		moneyThread = new Thread(new MoneyThread(world));
		startingTime = System.currentTimeMillis();
		
		u = new User("TestUser");
		p = new Player(u);
		
		starting_gateway = world.gateways.get("scandinavia");
		starting_gateway.setOwner(p);
		
		Gateway g1 = world.gateways.get("greenland");
		Gateway g2 = world.gateways.get("alaska");
		Gateway g3 = world.gateways.get("north west territory");
		g1.setOwner(p);
		g2.setOwner(p);
		g3.setOwner(p);
		
		starting_gateway.installSoftware(GameConsts.ANTIVIRUS, p);
		starting_gateway.installSoftware(GameConsts.BRUTEFORCER,  p);
		starting_gateway.installSoftware(GameConsts.DEEPTHROAT, p);
		
		//starting_gateway.installSoftware(GameConsts.DICTIONARY, p);
		//starting_gateway.installSoftware(GameConsts.FIREWALL, p);
		//starting_gateway.installSoftware(GameConsts.IDS, p);
		//starting_gateway.installSoftware(GameConsts.LOGCLEANER, p);
		//starting_gateway.installSoftware(GameConsts.PROXY, p);
		//starting_gateway.installSoftware(GameConsts.SNIFFER, p);
		//starting_gateway.installSoftware(GameConsts.VIRUS, p);
		
		//Upgrade by slot
		//starting_gateway.upgradeSoftware(0, p);
		
		//Upgrade by sw
		//starting_gateway.upgradeSoftware(GameConsts.VIRUS, p);

		//Upgrade by sw in a specific slot (for cumulatives)
		//starting_gateway.upgradeSoftware(GameConsts.VIRUS, p, 0);

		//System.out.println(starting_gateway.toString());
		
		//**********************//

		u2 = new User("TestUser2");
		p2 = new Player(u2);
		

		starting_gateway_u2 = world.gateways.get("kamchatka");
		starting_gateway_u2.setOwner(p2);

		starting_gateway_u2.installSoftware(GameConsts.ANTIVIRUS, p2);
		starting_gateway_u2.installSoftware(GameConsts.BRUTEFORCER,  p2);
		starting_gateway_u2.installSoftware(GameConsts.DEEPTHROAT, p2);
		starting_gateway_u2.upgradeSoftware(GameConsts.BRUTEFORCER, p2);

		//starting_gateway_u2.installSoftware(GameConsts.DICTIONARY, p);
		//starting_gateway_u2.installSoftware(GameConsts.FIREWALL, p);
		//starting_gateway_u2.installSoftware(GameConsts.IDS, p);
		//starting_gateway_u2.installSoftware(GameConsts.LOGCLEANER, p);
		//starting_gateway_u2.installSoftware(GameConsts.PROXY, p);
		//starting_gateway_u2.installSoftware(GameConsts.SNIFFER, p);
		//starting_gateway_u2.installSoftware(GameConsts.VIRUS, p);

		//**********************//
		
	}
	
	public static void hack(){
		//********** Test a caso. ************//
				Gateway target_gateway = world.gateways.get("ukraine");
				target_gateway.setOwner(p2);
				target_gateway.installSoftware(GameConsts.IDS, p2);
				//target_gateway.upgradeSoftware(GameConsts.IDS, p2);
				//Gateway.hack(starting_gateway, target_gateway);
				System.out.println("-------- Players info --------");
				//Gateway.hack(starting_gateway_u2, target_gateway);
				
				target_gateway.installSoftware(GameConsts.FIREWALL, p2);
				target_gateway.installSoftware(GameConsts.FIREWALL, p2);
				target_gateway.upgradeSoftware(GameConsts.FIREWALL, p2, 0);
				//target_gateway.uninstallSoftware(GameConsts.FIREWALL, p2, 0);

				
				target_gateway.uninstallSoftware(GameConsts.BRUTEFORCER, p2);
				

				//p.addMoney(target_gateway.getPaymentAmount());
				
				//target_gateway.upgradeSoftware(GameConsts.FIREWALL);
				target_gateway.installSoftware(GameConsts.BRUTEFORCER, p2);
				target_gateway.installSoftware(GameConsts.DEEPTHROAT, p2);
				target_gateway.upgradeSoftware(GameConsts.DEEPTHROAT, p2);
				/*target_gateway.downgradeSoftware(GameConsts.FIREWALL);
				target_gateway.upgradeSoftware(GameConsts.FIREWALL);
				target_gateway.upgradeSoftware(GameConsts.FIREWALL);
				target_gateway.upgradeSoftware(GameConsts.FIREWALL);*/
	}

	public static void main(String args[])
	{	
		init();
		hack();
		produceMoney();
		while(true)
		{
			console();
		}
	}

	private static void produceMoney() {
		moneyThread.start();
	}
	
	private static void console(){
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in) );
	    @SuppressWarnings("unused")
		String sample = null;
		try {
			sample = br.readLine();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		/*System.out.println(sample);
		switch (sample)
		{
			case "p":
				System.out.println("-------- Players info --------");
				System.out.println(p.toString()+"\n"+p2.toString());
				System.out.println("-------- Players info --------");
			break;
			case "g":
				System.out.println("-------- Starting gateway info --------");
				System.out.println(starting_gateway.toString()+"\n"+starting_gateway_u2.toString());
				System.out.println("-------- Starting gateway info --------");
			break;
		}*/
	}
	public static String json(){
		Set<String> set = world.gateways.keySet();
		Iterator<String> itr = set.iterator();
		String str;
		Gateway[] neighboors;
		Gateway currentGW = null;
		//JSON composition
		String JSONString  = "{";
		JSONString  = "{";
		int i;
		while (itr.hasNext()) {
			str = itr.next();
			currentGW = world.gateways.get(str);
			neighboors = currentGW.getNeighboors(); 
			JSONString += "\""+str+"\":{"+
						"\"STATE\":\""+str+"\","+
						"\"NB\": [";
					
			System.out.print(currentGW.getState()+":");
			for (i=0; i!=currentGW.getNeighboors().length-1; i++)
			{
				System.out.print(neighboors[i].getState()+", ");
				JSONString +="\""+neighboors[i].getState()+"\",";
			}	
			
			JSONString +="\""+neighboors[currentGW.getNeighboors().length-1].getState()+"\"";
			System.out.print(neighboors[currentGW.getNeighboors().length-1].getState()+"\n");
			JSONString +="]"+
			"},";
		}
		JSONString = JSONString.substring(0, JSONString.length()-1);
		JSONString+="}";
		return JSONString;
	}
}
