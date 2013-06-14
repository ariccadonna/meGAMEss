package sfs2x.projectsasha.game.entities;

import java.util.List;
import java.util.Vector;

import sfs2x.projectsasha.game.GameConsts;
import sfs2x.projectsasha.game.entities.software.Software;
import sfs2x.projectsasha.game.entities.software.SoftwareFactory;

public class Shop 
{
	protected List<Software> items;

	//COSTRUCTOR
	
	public Shop()
	{
		items = new Vector<Software>();
		items.add(SoftwareFactory.makeSoftware(GameConsts.FIREWALL));
		items.add(SoftwareFactory.makeSoftware(GameConsts.FIREWALL, 2));
		items.add(SoftwareFactory.makeSoftware(GameConsts.FIREWALL, 3));		
		items.add(SoftwareFactory.makeSoftware(GameConsts.IDS));
		items.add(SoftwareFactory.makeSoftware(GameConsts.IDS, 2));
		items.add(SoftwareFactory.makeSoftware(GameConsts.IDS, 3));
		items.add(SoftwareFactory.makeSoftware(GameConsts.LOGCLEANER));
		items.add(SoftwareFactory.makeSoftware(GameConsts.LOGCLEANER, 2));
		items.add(SoftwareFactory.makeSoftware(GameConsts.LOGCLEANER, 3));
		items.add(SoftwareFactory.makeSoftware(GameConsts.ANTIVIRUS));
		items.add(SoftwareFactory.makeSoftware(GameConsts.ANTIVIRUS, 2));
		items.add(SoftwareFactory.makeSoftware(GameConsts.ANTIVIRUS, 3));
		items.add(SoftwareFactory.makeSoftware(GameConsts.VIRUS));
		items.add(SoftwareFactory.makeSoftware(GameConsts.VIRUS, 2));
		items.add(SoftwareFactory.makeSoftware(GameConsts.VIRUS, 3));
		items.add(SoftwareFactory.makeSoftware(GameConsts.SNIFFER));
		items.add(SoftwareFactory.makeSoftware(GameConsts.SNIFFER, 2));
		items.add(SoftwareFactory.makeSoftware(GameConsts.SNIFFER, 3));
		items.add(SoftwareFactory.makeSoftware(GameConsts.BRUTEFORCER));
		items.add(SoftwareFactory.makeSoftware(GameConsts.BRUTEFORCER, 2));
		items.add(SoftwareFactory.makeSoftware(GameConsts.BRUTEFORCER, 3));
		items.add(SoftwareFactory.makeSoftware(GameConsts.DICTIONARY));
		items.add(SoftwareFactory.makeSoftware(GameConsts.DICTIONARY, 2));
		items.add(SoftwareFactory.makeSoftware(GameConsts.DICTIONARY, 3));
		items.add(SoftwareFactory.makeSoftware(GameConsts.PROXY));
		items.add(SoftwareFactory.makeSoftware(GameConsts.PROXY, 2));
		items.add(SoftwareFactory.makeSoftware(GameConsts.PROXY, 3));
		items.add(SoftwareFactory.makeSoftware(GameConsts.DEEPTHROAT));
	}
	
	public List<Software> getShop()
	{
		return this.items;
	}





}
