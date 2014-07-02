using UnityEngine;
using System.Collections;

public class ResourcesManager : MonoBehaviour {

    private Sprite edu, gov, sci, mil, ban, bas;

    private Sprite antiv1, antiv2, antiv3;
    private Sprite brute1, brute2, brute3;
    private Sprite dict1, dict2, dict3;
    private Sprite fire1, fire2, fire3;
    private Sprite ids1, ids2, ids3;
    private Sprite log1, log2, log3;
    private Sprite proxy1, proxy2, proxy3;
    private Sprite sniffer1, sniffer2, sniffer3;
    private Sprite virus1, virus2, virus3;
    private Sprite deepthroat1;

    private Sprite empty;
    private Sprite unknown;

    private Sprite tut1, tut2, tut3, tut4;
    private Sprite not_suc, not_err, not_wrn;

    // Use this for initialization
    void Start () 
    {
        /* GW TYPE SETUP */
        edu = (Sprite)Resources.Load("Images/Gateways/edu", typeof(Sprite));   
        gov = (Sprite)Resources.Load("Images/Gateways/gov", typeof(Sprite));  
        sci = (Sprite)Resources.Load("Images/Gateways/sci", typeof(Sprite));  
        mil = (Sprite)Resources.Load("Images/Gateways/mil", typeof(Sprite));  
        ban = (Sprite)Resources.Load("Images/Gateways/ban", typeof(Sprite));  
        bas = (Sprite)Resources.Load("Images/Gateways/bas", typeof(Sprite)); 

        /* SW SETUP */
        antiv1 = (Sprite)Resources.Load("Images/Software/antivirus/antivirus1", typeof(Sprite));   
        antiv2 = (Sprite)Resources.Load("Images/Software/antivirus/antivirus2", typeof(Sprite));   
        antiv3 = (Sprite)Resources.Load("Images/Software/antivirus/antivirus3", typeof(Sprite));   

        brute1 = (Sprite)Resources.Load("Images/Software/bruteforcer/brute1", typeof(Sprite));   
        brute2 = (Sprite)Resources.Load("Images/Software/bruteforcer/brute2", typeof(Sprite));   
        brute3 = (Sprite)Resources.Load("Images/Software/bruteforcer/brute3", typeof(Sprite));   

        dict1 = (Sprite)Resources.Load("Images/Software/dictionary/dict1", typeof(Sprite));   
        dict2 = (Sprite)Resources.Load("Images/Software/dictionary/dict2", typeof(Sprite));   
        dict3 = (Sprite)Resources.Load("Images/Software/dictionary/dict3", typeof(Sprite)); 

        fire1 = (Sprite)Resources.Load("Images/Software/firewall/fire1", typeof(Sprite));   
        fire2 = (Sprite)Resources.Load("Images/Software/firewall/fire2", typeof(Sprite));   
        fire3 = (Sprite)Resources.Load("Images/Software/firewall/fire3", typeof(Sprite));   
        
        ids1 = (Sprite)Resources.Load("Images/Software/IDS/ids1", typeof(Sprite));   
        ids2 = (Sprite)Resources.Load("Images/Software/IDS/ids2", typeof(Sprite));   
        ids3 = (Sprite)Resources.Load("Images/Software/IDS/ids3", typeof(Sprite)); 

        log1 = (Sprite)Resources.Load("Images/Software/logcleaner/log1", typeof(Sprite));   
        log2 = (Sprite)Resources.Load("Images/Software/logcleaner/log2", typeof(Sprite));   
        log3 = (Sprite)Resources.Load("Images/Software/logcleaner/log3", typeof(Sprite)); 

        proxy1 = (Sprite)Resources.Load("Images/Software/proxy/proxy1", typeof(Sprite));   
        proxy2 = (Sprite)Resources.Load("Images/Software/proxy/proxy2", typeof(Sprite));   
        proxy3 = (Sprite)Resources.Load("Images/Software/proxy/proxy3", typeof(Sprite)); 

        sniffer1 = (Sprite)Resources.Load("Images/Software/sniffer/sniffer1", typeof(Sprite));   
        sniffer2 = (Sprite)Resources.Load("Images/Software/sniffer/sniffer2", typeof(Sprite));   
        sniffer3 = (Sprite)Resources.Load("Images/Software/sniffer/sniffer3", typeof(Sprite)); 

        virus1 = (Sprite)Resources.Load("Images/Software/virus/virus1", typeof(Sprite));   
        virus2 = (Sprite)Resources.Load("Images/Software/virus/virus2", typeof(Sprite));   
        virus3 = (Sprite)Resources.Load("Images/Software/virus/virus3", typeof(Sprite)); 

        tut1 = (Sprite)Resources.Load("Images/Tutorial/tut1", typeof(Sprite));   
        tut2 = (Sprite)Resources.Load("Images/Tutorial/tut2", typeof(Sprite));   
        tut3 = (Sprite)Resources.Load("Images/Tutorial/tut3", typeof(Sprite)); 
        tut4 = (Sprite)Resources.Load("Images/Tutorial/tut4", typeof(Sprite)); 

        not_suc = (Sprite)Resources.Load("Images/Notifications/notification_success", typeof(Sprite));   
        not_err = (Sprite)Resources.Load("Images/Notifications/notification_error", typeof(Sprite));   
        not_wrn = (Sprite)Resources.Load("Images/Notifications/notification_warning", typeof(Sprite)); 

        deepthroat1 = (Sprite)Resources.Load("Images/Software/deepthroat/deepthroat1", typeof(Sprite));

        empty = (Sprite)Resources.Load("Images/Software/bg", typeof(Sprite));

        unknown = (Sprite)Resources.Load("Images/Software/questionMark", typeof(Sprite));
    }

    public Sprite getGwImage(string type)
    {
        Sprite ret = null;

        switch (type)
        {
            case "Educational":
                ret =  edu;
                break;
            case "Government":
                ret =  gov;
                break;
            case "Scientific":
                ret =  sci;
                break;
            case "Military":
                ret =  mil;
                break;
            case "Bank":
                ret =  ban;
                break;
            default :
                ret = bas;
                break;
        }

        return ret;
    }

    public Sprite getSlotImage(string item)
    {
        Sprite ret = null;

        switch (item)
        {
            case "Morton_Antivirus_V1":
                ret = antiv1;
                break;
            case "Morton_Antivirus_V2":
                ret = antiv2;
                break;
            case "Morton_Antivirus_V3":
                ret = antiv3;
                break;

            case "Brutus_V1":
                ret = brute1;
                break;
            case "Brutus_V2":
                ret = brute2;
                break; 
            case "Brutus_V3":
                ret = brute3;
                break;

            case "John_The_Rapper_V1":
                ret = dict1;
                break;   
            case "John_The_Rapper_V2":
                ret = dict2;
                break;
            case "John_The_Rapper_V3":
                ret = dict3;
                break;

            case "FireWool_V1":
                ret = fire1;
                break;
            case "FireWool_V2":
                ret = fire2;
                break;
            case "FireWool_V3":
                ret = fire3;
                break;
            
          
            case "Prelude_Detection_System_V1":
                ret = ids1;
                break;
            case "Prelude_Detection_System_V2":
                ret = ids2;
                break;
            case "Prelude_Detection_System_V3":
                ret = ids3;
                break;

            case "LCleaner_V1":
                ret = log1;
                break;  
            case "LCleaner_V2":
                ret = log2;
                break;  
            case "LCleaner_V3":
                ret = log3;
                break;
            
            case "Thor_Garlic_Proxy_V1": 
                ret = proxy1;
                break;
            case "Thor_Garlic_Proxy_V2":
                ret = proxy2;
                break;
            case "Thor_Garlic_Proxy_V3":
                ret = proxy3;
                break;

            case "WireBass_V1":
                ret = sniffer1;
                break;
            case "WireBass_V2":
                ret = sniffer2;
                break;
            case "WireBass_V3":
                ret = sniffer3;
                break;

            case "Blaster_V1":
                ret = virus1;
                break;
            case "Blaster_V2":
                ret = virus2;
                break;
            case "Blaster_V3":
                ret = virus3;
                break;

            case "Deep_Throat_V1":
                ret = deepthroat1;
                break;

            default:
                ret = null;
                break;
        }

        return ret;
    }

    public Sprite getEmptySlot()
    {
        return empty;
    }

    public Sprite getUnknownSlot()
    {
        return unknown;
    }

    public Sprite getNextTutorialStep(int current_step)
    {
        Sprite ret = null;
        switch (current_step)
        {
            case 1:
                ret = tut2;
                break;
            case 2:
                ret = tut3;
                break;
            case 3:
                ret = tut4;
                break;
            case 4:
                ret = tut1;
                break;

            default:
                ret = tut1;
                break;
        }
        return ret;
    }


    public Sprite getNotificationSprite(string type)
    {
        Sprite ret = null;
        switch (type)
        {
            case "ERROR":
                ret = not_err;
                break;
            case "WARNING":
                ret = not_wrn;
                break;
            case "SUCCESS":
                ret = not_suc;
                break;

            default:
                ret = not_suc;
                break;
        }
        return ret;
    }


    // Update is called once per frame
    void Update () {
        
    }
}
