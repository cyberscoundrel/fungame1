using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gungus : ItemController
{

	Player p;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void behavior()
    {
    	p.setHealth(p.getHealth() + 1);
    	p.setHealth(p.getHealth() % p.baseHealth);
    	//return 1f;

    }
}
