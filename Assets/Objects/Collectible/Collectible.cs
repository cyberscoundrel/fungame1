using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible
{

	public GameObject gameObject;

	public GameObject prefab;

	public int rbIndex;

	public int baselvl;

	public int baseTick;

	//public int uTag;
    public ushort uTag;

	public int typeFlag = 0x0;
    //public short tFlag;

	//public Player holder;
	public Entity holder;

	public bool pickupEffect = true;

	public bool discardEffect = true;

    public bool activeInWorld = false;

	/*public static Dictionary<String, int> collectibleTypeFlags = new Dictionary<String, int>()
	{
		{"Collectible", 0x0},
		{"Weapon", 0x2},
		{"Item", 0x4},
		{"Active", 0x8},
		{"OnAction", 0x10}
	};*/



	protected Collectible(int baselvl, GameObject prefab)
	{
		this.baselvl = baselvl;
		this.prefab = prefab;
		gameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;

	} 

    public void disableBigCollider()
    {
    	GameObject g = gameObject.transform.Find("bigcollider").gameObject;
    	if(g != null)
    	{
    		g.SetActive(false);
    	}
    }

    public void enableBigCollider()
    {
    	GameObject g = gameObject.transform.Find("bigcollider").gameObject;
    	if(g != null)
    	{
    		g.SetActive(true);
    	}
    }

    public virtual void behavior()
    {

    }

    public virtual void PickUp(Entity p)
    {
    	//Debug.Log("picked up");
    	holder = p;
    	if(pickupEffect)
    	{
    		onPickUp();
    	}


    }

    public virtual void onPickUp()
    {
        CollectibleManager.DeactivatePhysicalInstance(this);
        CollectibleController cc = gameObject.GetComponent<CollectibleController>();
        if(cc != null)
        {
            cc.enabled = true;
        }
    	Debug.Log("picked up");

    }

    public virtual void Discard()
    {
    	//Debug.Log("dropped");
    	holder = null;
    	if(discardEffect)
    	{
    		onDiscard();
    	}

    }

    public virtual void onDiscard()
    {
        CollectibleManager.InitializePhysics(this);
        CollectibleController cc = gameObject.GetComponent<CollectibleController>();
        if(cc != null)
        {
            cc.enabled = false;
        }
    	Debug.Log("discarded");
    }

    /*public void ActivatePhysicalInstance()
    {
    	foreach(Transform t in gameObject.transform)
    	{
    		t.gameObject.SetActive(true);
    	}
    }*/

    public Collectible InitializePhysics()
    {
        if(!activeInWorld)
        {
            Debug.Log("InitializePhysics");
            Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            Rigidbody p = gameObject.GetComponent<Rigidbody>();
            foreach(Rigidbody r in rigidbodies)
            {
                GalaxyManager.AddRb(r);
            }
            foreach(Transform t in gameObject.transform)
            {
                Debug.Log("transform in gameObject " + t.gameObject.name + "SetActive true");
                t.gameObject.SetActive(true);
                //t.gameObject.enable = true;
                //t.gameObject.activeSelf = true;
            }
            if(p != null)
            {
                GalaxyManager.AddRb(p);
            }
            activeInWorld = true;
        }
        return this;
    }

    public Collectible DeactivatePhysicalInstance()
    {
        if(activeInWorld)
        {
            Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            Rigidbody p = gameObject.GetComponent<Rigidbody>();
            foreach(Rigidbody r in rigidbodies)
            {
                GalaxyManager.RemoveRb(r);
            }
            foreach(Transform t in gameObject.transform)
            {
                t.gameObject.SetActive(false);
            }
            if(p != null)
            {
                GalaxyManager.RemoveRb(p);

            }
            activeInWorld = false;
        }
        return this;
    }

    void OnDisable()
    {
        Debug.Log("collectible disabled");
    }

    void OnEnable()
    {
        Debug.Log("collectible enabled");
    }

	public int stack;

	public float mult;

}
