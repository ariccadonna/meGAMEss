
using UnityEngine;
using System.Collections;

public class fullScreenUI : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		ResizeSpriteToScreen ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void ResizeSpriteToScreen() 
	{
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		if (sr == null) return;

		gameObject.transform.localScale = new Vector3(1,1,1);
		
		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;


		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		
		transform.localScale = new Vector2(worldScreenWidth / width, worldScreenHeight / height);
        GameObject.Find("Manager").GetComponent<Manager>().setScale(transform.localScale);

	}
}
