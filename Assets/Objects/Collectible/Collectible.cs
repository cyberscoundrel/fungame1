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

	public Collectible InitializePhysics()
    {
    	Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
    	Rigidbody p = gameObject.GetComponent<Rigidbody>();
    	foreach(Rigidbody r in rigidbodies)
    	{
    		GalaxyManager.AddRb(r);
    	}
    	foreach(Transform t in gameObject.transform)
    	{
    		t.gameObject.SetActive(true);
    	}
    	if(p != null)
    	{
    		GalaxyManager.AddRb(p);
    	}
    	return this;
    }

    public Collectible DeactivatePhysicalInstance()
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
    	return this;
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
    	Debug.Log("discarded");
    }

    /*public void ActivatePhysicalInstance()
    {
    	foreach(Transform t in gameObject.transform)
    	{
    		t.gameObject.SetActive(true);
    	}
    }*/

	public int stack;

	public float mult;

}
