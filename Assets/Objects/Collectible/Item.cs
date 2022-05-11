using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Collectible
{

	public String name;

    public override String prefabDir { get {return "Prefabs/items";}}

	//public int typeflag;



    //public Item(int baselvl, GameObject prefab) : base(baselvl, prefab)
    public Item(int baselvl, uint prefab) : base(baselvl, prefab)
    {

    	gameObject.GetComponent<ItemController>().setManager(this);

    	CollectibleManager.InitializePhysics(this);

    	typeFlag = 0x01;
    	
    }

    //public static new Collectible generateCollectible(int baselvl, GameObject prefab)
    public static new Collectible generateCollectible(int baselvl, uint prefab)
    {
    	return new Item(baselvl, prefab);
    }

    //void Start()
    //{
    	//Collectible.funcMap.Add(1,this.generateCollectible);
    //}






}
