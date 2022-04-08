using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible
{

	public GameObject gameObject;

	public GameObject prefab;

	public int rbIndex;

	public int baselvl;

	protected Collectible(int baselvl, GameObject prefab)
	{
		this.baselvl = baselvl;
		this.prefab = prefab;
		gameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;

	}

	public Collectible InitializePhysics()
    {
    	Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
    	foreach(Rigidbody r in rigidbodies)
    	{
    		GalaxyManager.AddRb(r);
    	}
    	foreach(Transform t in gameObject.transform)
    	{
    		t.gameObject.SetActive(true);
    	}
    	return this;
    }

    public Collectible DeactivatePhysicalInstance()
    {
    	Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
    	foreach(Rigidbody r in rigidbodies)
    	{
    		GalaxyManager.RemoveRb(r);
    	}
    	foreach(Transform t in gameObject.transform)
    	{
    		t.gameObject.SetActive(false);
    	}
    	return this;
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
