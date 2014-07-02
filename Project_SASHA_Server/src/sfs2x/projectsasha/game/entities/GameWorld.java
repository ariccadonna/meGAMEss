package sfs2x.projectsasha.game.entities;

import java.util.Hashtable;
import java.util.Map;
import sfs2x.projectsasha.game.entities.gateways.*;

public class GameWorld 
{
	public int id;
	public Thread ai;
	public Thread money;
	public Region[] regions;
	public Map<String,Gateway> gateways = new Hashtable<String,Gateway>();
	
	public GameWorld(int id)
	{
		this.id = id;
	
		gateways.put("iceland",				new Scientific(null, "Bjork Reactable Lab.", "Iceland", -1.6f, 3, 64.963051f, -19.020835f));						//
		gateways.put("great britain",		new Base(null, "Gary McKinnon House", "Great Britain", -1, 2.2f, 55.378051f, -3.435973f));							//
		gateways.put("scandinavia", 		new Educational(null, "Uppsala University", "Scandinavia", -0.2f, 3, 62.278648f, 12.340171f));						//
		gateways.put("northern europe",		new Bank(null, "European Central Bank", "Northern Europe", -0.3f, 2.1f, 52.519171f, 13.406091f));					//
		gateways.put("ukraine", 			new Base(null, "nUKlear Research Lab.", "Ukraine", 0.7f, 2.4f, 48.379433f, 31.165580f));							//
		gateways.put("western europe",		new Educational(null, "Pietro Maria Marrone's Movie Archives", "Western Europe", -1.1f, 1.5f, 40.463667f, -3.749220f));	//
		gateways.put("southern europe",		new Base(null, "PONG Lab.", "Southern Europe", -0.35f, 1.6f, 41.608635f, 21.745275f));								//
		gateways.put("north africa",		new Base(null, "Black Oil Corp.", "North Africa", -1.2f, 0.3f, 21.007890f, -10.940835f));							//
		gateways.put("egypt",				new Military(null, "Anubi Military Airpor", "Egypt", -0.16f, 0.76f, 26.820553f, 30.802498f));						//
		gateways.put("east africa",			new Base(null, "Desert City Mainframe", "East Africa", 0.45f, -0.25f, -0.023559f, 37.906193f));						//
		gateways.put("congo",				new Base(null, "Reign in Blood Anti-Democracy", "Congo", -0.28f, -0.6f, -0.228021f, 15.827659f));					//
		gateways.put("madagascar",			new Bank(null, "MastErAldo's Diamonds bank", "Madagascar", 0.8f, -1.35f, -18.766947f, 46.869107f));					//
		gateways.put("south africa",		new Educational(null, "University of the South", "South Africa", 0, -1.6f, -30.559482f, 22.937506f));				//
		gateways.put("middle east",			new Base(null, "Technion Israel Inst. of Technology", "Middle East", 0.6f, 1, 29.298528f, 42.550960f));				//
		gateways.put("afghanistan",			new Military(null, "Afghan National Army", "Afghanistan", 1.5f, 1.7f, 33.939110f, 67.709953f));						//
		gateways.put("india",				new Scientific(null, "Silicon Valley of India", "India", 2, 0.7f, 20.593684f, 78.962880f));							//
		gateways.put("siam",				new Base(null, "Huaway Technologies", "Siam", 2.8f, 0.3f, 51.506568f, 3.633689f));							//
		gateways.put("china",				new Government(null, "Gov.cn", "China", 2.8f, 1, 35.861660f, 104.195397f));									//
		gateways.put("ural",				new Base(null, "Borderland Radio Transmitter", "Ural", 1.75f, 2.6f, 54.428772f, 40.057638f));				//
		gateways.put("siberia",				new Base(null, "Project SASHA Headquarter", "Siberia", 2.7f, 3f, 34.627258f, -115.987335f));				//
		gateways.put("mongolia",			new Base(null, "Gengis Kahn Hacking Team", "Mongolia", 3.8f, 1.7f, 46.862496f, 103.846656f));				//
		gateways.put("irkutsk",				new Educational(null, "Department of UFOlogy", "Irkutsk", 3.8f, 2.1f, 52.277141f, 104.304164f));			//
		gateways.put("yakutsk",				new Base(null, "National Weather Station", "Yakutsk", 4, 3, 62.026324f, 129.731145f));						//
		gateways.put("kamchatka",			new Base(null, "Tundra Main Airport", "Kamchatka", 5.2f, 3, 44.708646f, 37.632769f));						//
		gateways.put("japan",				new Scientific(null, "SONI Technology", "Japan", 4.2f, 1.3f, 36.204824f, 138.252924f));						//
		gateways.put("indonesia",			new Base(null, "SUN Java Islands", "Indonesia", 3.3f, -0.8f, -0.789275f, 113.921327f));						//
		gateways.put("western australia", 	new Government(null, "Commonwealth Headquarter", "Western Australia", 4.1f, -2, -27.672817f, 121.628310f));	//
		gateways.put("eastern australia", 	new Bank(null, "Commonwealth Bank", "Eastern Australia", 4.9f, -2, -35.778940f, 137.795825f));				//
		gateways.put("new guinea", 			new Base(null, "AND CPU Factory", "New Guinea", 4.7f, -1, -5.092566f, 140.711092f));						//
		gateways.put("alaska", 				new Scientific(null, "I.C.E. Experimental Site", "Alaska", -6.2f, 2.7f, 64.200841f, -149.493673f));			//
		gateways.put("north west territory",new Base(null, "Ubrisoft", "North West Territory", -4.2f, 2.2f, 39.800603f, -89.725199f));					//
		gateways.put("alberta", 			new Base(null, "Missilistic Base of Alberta", "Alberta", -5f, 1.85f, 53.933271f, -116.576504f));			//
		gateways.put("western usa", 		new Bank(null, "Kevin Mitnick House", "Western USA", -4.7f, 1, 37.661950f, -122.409936f));					//
		gateways.put("eastern usa", 		new Military(null, "NYC Military Base", "Eastern USA", -3.9f, 1, 31.952856f, -102.171694f)); 				//
		gateways.put("ontario", 			new Base(null, "UTAH-TECH", "Ontario", -4.1f, 1.75f, 51.253775f, -85.323214f));								//
		gateways.put("central america", 	new Base(null, "McAfee Secret Hidden Place", "Central America", -4.3f, 0.15f, 12.769013f, -85.602364f));	//
		gateways.put("quebec",				new Base(null, "Q.", "Quebec", -3.2f, 1.7f, 52.939916f, -73.549136f));										//
		gateways.put("greenland",			new Government(null, "Greenland DNS", "Greenland", -2.3f, 3.3f, 71.706936f, -42.604303f));					//
		gateways.put("venezuela",			new Base(null, "Caracas Rum Factory", "Venezuela", -3.3f, -0.7f, 6.423750f, -66.589730f));					//
		gateways.put("peru",				new Base(null, "New Mayan Empire", "Peru", -3.5f, -1.25f, -9.189967f, -75.015152f));						//
		gateways.put("brazil",				new Military(null, "Uncle Sam Army Ranch", "Brazil", -2.7f, -1.5f, -14.235004f, -51.925280f));				//
		gateways.put("argentina",			new Government(null, "Tango Down operation HQ", "Argentina", -3.2f, -2.6f, -38.416097f, -63.616672f));		//
		
		gateways.get("iceland").setNeighborhoods(new Gateway[]
		{
			gateways.get("greenland"), 
			gateways.get("great britain")
		});
		gateways.get("great britain").setNeighborhoods(new Gateway[]
		{
			gateways.get("iceland"), 
			gateways.get("western europe"),
			gateways.get("northern europe")
		});
		gateways.get("scandinavia").setNeighborhoods(new Gateway[]
		{
			gateways.get("northern europe"), 
			gateways.get("ukraine")
		});
		gateways.get("northern europe").setNeighborhoods(new Gateway[]
		{
			gateways.get("scandinavia"), 
			gateways.get("great britain"),
			gateways.get("ukraine"),
			gateways.get("western europe"),
			gateways.get("southern europe")
		});
		gateways.get("ukraine").setNeighborhoods(new Gateway[]
		{
			gateways.get("scandinavia"), 
			gateways.get("northern europe"),
			gateways.get("southern europe"),
			gateways.get("middle east"),
			gateways.get("ural"),
			gateways.get("afghanistan")
		});
		gateways.get("western europe").setNeighborhoods(new Gateway[]
		{
			gateways.get("northern europe"), 
			gateways.get("great britain"),
			gateways.get("southern europe"),
			gateways.get("north africa")
		});
		gateways.get("southern europe").setNeighborhoods(new Gateway[]
		{
			gateways.get("western europe"), 
			gateways.get("northern europe"),
			gateways.get("ukraine"),
			gateways.get("middle east"),
			gateways.get("egypt"),
			gateways.get("north africa"),
		});
		gateways.get("north africa").setNeighborhoods(new Gateway[]
		{
			gateways.get("western europe"), 
			gateways.get("southern europe"),
			gateways.get("egypt"),
			gateways.get("east africa"),
			gateways.get("congo"),
			gateways.get("brazil"),
		});
		gateways.get("egypt").setNeighborhoods(new Gateway[]
		{
			gateways.get("north africa"), 
			gateways.get("southern europe"),
			gateways.get("middle east"),
			gateways.get("east africa")
		});
		gateways.get("east africa").setNeighborhoods(new Gateway[]
		{
			gateways.get("north africa"), 
			gateways.get("south africa"),
			gateways.get("egypt"),
			gateways.get("congo"),
			gateways.get("madagascar"),
			gateways.get("middle east"),
		});
		gateways.get("congo").setNeighborhoods(new Gateway[]
		{
			gateways.get("north africa"), 
			gateways.get("south africa"),
			gateways.get("east africa")
		});
		gateways.get("south africa").setNeighborhoods(new Gateway[]
		{
			gateways.get("madagascar"), 
			gateways.get("east africa"),
			gateways.get("congo")
		});
		gateways.get("madagascar").setNeighborhoods(new Gateway[]
		{
			gateways.get("south africa"), 
			gateways.get("east africa")
		});
		gateways.get("middle east").setNeighborhoods(new Gateway[]
		{
			gateways.get("ukraine"), 
			gateways.get("east africa"),
			gateways.get("southern europe"),
			gateways.get("egypt"),
			gateways.get("afghanistan"),
			gateways.get("india")
		});
		gateways.get("afghanistan").setNeighborhoods(new Gateway[]
		{
			gateways.get("ukraine"), 
			gateways.get("middle east"),
			gateways.get("china"),
			gateways.get("ural"),
			gateways.get("india")
		});
		gateways.get("india").setNeighborhoods(new Gateway[]
		{
			gateways.get("middle east"), 
			gateways.get("siam"),
			gateways.get("china"),
			gateways.get("afghanistan"),
		});
		gateways.get("siam").setNeighborhoods(new Gateway[]
		{
			gateways.get("indonesia"), 
			gateways.get("china"),
			gateways.get("india")
		});
		gateways.get("china").setNeighborhoods(new Gateway[]
		{
			gateways.get("siam"), 
			gateways.get("india"),
			gateways.get("afghanistan"),
			gateways.get("ural"),
			gateways.get("siberia"),
			gateways.get("mongolia")
		});
		gateways.get("ural").setNeighborhoods(new Gateway[]
		{
			gateways.get("ukraine"), 
			gateways.get("siberia"),
			gateways.get("china"),
			gateways.get("afghanistan")
		});
		gateways.get("siberia").setNeighborhoods(new Gateway[]
		{
			gateways.get("ural"), 
			gateways.get("china"),
			gateways.get("mongolia"),
			gateways.get("irkutsk"),
			gateways.get("yakutsk")
		});
		gateways.get("mongolia").setNeighborhoods(new Gateway[]
		{
			gateways.get("china"), 
			gateways.get("siberia"),
			gateways.get("irkutsk"),
			gateways.get("kamchatka"),
			gateways.get("japan")
		});
		gateways.get("irkutsk").setNeighborhoods(new Gateway[]
		{
			gateways.get("mongolia"), 
			gateways.get("siberia"),
			gateways.get("yakutsk"),
			gateways.get("kamchatka")
		});
		gateways.get("yakutsk").setNeighborhoods(new Gateway[]
		{
			gateways.get("siberia"), 
			gateways.get("irkutsk"),
			gateways.get("kamchatka")
		});
		gateways.get("kamchatka").setNeighborhoods(new Gateway[]
		{
			gateways.get("yakutsk"), 
			gateways.get("irkutsk"),
			gateways.get("mongolia"),
			gateways.get("japan"),
			gateways.get("alaska"),
		});
		gateways.get("japan").setNeighborhoods(new Gateway[]
		{
			gateways.get("kamchatka"), 
			gateways.get("mongolia")
		});
		gateways.get("indonesia").setNeighborhoods(new Gateway[]
		{
			gateways.get("western australia"), 
			gateways.get("new guinea"),
			gateways.get("siam")

		});
		gateways.get("western australia").setNeighborhoods(new Gateway[]
		{
			gateways.get("indonesia"), 
			gateways.get("new guinea"),
			gateways.get("eastern australia")
		});
		gateways.get("eastern australia").setNeighborhoods(new Gateway[]
		{
			gateways.get("new guinea"), 
			gateways.get("western australia")
		});
		gateways.get("new guinea").setNeighborhoods(new Gateway[]
		{
			gateways.get("western australia"), 
			gateways.get("indonesia"),
			gateways.get("eastern australia")
		});
		gateways.get("alaska").setNeighborhoods(new Gateway[]
		{
			gateways.get("kamchatka"), 
			gateways.get("north west territory"),
			gateways.get("alberta")
		});
		gateways.get("alberta").setNeighborhoods(new Gateway[]
		{
			gateways.get("alaska"), 
			gateways.get("north west territory"),
			gateways.get("ontario"),
			gateways.get("western usa")
		});
		gateways.get("western usa").setNeighborhoods(new Gateway[]
		{
			gateways.get("alberta"), 
			gateways.get("ontario"),
			gateways.get("eastern usa"),
			gateways.get("central america")
		});
		gateways.get("north west territory").setNeighborhoods(new Gateway[]
		{
			gateways.get("alaska"), 
			gateways.get("alberta"),
			gateways.get("ontario"),
			gateways.get("greenland")
		});
		gateways.get("central america").setNeighborhoods(new Gateway[]
		{
			gateways.get("western usa"), 
			gateways.get("venezuela"),
			gateways.get("eastern usa")
		});
		gateways.get("eastern usa").setNeighborhoods(new Gateway[]
		{
			gateways.get("quebec"), 
			gateways.get("ontario"),
			gateways.get("western usa"),
			gateways.get("central america")
		});
		gateways.get("ontario").setNeighborhoods(new Gateway[]
		{
			gateways.get("alberta"), 
			gateways.get("quebec"),
			gateways.get("eastern usa"),
			gateways.get("north west territory"),
			gateways.get("western usa"),
			gateways.get("greenland")
		});
		gateways.get("quebec").setNeighborhoods(new Gateway[]
		{
			gateways.get("greenland"), 
			gateways.get("ontario"),
			gateways.get("eastern usa")
		});
		gateways.get("greenland").setNeighborhoods(new Gateway[]
		{
			gateways.get("quebec"), 
			gateways.get("ontario"),
			gateways.get("north west territory"),
			gateways.get("iceland")
		});
		gateways.get("venezuela").setNeighborhoods(new Gateway[]
		{
			gateways.get("peru"), 
			gateways.get("brazil"),
			gateways.get("central america")
		});
		gateways.get("peru").setNeighborhoods(new Gateway[]
		{
			gateways.get("venezuela"), 
			gateways.get("brazil"),
			gateways.get("argentina")
		});
		gateways.get("brazil").setNeighborhoods(new Gateway[]
		{
			gateways.get("venezuela"), 
			gateways.get("peru"),
			gateways.get("argentina"),
			gateways.get("north africa")
		});
		gateways.get("argentina").setNeighborhoods(new Gateway[]
		{
			gateways.get("brazil"), 
			gateways.get("peru")
		});

		
		regions = new Region[]
			{
				new Region("The Frozen Lands", new Gateway[]
						{
							gateways.get("greenland"), 
							gateways.get("alaska"),
							gateways.get("north west territory")
						}),
				new Region("Atlantic Coast", new Gateway[]
						{
							gateways.get("ontario"), 
							gateways.get("quebec"),
							gateways.get("eastern usa")
						}),
				new Region("Pacific Coast", new Gateway[]
						{
							gateways.get("alberta"), 
							gateways.get("central america"),
							gateways.get("western usa")
						}),
				new Region("The Andes", new Gateway[]
						{
							gateways.get("peru"), 
							gateways.get("argentina")
						}),
				new Region("Amazon", new Gateway[]
						{
							gateways.get("brazil"), 
							gateways.get("venezuela")
						}),
				new Region("The Great Kingdom", new Gateway[]
						{
							gateways.get("great britain"), 
							gateways.get("iceland")
						}),
				new Region("The New World Order", new Gateway[]
						{
							gateways.get("ukraine"), 
							gateways.get("scandinavia"),
							gateways.get("northern europe")
						}),
				new Region("The Mediterranean Confederation", new Gateway[]
						{
							gateways.get("western europe"), 
							gateways.get("southern europe")
						}),
				new Region("The Desert", new Gateway[]
						{
							gateways.get("north africa"), 
							gateways.get("east africa"),
							gateways.get("egypt")
						}),
				new Region("The Jungle", new Gateway[]
						{
							gateways.get("congo"), 
							gateways.get("south africa"),
							gateways.get("madagascar")
						}),
				new Region("The Gate to the West", new Gateway[]
						{
							gateways.get("japan"), 
							gateways.get("kamchatka"),
							gateways.get("mongolia")
						}),
				new Region("The Russian Rejects", new Gateway[]
						{
							gateways.get("siberia"), 
							gateways.get("irkutsk"),
							gateways.get("yakutsk")
						}),
				new Region("The Harabs", new Gateway[]
						{
							gateways.get("middle east"), 
							gateways.get("ural"),
							gateways.get("afghanistan")
						}),
				new Region("Buddhack Lodge", new Gateway[]
						{
							gateways.get("india"), 
							gateways.get("china"),
							gateways.get("siam")
						}),
				new Region("Austrislands", new Gateway[]
						{
							gateways.get("new guinea"), 
							gateways.get("indonesia")
						}),
				new Region("The Giant Spiders Land", new Gateway[]
						{
							gateways.get("western australia"), 
							gateways.get("eastern australia")
						}),
			};
	}
	
}
