using UnityEngine;
using System.Collections;

public class openingLogo : MonoBehaviour {
	

	// Use this for initialization
	IEnumerator Start () 
	{
		yield return StartCoroutine( WaitRoutine(5.0F) );
		Application.LoadLevel("loginScreen");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void onGUI()
	{
		
	}
	
	IEnumerator WaitRoutine(float seconds) {
		yield return new WaitForSeconds( seconds );
	}
}
