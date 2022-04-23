using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //public GameObject gameObject;

    public GameObject prefab;

    public Enemy(int baselvl, GameObject prefab)
    {
    	collectibles = new List<Collectible>();
		gameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;
		Component[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
		foreach(Rigidbody r in rigidbodies)
		{
			GalaxyManager.AddRb(r);
		}
		gameObject.GetComponent<EnemyController>().enemyObject = this;
		baseDmg = 10f;
		baseSpd = 10f;
		baseTick = 1f;
		baseRegen = 1f;
		baseHealth = 100;
		level = baselvl;
    	this.prefab = prefab;
    	//gameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;
    }
}
