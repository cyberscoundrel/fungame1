using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public GameObject gameObject;

    public GameObject prefab;

    public Enemy(int baselvl, GameObject prefab)
    {
    	this.prefab = prefab;
    	gameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;
    }
}
