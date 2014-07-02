using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class DragItem : MonoBehaviour
{
    Vector3 startPosition;
    void Start()
    {
        startPosition = gameObject.transform.position;
    }
    void OnMouseDrag()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = gameObject.transform.position.z;
        gameObject.transform.position = point;
        Screen.showCursor = false;
    }
    
    void OnMouseUp()
    {
        Screen.showCursor = true;
        GameObject mgr = GameObject.Find("Manager");
        Gateway gtw = mgr.GetComponent<TextManager>().gtw;
        bool isGtwSelected = (gtw != null);

        if (!isGtwSelected)
        {
            gameObject.transform.position = startPosition;
            mgr.GetComponent<NotificationManager>().DisplayWindow("GTWNOTSELECTED");
            return;
        }
        if (gtw.getOwner()!= mgr.GetComponent<NetworkManager>().getCurrentPlayer())
        {
            gameObject.transform.position = startPosition;
            mgr.GetComponent<NotificationManager>().DisplayWindow("NOTOWNER");
            return;
        }


        if (gameObject.renderer.bounds.Intersects(GameObject.Find("Slot1").renderer.bounds) && isGtwSelected)
            mgr.GetComponent<NetworkManager>().installSoftware(gameObject.transform.name, gtw.getState());
        else if(gameObject.renderer.bounds.Intersects(GameObject.Find("Slot2").renderer.bounds) && isGtwSelected)
            mgr.GetComponent<NetworkManager>().installSoftware(gameObject.transform.name, gtw.getState());
        else if(gameObject.renderer.bounds.Intersects(GameObject.Find("Slot3").renderer.bounds) && isGtwSelected)
            mgr.GetComponent<NetworkManager>().installSoftware(gameObject.transform.name, gtw.getState());
        else
            gameObject.transform.position = startPosition;
    }
}
