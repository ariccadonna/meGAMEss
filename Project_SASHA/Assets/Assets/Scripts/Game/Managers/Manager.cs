using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    private NetworkManager networkManager;
    private CollisionManager collisionManager;
    private TextManager textManager;
    private NotificationManager notificationManager;
    private GameObject panelManager;

    private Vector2 scale;
	// Use this for initialization
	void Start () {
        networkManager = gameObject.GetComponent<NetworkManager>();
        collisionManager = gameObject.GetComponent<CollisionManager>();
        textManager = gameObject.GetComponent<TextManager>();
        notificationManager = gameObject.GetComponent<NotificationManager>();

        panelManager = GameObject.Find("Panel");
        scaleChilds(panelManager);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public TextManager getTextManager()
    {
        return this.textManager;
    }

    public NetworkManager getNetworkManager()
    {
        return this.networkManager;
    }

    public NotificationManager getNotificationManager()
    {
        return this.notificationManager;
    }

    public CollisionManager getCollisionManager()
    {
        return this.collisionManager;
    }

    public void setScale(Vector2 resizedScale)
    {
        scale = resizedScale;
    }

    public Vector2 getScale()
    {
        return this.scale;
    }

    void scaleChilds(GameObject obj)
    {

        foreach (Transform child in obj.transform)
        {
            child.transform.localScale = new Vector3(child.transform.localScale.x * this.getScale().x, child.transform.localScale.y * this.getScale().y, 1F);
            scaleChilds(child.gameObject);
        }
        
    }

    public void startParticle(Gateway gw)
    {
        gw.particleSystem.Play();
    }
    
    public IEnumerator stopParticle(Gateway gw, float time)
    {
        yield return new WaitForSeconds(time);
        gw.particleSystem.Stop();
    }
    
    public void stopParticle(Gateway gw)
    {
        gw.particleSystem.Stop();
    }

    public void stopAllParticle()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Gateway"))
            g.particleSystem.Stop();
    }
}
