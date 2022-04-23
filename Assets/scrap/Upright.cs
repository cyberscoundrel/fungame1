using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upright : MonoBehaviour
{


	public RagdollScript rdoll;

	//public int timeForGetup;

	//public bool freeFall = true;

	//public ControlledObject co;



    // Start is called before the first frame update
    /*\\void Start()
    {

        
    }*/

    /*IEnumerator waiter()
    {
    	yield return new WaitForSeconds(timeForGetup);

    	/*Vector3 v = GalaxyManager.getGravityVector(rdoll.gameObject.transform);

    	transform.position = rdoll.gameObject.transform.position + -v;
    	transform.up = transform.position + -v;*/

    	//co.puppetReady = true;
    //}

    /*void OnEnable()
    {
    	Debug.Log("upright");

    	StartCoroutine(waiter());


    }

    void OnDisable()
    {
    	Debug.Log("disable");

    }*/

    /*public void Animate()
    {
    	freeFall = !freeFall;
    	if(!freeFall)
    	{
    		gameObject.GetComponent<SpringJoint>().connectedBody = rdoll.getHead();



    	}
    }*/

    void setRdoll(RagdollScript newRdoll)
    {
    	if(rdoll)
    	{
    		//rdoll.upright = null;
    	}

    	rdoll = newRdoll;
    	//rdoll.upright = this;
    }

    // Update is called once per frame
    void Update()
    {

    	if(true)
    	{
    		Vector3 v = GalaxyManager.getGravityVector(rdoll.getHead().transform);

	    	//transform.position = rdoll.getHead().transform.position + (0.5f * v);
	    	transform.up = v;
	    	transform.position = rdoll.getHead().transform.position + (rdoll.getHead().transform.up * 0.1f);
	    	transform.up = GalaxyManager.getGravityVector(rdoll.gameObject.transform);
    	}
        
    }
}
