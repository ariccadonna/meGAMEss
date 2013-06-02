package sfs2x.extensions.projectsasha.game.entities;



import java.util.Hashtable;
import java.util.Map;

import sfs2x.extensions.projectsasha.game.entities.gateways.*;

public class GameWorld 
{
	public int id;
	public Region[] regions;
	public static Map<String,Gateway> gateways = new Hashtable<String,Gateway>();
	public long startingTime;
	
	public GameWorld(int id)
	{
		this.id = id;
	
		gateways.put("iceland",				new Scientific(null, "nome", "Iceland", 221, 176));
		gateways.put("great britain",		new Base(null, "nome", "Great Britain", 138, 30));
		gateways.put("scandinavia", 		new Educational(null, "nome", "Scandinavia", 203, 106));
		gateways.put("northern europe",		new Bank(null, "nome", "Northern Europe", 194, 23));
		gateways.put("ukraine", 			new Base(null, "nome", "Ukraine", 288, 58));
		gateways.put("western europe",		new Educational(null, "nome", "Western Europe", 136, -37));
		gateways.put("southern europe",		new Base(null, "nome", "Southern Europe", 219, -14));
		gateways.put("north africa",		new Base(null, "nome", "Northern Europe", 154, -138 ));
		gateways.put("egypt",				new Military(null, "nome", "Egypt", 238, -100));
		gateways.put("east africa",			new Base(null, "nome", "East Africa", 266, -159));
		gateways.put("congo",				new Base(null, "nome", "Congo", 227, -219));
		gateways.put("madagascar",			new Bank(null, "nome", "Madagascar", 311, -306));
		gateways.put("south africa",		new Educational(null, "nome", "South Africa", 231, -308));
		gateways.put("middle east",			new Base(null, "nome", "Middle East", 302, -71));
		gateways.put("afghanistan",			new Military(null, "nome", "Afghanistan", 377, -8));
		gateways.put("india",				new Scientific(null, "nome", "India", 422, -110));
		gateways.put("siam",				new Base(null, "nome", "Siam", 511, -135));
		gateways.put("china",				new Government(null, "nome", "China", 484, -40));
		gateways.put("ural",				new Base(null, "nome", "Ural", 387, 87));
		gateways.put("siberia",				new Base(null, "nome", "Siberia", 449, 118));
		gateways.put("mongolia",			new Base(null, "nome", "Mongolia", 545, 30)); //missing 
		gateways.put("irkutsk",				new Educational(null, "nome", "Irkutsk", 518, 62));
		gateways.put("yakutsk",				new Base(null, "nome", "Yakutsk", 543, 133));
		gateways.put("kamchatka",			new Base(null, "nome", "Kamchatka", 639, 126));
		gateways.put("japan",				new Scientific(null, "nome", "Japan", 322, -8));
		gateways.put("indonesia",			new Base(null, "nome", "Indonesia", 555, -202));
		gateways.put("western australia", 	new Government(null, "nome", "Western Australia", 562, -319));
		gateways.put("eastern australia", 	new Bank(null, "nome", "Eastern Australia", 649, -294));
		gateways.put("new guinea", 			new Base(null, "nome", "New Guinea", 639, -220));
		gateways.put("alaska", 				new Scientific(null, "nome", "Alaska", -244, 128));
		gateways.put("north west territory",new Base(null, "nome", "North West Territory", -146, 122));
		gateways.put("alberta", 			new Base(null, "nome", "Alberta", -136, 70));
		gateways.put("western usa", 		new Bank(null, "nome", "Western USA", -146, -2));
		gateways.put("eastern usa", 		new Military(null, "nome", "Eastern USA", -76, -19));
		gateways.put("ontario", 			new Base(null, "nome", "Ontario", -64, 65));
		gateways.put("central america", 	new Base(null, "nome", "Central America", -137, -68));
		gateways.put("quebec",				new Base(null, "nome", "Quebec", -12, 65));
		gateways.put("greenland",			new Government(null, "nome", "Greenland", 85, 166));
		gateways.put("venezuela",			new Base(null, "nome", "Venezuela", -185, -120));
		gateways.put("peru",				new Base(null, "nome", "Perù", -163, -184));
		gateways.put("brazil",				new Military(null, "nome", "Brazil", -102, -163));
		gateways.put("argentina",			new Government(null, "nome", "Argentina", -160, -256));	
		
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
			gateways.get("north west territory"),
			gateways.get("iceland")
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
			gateways.get("argentina")
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
	
	public void setStartingTime(long time)
	{
		startingTime = System.currentTimeMillis();
	}
}
