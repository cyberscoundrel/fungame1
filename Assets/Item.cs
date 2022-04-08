using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Collectible
{

	public String name;

	//public int typeflag;



    public Item(int baselvl, GameObject prefab) : base(baselvl, prefab)
    {

    	gameObject.GetComponent<ItemController>().itemObject = this;

    	InitializePhysics();
    	
    }






}
