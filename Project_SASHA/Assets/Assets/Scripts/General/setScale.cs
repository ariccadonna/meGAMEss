using UnityEngine;
using System.Collections;

public class setScale : MonoBehaviour {
    public GameObject UI = null;
	// Use this for initialization
	void Start () {
        float scaleY = UI.transform.localScale.y;
        Debug.Log(scaleY);
        gameObject.transform.localScale *= scaleY;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
