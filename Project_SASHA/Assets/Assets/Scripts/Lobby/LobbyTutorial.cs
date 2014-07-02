using UnityEngine;
using System.Collections;

public class LobbyTutorial : MonoBehaviour 
	{
	public GameObject shopUI;
	public GameObject shopText;
	public GameObject escapeMenu = null;
	public GameObject tutorial = null;
	private GameObject collided = null;
	private GameObject lastCollided = null;
	RaycastHit2D hit;
	Ray ray;
	private NetworkManager nwm;
	// Use this for initialization
	void Start () 
	{
		nwm = gameObject.GetComponent<NetworkManager>();
	}

	// Update is called once per frame
	void Update () 
		{
		if (Input.GetMouseButtonUp(0))
			{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
			if(hit.collider != null)
			{
				switch (hit.transform.name)
				{   
					case "tutorial":
						tutorial.GetComponent<SpriteRenderer>().enabled = true;
						tutorial.GetComponent<BoxCollider2D>().enabled = true;
						GameObject.Find("GUICamera").GetComponent<Camera>().enabled = false;
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
					default:
					break;
				}
			}
		}
	}
}

