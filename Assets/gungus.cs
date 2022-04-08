using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gungus : ItemController
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
    	p.health += 1;
    	p.health %= p.baseHealth;
    	return 1f;

    }
}
