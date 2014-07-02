using UnityEngine;
using System.Collections;

public class NotificationManager : MonoBehaviour {

    public GameObject not_spr, newsText;
    SpriteRenderer spr_rend;
    ResourcesManager rm;
    NetworkManager nwm;
    Vector2 scale;
    float currentNewsWidth;
    float maxLeftBound;
    int counter = 0;

	// Use this for initialization
	void Start () {
        nwm = gameObject.GetComponent<NetworkManager>();
        rm = gameObject.GetComponent<ResourcesManager>();
        spr_rend = not_spr.GetComponent<SpriteRenderer>();
        scale = gameObject.GetComponent<Manager>().getScale();
        spr_rend.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        marquee(newsText);
        if (newsText.transform.position.x + currentNewsWidth < -currentNewsWidth)
            ResetNewsPosition();
	}

    void ResetNewsPosition()
    {
        counter++;
        if (counter > 2)
            DisplayNews("");
        newsText.transform.position = new Vector3(10.0F * scale.x, 5.1F * scale.y, 0);
        newsText.transform.localScale = new Vector3(0.25F, 0.25F, 0.25F);
    }
    public void DisplayNews(string action)
    {
        switch(action)
        {
            case "POLICETRACE":
                counter = 0;
                startNewsRoutine("The Police found some trace and is now looking for it's source.");
                break;
            case "POLICELOST":
                counter = 0;
                startNewsRoutine("The Police lost the traces it was following.");
                break;
            case "POLICERESET":
                counter = 0;
                startNewsRoutine("The Police consficated a Gateway.");
                break;
            case "LOSTAGW":
                counter = 0;
                startNewsRoutine("You lost a gateway.");
                break;
            case "":
                startNewsRoutine("");
                break;
            default:
                break;
        }

    }
    public void DisplayWindow(string action)
    {
        UILabel windowMessage = GameObject.Find("notification_text").GetComponent<UILabel>();
        spr_rend.sprite = rm.getNotificationSprite("SUCCESS");
        switch(action)
        {
            case "YOUWON":
                nwm.lost = true;
                StartCoroutine(nwm.endGame());
                startNotifRoutine("SUCCESSBUY", "You win.");
                break;
            case "SUCCESSBUY":
                startNotifRoutine("SUCCESS", "Item bought with success.");
                break;
            case "INSTALLED":
                startNotifRoutine("SUCCESS", "Software installed.");
                break;
            case "NOPATH":
                startNotifRoutine("ERROR", "No hacking path available.");
                break;
            case "HACKDISABLED":
                startNotifRoutine("ERROR","Your hack is disabled OR one of the selected Gateway is disabled for some seconds.");
                break;
            case "NOTOWNER":
                startNotifRoutine("ERROR","You dont own that Gateway.");
                break;
            case "ITEMNOTOWNED":
                startNotifRoutine("ERROR", "You dont own that item.");
                break;
            case "ACTIONSDISABLED":
                startNotifRoutine("ERROR", "All the actions have been disabled.");
                break;
            case "ITEMNOTAVAILABLE":
                startNotifRoutine("WARNING", "Item out of stock.");
                break;
            case "INVENTORYFULL":
                startNotifRoutine("WARNING","Your inventory is full.");
                break;
            case "NOTENOUGHMONEY":
                startNotifRoutine("WARNING", "You dont have enough money.");
                break;
            case "GTWNOTSELECTED":
                startNotifRoutine("WARNING", "You have to select an Gateway before installing an item.");
                break;
            case "NOTCUMULATIVE":
                startNotifRoutine("WARNING", "You cant install that item on that Gateway since it's already installed.");
                break;
            case "ITEMNOTPRESENT":
                startNotifRoutine("WARNING", "You need to install a previous version of that software.");
                break;
            case "ATTACKINGDISABLED":
                startNotifRoutine("WARNING", "Attacking gateway is disabled for some seconds. Try again later.");
                break;
            case "ATTACKEDISABLED":
                startNotifRoutine("WARNING", "Attacked gateway is disabled for some seconds. Try again later.");
                break;
            case "SELFHACKING":
                startNotifRoutine("WARNING", "You are trying to hack one of your gateways!");
                break;
            case "MISSINGORIGIN":
                startNotifRoutine("WARNING", "You have to select a starting gateway.");
                break;
            case "MISSINGDESTINATION":
                startNotifRoutine("WARNING", "You have to select a destination gateway.");
                break;
            case "LOST":
                nwm.lost = true;
                startNotifRoutine("WARNING", "You lost.");
                break;
            default:
                windowMessage.text = "";
                spr_rend.enabled = false;
                break;
                
        }
    }
    private void startNewsRoutine(string message)
    {
        newsText.transform.position = new Vector3(10.0F * scale.x, 5.1F * scale.y, 0);
        newsText.transform.localScale = new Vector3(newsText.transform.localScale.x * scale.x, newsText.transform.localScale.y * scale.y, 1F);
        UILabel windowMessage = GameObject.Find("news_text").GetComponent<UILabel>();
        windowMessage.text = message;
        currentNewsWidth = NGUIMath.CalculateRelativeWidgetBounds(newsText.transform).center.x;
        marquee(windowMessage.gameObject);
    }
    private void startNotifRoutine(string type, string message)
    {
        UILabel windowMessage = GameObject.Find("notification_text").GetComponent<UILabel>();
        spr_rend.sprite = rm.getNotificationSprite(type);

        switch (type)
        {
            case "ERROR":
                windowMessage.color = Color.red;
                break;
            case "WARNING":
                windowMessage.color = Color.yellow;
                break;
            case "SUCCESS":
            default:
                windowMessage.color = Color.green;
                break;
        }

        windowMessage.text = message;
        StartCoroutine(displayNotificationWin());
    }

    private IEnumerator displayNotificationWin()
    {
        spr_rend.enabled = true;
        yield return new WaitForSeconds(5f);
        GameObject.Find("notification_text").GetComponent<UILabel>().text = "";
        spr_rend.enabled = false;
  
    }

    private void marquee(GameObject item)
    {
           item.transform.Translate(Vector3.left * Time.deltaTime*1.5F);
    }
}
