using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderPistol : PistolController
{

	public Animation a;

	public int count = 0;

    public GameObject projectileSource;


    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("pistol controller is here");

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(projectileSource.transform.position, gameObject.transform.forward, Color.white);
    	//Debug.Log("shoot update");
    	/*if(Input.GetKeyDown("p"))
    	{
    		Debug.Log("play shoot");
    		count++;
    		onFire();
    		//a.Play("discharge");
    	}*/
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("play shoot");
            count++;
            onFire();

        }
        
    }

    public override void onFire()
    {
    	a["discharge"].speed = 20f;
    	a.Play("discharge");
        RaycastHit rch;
        if(Physics.Raycast(projectileSource.transform.position, gameObject.transform.forward, out rch))
        {
            Debug.Log("hit a thing " + rch.transform.gameObject.name);
            EntityController e;
            e = rch.transform.root.gameObject.GetComponent<EntityController>();
            if(e != null)
            {
                Debug.Log("hit entity");
                Entity entity = e.entityObject;
                Debug.Log("entity health " + entity.getHealth());
                entity.setHealth(entity.getHealth() - 10);
                Debug.Log("entity new health " + entity.getHealth());

            }
        }
    }
}
