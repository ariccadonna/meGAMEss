package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;

public class SoftwareFactory {
	public static Software makeSoftware(String type) 
	{
		Software ret = null;
		
		switch(type){	
		case GameConsts.FIREWALL:	
			ret = new Firewall();
		break;
		case GameConsts.IDS:
			ret = new IDS();
			break;
		case GameConsts.LOGCLEANER:
			ret = new LogCleaner();
			break;
		case GameConsts.ANTIVIRUS: 
			ret = new Antivirus();
			break;
		case GameConsts.VIRUS: 
			ret = new Virus();
			break;
		case GameConsts.DEEPTHROAT: 
			ret = new DeepThroat();
			break;
		case GameConsts.SNIFFER: 
			ret = new Sniffer();
			break;
		case GameConsts.BRUTEFORCER: 
			ret = new BruteForcer();
			break;
		case GameConsts.DICTIONARY: 
			ret = new DictionaryAttacker();
			break;
		case GameConsts.PROXY: 
			ret = new Proxy();
			break;
		}
		return ret;
	}
}
