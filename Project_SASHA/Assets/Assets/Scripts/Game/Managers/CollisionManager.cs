using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {
    public GameObject shopUI;
    public GameObject shopText;
	public GameObject escapeMenu = null;
    public GameObject tutorial = null;
    private GameObject collided = null;
    private GameObject lastCollided = null;
    private NotificationManager nm;
    RaycastHit2D hit;
    Ray ray;
    private NetworkManager nwm;
	// Use this for initialization
	void Start () 
    {
        nwm = gameObject.GetComponent<NetworkManager>();
        nm = gameObject.GetComponent<NotificationManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonUp(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
            if(hit.collider != null && nwm.gameEnded == false)
            {
                if (hit.transform.tag == "Gateway")
                {
                    Gateway gw = hit.transform.GetComponent<Gateway>();
                    gameObject.GetComponent<TextManager>().printGwInfo(gw);
                    gameObject.GetComponent<Manager>().stopAllParticle();
                    gameObject.GetComponent<Manager>().startParticle(gw);
                    StartCoroutine(gameObject.GetComponent<Manager>().stopParticle(gw, 6.0f));
                    gameObject.GetComponent<NetworkManager>().setEnd(gw.getState());

					GameObject.Find("To").GetComponent<UILabel>().text = gw.getName()+ "\n Def: " + gw.getDef();
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
            if(hit.collider != null && nwm.gameEnded == false)
            {
                if (hit.transform.tag == "Gateway")
                {
                    Gateway gw = hit.transform.GetComponent<Gateway>();
                    gameObject.GetComponent<TextManager>().printGwInfo(gw);
                    gameObject.GetComponent<Manager>().stopAllParticle();
                    gameObject.GetComponent<Manager>().startParticle(gw);
                    StartCoroutine(gameObject.GetComponent<Manager>().stopParticle(gw, 6.0f));
                    gameObject.GetComponent<NetworkManager>().setStart(gw.getState());
					GameObject.Find("From").GetComponent<UILabel>().text = gw.getName() + "\n ATK: " + gw.getAtk();
                }
                else
                {
                    switch (hit.transform.name)
                    {   

                        case "ShopButton":
                            toggleShop();
                            break;
                        case "BlasterButton":
                            collided = GameObject.Find("blaster");
                            lastCollided = collided;
                            break;
                        case "BrutusButton":
                            collided = GameObject.Find("brutus");
                            lastCollided = collided;
                            break;
                        case "DeepThroatButton":
                            collided = GameObject.Find("deepthroat");
                            lastCollided = collided;
                            break;
                        case "FireWoolButton":
                            collided = GameObject.Find("firewool");
                            lastCollided = collided;
                            break;
                        case "JohnButton":
                            collided = GameObject.Find("john");
                            lastCollided = collided;
                            break;
                        case "LcleanerButton":
                            collided = GameObject.Find("lcleaner");
                            lastCollided = collided;
                            break;
                        case "MortonButton":
                            collided = GameObject.Find("morton");
                            lastCollided = collided;
                            break;
                        case "PreludeButton":
                            collided = GameObject.Find("PDS");
                            lastCollided = collided;
                            break;
                        case "ThorButton":
                            collided = GameObject.Find("thor");
                            lastCollided = collided;
                            break;
                        case "WireBassButton":
                            collided = GameObject.Find("wirebass");
                            lastCollided = collided;
                            break;
                        case "shop_item_lv_1":
                            if(lastCollided.transform.name == "PDS" || lastCollided.transform.name == "wirebass" || lastCollided.transform.name == "deepthroat" )
                                gameObject.GetComponent<NotificationManager>().DisplayWindow("ITEMNOTAVAILABLE");
                            else
                                gameObject.GetComponent<NetworkManager>().buyItem(lastCollided.transform.name+"1");
                            break;
                        case "shop_item_lv_2":
                            if(lastCollided.transform.name == "PDS" || lastCollided.transform.name == "wirebass")
                                gameObject.GetComponent<NotificationManager>().DisplayWindow("ITEMNOTAVAILABLE");
                            else
                                if(lastCollided.transform.name != "deepthroat")
                                    gameObject.GetComponent<NetworkManager>().buyItem(lastCollided.transform.name+"2");
                            break;
                        case "shop_item_lv_3":
                            if(lastCollided.transform.name == "PDS" || lastCollided.transform.name == "wirebass")
                                gameObject.GetComponent<NotificationManager>().DisplayWindow("ITEMNOTAVAILABLE");
                            else
                                if(lastCollided.transform.name != "deepthroat")
                                    gameObject.GetComponent<NetworkManager>().buyItem(lastCollided.transform.name+"3");
                            break;
                        case "back":
                            escapeMenu.SetActive(false);
                            changeGatewayCollider();
                            break;
                        case "tutorial":
                            tutorial.GetComponent<SpriteRenderer>().enabled = true;
                            tutorial.GetComponent<BoxCollider2D>().enabled = true;
                            GameObject.Find("GUICamera").GetComponent<Camera>().enabled = false;
                            break;
                        case "leave":
                            gameObject.GetComponent<NetworkManager>().leaveGame();
                            break;
                        case "exit":
                            gameObject.GetComponent<NetworkManager>().CloseApplication();
                            break;
                        case "TutorialUI":
                            string tut_name = tutorial.GetComponent<SpriteRenderer>().sprite.name;
                            int curr_step = int.Parse(tut_name.Substring(tut_name.Length-1, 1));
                            Sprite next_step = gameObject.GetComponent<ResourcesManager>().getNextTutorialStep(curr_step);
                            if(tut_name == "tut4")
                            {
                                tutorial.GetComponent<SpriteRenderer>().enabled = false;
                                tutorial.GetComponent<BoxCollider2D>().enabled = false;
                                GameObject.Find("GUICamera").GetComponent<Camera>().enabled = true;
                            }
                            tutorial.GetComponent<SpriteRenderer>().sprite = next_step;
                            break;
                        case "hack":
                            if(nwm.getStart().Equals(""))
                                nm.DisplayWindow("MISSINGORIGIN");
                            else 
                                if(nwm.getEnd().Equals(""))
                                    nm.DisplayWindow("MISSINGDESTINATION");
                            else
                                nwm.SendHackRequest(nwm.getStart(),nwm.getEnd(), false);
                            break;
                        case "neutralize":
                            if(nwm.getStart().Equals(""))
                                nm.DisplayWindow("MISSINGORIGIN");
                            else
                                if(nwm.getEnd().Equals(""))
                                    nm.DisplayWindow("MISSINGDESTINATION");
                            else
                                nwm.SendHackRequest(nwm.getStart(), nwm.getEnd(), true);
                            break;
                        default:
                            //collided = null;
                            break;

                    }
                
                    GameObject[] gos;
                    gos = GameObject.FindGameObjectsWithTag("shopItem");
                    foreach (GameObject go in gos) 
                        go.GetComponent<SpriteRenderer>().enabled = false;

                    if(collided != null)
                        collided.GetComponent<SpriteRenderer>().enabled = true;
                    //collided.SetActive(!collided.activeInHierarchy);
                }
            }
            else
            {
                if(nwm.gameEnded == true)
                {
                    if (hit.transform.tag == "exitGame")
                        nwm.SendLeaveRoom();
                }
            }
        }
	}

    public void toggleShop()
    {
            shopUI.SetActive(!shopUI.activeInHierarchy);
            changeGatewayCollider();
            if (shopUI.activeInHierarchy)
            {
                collided = GameObject.Find("blaster");
                lastCollided = collided;
            }
    }
    public void changeGatewayCollider()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Gateway"))
            go.collider2D.enabled = !go.collider2D.enabled;

    }
}
