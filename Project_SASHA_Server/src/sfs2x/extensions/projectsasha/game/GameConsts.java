package sfs2x.extensions.projectsasha.game;

public class GameConsts 
{
	public static final String USER_TABLE 			= "users";
	public static final boolean DEBUG 				= false;
	public static final int EARTH_RADIUS			= 6371;
	public static final int EARTH_MAX_DISTANCE		= 20015;
	public static final int MONEY_UPDATE_TIME		= 5000;
	public static final int BONUS_MONEY_PER_REGION	= 10;
	public static final int CONQUER_TIME_TRESHOLD	= 30; //time to add when neutralizing and hacking a gateway in seconds
	public static final int FAILTIME				= 10; //time to wait if an hack fails in seconds
	
	public static final int INVENTORY_SIZE			= 10;

	/* Gateway parameters */
	public static final String[] STARTING_SPOTS		= {
														"north west territory",	//starting spot p1
														"siberia",				//starting spot p2
														"peru",					//starting spot p3
														"new guinea",			//starting spot p4
														"ukraine"				//starting spot p5
														};
	
	public static final String BASE_GATEWAY			= "Base";
	public static final int BASE_PAYMENT_AMOUNT 	= 5;
	public static final int BASE_DEFENCE_LEVEL		= 5;
	public static final int BASE_ATTACK_LEVEL		= 5;
	
	public static final String BANK_GATEWAY			= "Bank";
	public static final int BANK_BONUS_MULTIPLIER	= 10;
	public static final int BANK_BASE_PAYMENT_AMOUNT= 20;
	public static final int BANK_BASE_DEFENCE_LEVEL	= 10;
	public static final int BANK_BASE_ATTACK_LEVEL	= 5;
	
	public static final String EDU_GATEWAY			= "Educational";
	public static final int EDU_BONUS_MULTIPLIER	= 10;
	public static final int EDU_BASE_PAYMENT_AMOUNT	= 5;
	public static final int EDU_BASE_DEFENCE_LEVEL	= 10;
	public static final int EDU_BASE_ATTACK_LEVEL	= 5;
	
	public static final String GOV_GATEWAY			= "Government";
	public static final int GOV_BONUS_MULTIPLIER	= 10;
	public static final int GOV_BASE_PAYMENT_AMOUNT	= 15;
	public static final int GOV_BASE_DEFENCE_LEVEL	= 10;
	public static final int GOV_BASE_ATTACK_LEVEL	= 10;
	
	public static final String MIL_GATEWAY			= "Military";
	public static final int MIL_BONUS_MULTIPLIER	= 10;
	public static final int MIL_BASE_PAYMENT_AMOUNT = 10;
	public static final int MIL_BASE_DEFENCE_LEVEL	= 10;
	public static final int MIL_BASE_ATTACK_LEVEL	= 15;
	
	public static final String SCI_GATEWAY			= "Scientific";
	public static final int SCI_BONUS_MULTIPLIER	= 10;
	public static final int SCI_BASE_PAYMENT_AMOUNT = 10;
	public static final int SCI_BASE_DEFENCE_LEVEL	= 15;
	public static final int SCI_BASE_ATTACK_LEVEL	= 5;
	
	//***********************//

	public static final int MAX_SOFTWARE_INSTALLED	= 3;
	
	/* Defensive Software */
	public static final String FIREWALL 				= "FIREWALL";
	public static final String FIREWALL_NAME 			= "FireWool";
	public static final int FIREWALL_DEFENCE_LEVEL 		= 10;
	public static final int FIREWALL_MAX_LEVEL			= 3;
	public static final boolean FIREWALL_CUMULATIVE		= true;
	public static final int FIREWALL_PRICE_PERLEVEL		= 10;
	public static final String FIREWALL_DESCRIPTION 		= "Add a value to defense of gateway";
	
	public static final String IDS						= "IDS";
	public static final String IDS_NAME 				= "Prelude Detection System";
	public static final int IDS_DETECTION 				= 100;
	public static final int IDS_MAX_LEVEL				= 3;
	public static final boolean IDS_CUMULATIVE			= false;
	public static final int IDS_PRICE_PERLEVEL			= 10;
	public static final String IDS_DESCRIPTION		 	= "The system alerts a player of ana attack when it spent 75% of the hacking time. IDS allows to reduce this percentage";
	
	public static final String LOGCLEANER				= "LOGCLEANER";
	public static final String LOGCLEANER_NAME 			= "LCleaner";
	public static final int LOGCLEANER_MAX_LEVEL		= 3;
	public static final boolean LOGCLEANER_CUMULATIVE	= false;
	public static final int LOGCLEANER_PRICE_PERLEVEL	= 10;
	public static final String LOGCLEANER_DESCRIPTION		= "Allows to carry out ana attack with a reduced number of track along the path based on a level device";
	
	public static final String ANTIVIRUS				= "ANTIVIRUS";
	public static final String ANTIVIRUS_NAME 			= "Morton Antivirus";
	public static final int ANTIVIRUS_MAX_LEVEL			= 3;
	public static final boolean ANTIVIRUS_CUMULATIVE	= false;
	public static final int ANTIVIRUS_PRICE_PERLEVEL	= 10;
	public static final String ANTIVIRUS_DESCRIPTION 		= "Allows to protect from a Virus";

	
	public static final String VIRUS					= "VIRUS";
	public static final String VIRUS_NAME 				= "Blaster";
	public static final int VIRUS_MAX_LEVEL				= 3;
	public static final boolean VIRUS_CUMULATIVE		= false;
	public static final int VIRUS_PRICE_PERLEVEL		= 10;
	public static final String VIRUS_DESCRIPTION 		= "When it's equipped on a gateway, the attacker will lose one or more items on the gateway attacker, based on the specific antivirus";
	
	public static final String DEEPTHROAT				= "DEEPTHROAT";
	public static final String DEEPTHROAT_NAME 			= "Deep Throat";
	public static final int DEEPTHROAT_MAX_LEVEL		= 1;
	public static final boolean DEEPTHROAT_CUMULATIVE	= false;
	public static final int DEEPTHROAT_PRICE_PERLEVEL	= 10;
	public static final String DEEPTHROAT_DESCRIPTION 		= "Equipped on a gateway, if it's attacked it responds by informing the police about the identity of the attacker";

	
	/* Offensive Software */
	public static final String SNIFFER					= "SNIFFER";
	public static final String SNIFFER_NAME 			= "WireBass";
	public static final int SNIFFER_MAX_LEVEL			= 3;
	public static final boolean SNIFFER_CUMULATIVE		= false;
	public static final int SNIFFER_PRICE_PERLEVEL		= 10;
	public static final String SNIFFER_DESCRIPTION 		= "It allows to reveal a number of items equipped based on level on the attacked gateway";

	
	public static final String BRUTEFORCER				= "BRUTEFORCER";
	public static final String BRUTEFORCER_NAME 		= "Brutus";
	public static final int BRUTEFORCER_ATTACK_LEVEL	= 10;
	public static final int BRUTEFORCER_MAX_LEVEL		= 3;
	public static final boolean BRUTEFORCER_CUMULATIVE	= true;
	public static final int BRUTEFORCER_PRICE_PERLEVEL	= 10;
	public static final String BRUTEFORCER_DESCRIPTION 		= "Add a value to attack of gateway but it increases the relevance of the traces left along the path";

	
	public static final String DICTIONARY				= "DICTIONARY";
	public static final String DICTIONARY_NAME 			= "John The Rapper";
	public static final int DICTIONARY_ATTACK_LEVEL		= 10;
	public static final int DICTIONARY_MAX_LEVEL		= 3;
	public static final boolean DICTIONARY_CUMULATIVE	= true;
	public static final int DICTIONARY_PRICE_PERLEVEL	= 10;
	public static final String DICTIONARY_DESCRIPTION 		= "As the Bruteforcer, it adds a a value to attack of gateway, but it will leaves less relevance and increase the hakcing time";
	
	public static final String PROXY					= "PROXY";
	public static final String PROXY_NAME 				= "Thor Garlic Proxy";
	public static final int PROXY_LEV_1_2_MAX_HOPS		= 3;
	public static final int PROXY_LEV_3_MAX_HOPS		= 5;
	public static final int PROXY_MAX_LEVEL				= 3;
	public static final boolean PROXY_CUMULATIVE		= false;
	public static final int PROXY_PRICE_PERLEVEL		= 10;
	public static final String PROXY_DESCRIPTION 		= "Allows to expand the range of possible path";
	
	//***********************//
	
	/* Police constants */
	public static final int IA_THREAD_SLEEP_TIME 			= 50;
	public static final int DEFAULT_POLICE_RELEVANCE_THRS	= 4;
	public static final int POLICE_SLEEP_TIME				= 10*20;

	//**********************//
	
	/* Quests constants */
	public static final String QUEST_TYPE1 					= "CONQUISTA";
	public static final String QUEST_TYPE2					= "RUBA";
	public static final int QUEST1_REWARD					= 2;
	public static final int QUEST2_REWARD					= 3;

	


	



}
