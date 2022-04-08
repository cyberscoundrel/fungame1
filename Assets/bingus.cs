using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bingus : ItemController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public float behavior(Player p)
    {

    	//TODO: noise for randomized damage modifier

    	return 2f;

    }
}
