package sfs2x.extensions.projectsasha.game.entities.software;

import sfs2x.extensions.projectsasha.game.GameConsts;

public class SoftwareFactory {
	public static Software makeSoftware(String type) 
	{
		Software ret = null;
		if(type == GameConsts.FIREWALL)
			ret = new Firewall();
		if(type == GameConsts.IDS)
			ret = new IDS();
		if(type == GameConsts.LOGCLEANER)
			ret = new LogCleaner();
		if(type == GameConsts.ANTIVIRUS)
			ret = new Antivirus();
		if(type == GameConsts.VIRUS)
			ret = new Virus();
		if(type == GameConsts.DEEPTHROAT)
			ret = new DeepThroat();
		if(type == GameConsts.SNIFFER)
			ret = new Sniffer();
		if(type == GameConsts.BRUTEFORCER)
			ret = new BruteForcer();
		if(type == GameConsts.DICTIONARY)
			ret = new DictionaryAttacker();
		if(type == GameConsts.PROXY)
			ret = new Proxy();
		/*
		 * FIXME: togliere i fix per Sam
		 */
		/*switch(type){	
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
		}*/
		return ret;
	}
}
