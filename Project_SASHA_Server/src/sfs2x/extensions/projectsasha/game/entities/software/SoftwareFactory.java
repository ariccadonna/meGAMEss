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
	
	public static Software makeSoftware(String type, int version) 
	{
		Software ret = null;
		if(type == GameConsts.FIREWALL)
			ret = new Firewall(version);
		if(type == GameConsts.IDS)
			ret = new IDS(version);
		if(type == GameConsts.LOGCLEANER)
			ret = new LogCleaner(version);
		if(type == GameConsts.ANTIVIRUS)
			ret = new Antivirus(version);
		if(type == GameConsts.VIRUS)
			ret = new Virus(version);
		if(type == GameConsts.DEEPTHROAT)
			ret = new DeepThroat(version);
		if(type == GameConsts.SNIFFER)
			ret = new Sniffer(version);
		if(type == GameConsts.BRUTEFORCER)
			ret = new BruteForcer(version);
		if(type == GameConsts.DICTIONARY)
			ret = new DictionaryAttacker();
		if(type == GameConsts.PROXY)
			ret = new Proxy(version);
		/*
		 * FIXME: togliere i fix per Sam
		 */
		/*switch(type){	
		case GameConsts.FIREWALL:	
			ret = new Firewall(version);
		break;
		case GameConsts.IDS:
			ret = new IDS(version);
			break;
		case GameConsts.LOGCLEANER:
			ret = new LogCleaner(version);
			break;
		case GameConsts.ANTIVIRUS: 
			ret = new Antivirus(version);
			break;
		case GameConsts.VIRUS: 
			ret = new Virus(version);
			break;
		case GameConsts.DEEPTHROAT: 
			ret = new DeepThroat(version);
			break;
		case GameConsts.SNIFFER: 
			ret = new Sniffer(version);
			break;
		case GameConsts.BRUTEFORCER: 
			ret = new BruteForcer(version);
			break;
		case GameConsts.DICTIONARY: 
			ret = new DictionaryAttacker(version);
			break;
		case GameConsts.PROXY: 
			ret = new Proxy(version);
			break;
		}*/
		return ret;
	}
}
