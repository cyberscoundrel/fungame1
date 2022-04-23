using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderPistol : PistolController
{

	public Animation a;

	public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("pistol controller is here");

        
    }

    // Update is called once per frame
    void Update()
    {
    	//Debug.Log("shoot update");
    	if(Input.GetKeyDown("p"))
    	{
    		Debug.Log("play shoot");
    		count++;
    		onFire();
    		//a.Play("discharge");
    	}
        
    }

    public override void onFire()
    {
    	a["discharge"].speed = 10f;
    	a.Play("discharge");
    }
}
