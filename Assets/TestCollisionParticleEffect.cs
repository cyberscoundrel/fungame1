using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollisionParticleEffect : MonoBehaviour
{

	public bool effect;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
    	if(effect)
    	{
    		if(!gameObject.GetComponent<ParticleSystem>().isPlaying)
    		{
    			//Debug.Log("isPlaying");
    			gameObject.GetComponent<ParticleSystem>().Play();
    		}
    		else
    		{
    			gameObject.GetComponent<ParticleSystem>().Stop();
    		}
    	}
        
    }

    void OnCollisionEnter(Collision c)
    {
    	//Debug.Log("collide with " + c.gameObject.tag);

    	if(c.gameObject.tag == "testcollisioncube")
    	{
    		Debug.Log("testcollisioncube");
    		effect = !effect;
    	}

    }
}
