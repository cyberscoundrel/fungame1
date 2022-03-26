using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{

	//public Upright upright;

	//public Rigidbody head, feetl, feetr, chest;

	public Rigidbody head, feetl, feetr, head2;

	public bool animate = false;

	public float speed = 10f;

	public float boyant, fdown;

	public Camera c;

	public float slow = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    IEnumerator waiter()
    {
    	yield return new WaitForSeconds(3);
    }

    void OnCollisionEnter(Collision collision)
    {
    	Debug.Log("ragdoll collision");

    	if(!animate)
    	{
    		Debug.Log("collision galaxyCenter");
	    	if(collision.transform.gameObject.name == "gravityCenter")
	    	{
	    		animate = !animate;
	    		//upright.gameObject.SetActive(true);
	    		//upright.Animate();
	    	}
	    }

    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    	/*if(animate)
    	{
    		//Debug.Log("animating");
    		head.AddForce((GalaxyManager.getGravityVector(head.gameObject.transform)) * ((Time.fixedDeltaTime * boyant) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		//head.AddForce(transform.forward * ((Time.fixedDeltaTime * 4000f) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		feetl.AddForce((-GalaxyManager.getGravityVector(head.gameObject.transform)) * ((Time.fixedDeltaTime * fdown) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		feetr.AddForce((-GalaxyManager.getGravityVector(head.gameObject.transform)) * ((Time.fixedDeltaTime * fdown) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		float s = 100f;
    		Ray r3 = c.ScreenPointToRay(Input.mousePosition);
    		head.transform.LookAt(r3.GetPoint(1000f));
    		//head.transform.rotation = Quaternion.LookRotation(r3.GetPoint(1000f) - head.transform.position);
    		Ray r = new Ray(head.transform.position, head.transform.forward);
    		Ray r2 = new Ray(head.transform.position, (GalaxyManager.getGravityVector(head.transform)));
    		Debug.DrawRay(head.transform.position, head.transform.forward, Color.yellow);
    		Debug.DrawRay(head.transform.position, (GalaxyManager.getGravityVector(head.transform)), Color.red);
    		Vector3 p1 = r.GetPoint(1000f);
    		float angle = Vector3.Angle(p1, (GalaxyManager.getGravityVector(head.transform)));
    		Vector3 p2 = r2.GetPoint(Mathf.Cos(angle * Mathf.Deg2Rad) * 1000f);
    		Vector3 p3 = p1 - p2;
    		Debug.DrawRay(head.transform.position, p3, Color.cyan);
    		Debug.Log(angle);
    		Debug.Log(Mathf.Cos(angle * Mathf.Deg2Rad) * 1000f);
    		//head.AddForce(p3.normalized * speed);
    		/*Ray r3 = Camera.current.ScreenPointToRay(Input.mousePosition);
    		head.transform.LookAt(r3.GetPoint(1000f));*/
    		//head.transform.rotation = Quaternion.LookRotation(r3.GetPoint(1000f) - head.transform.position);
    		//GetComponent<LineRenderer>().SetPosition(0, head.transform.position);
    		//GetComponent<LineRenderer>().SetPosition(1, r3.GetPoint(1000f));




    		//head.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.fixedDeltaTime * s);


    	//}
        
    //}

    void FixedUpdate()
    {
    	if(animate)
    	{
    		//Debug.Log("animating");
    		head.AddForce((GalaxyManager.getGravityVector(head.gameObject.transform)) * ((Time.fixedDeltaTime * boyant) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		//head.AddForce(transform.forward * ((Time.fixedDeltaTime * 4000f) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		feetl.AddForce((-GalaxyManager.getGravityVector(head.gameObject.transform)) * ((Time.fixedDeltaTime * fdown) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		feetr.AddForce((-GalaxyManager.getGravityVector(head.gameObject.transform)) * ((Time.fixedDeltaTime * fdown) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		//float s = 100f;
    		Ray r3 = c.ScreenPointToRay(Input.mousePosition);
    		//chest.transform.LookAt(r3.GetPoint(1000f));
    		//chest.transform.rotation = Quaternion.LookRotation(r3.GetPoint(1000f) - head.transform.position);
    		Ray r = new Ray(head2.transform.position, head2.transform.forward);
    		Ray r2 = new Ray(head2.transform.position, (GalaxyManager.getGravityVector(head2.transform)));
    		Debug.DrawRay(head2.transform.position, head2.transform.forward, Color.yellow);
    		Debug.DrawRay(head2.transform.position, (GalaxyManager.getGravityVector(head2.transform)), Color.red);
    		Vector3 p1 = r.GetPoint(50f);
    		float angle = Vector3.Angle(p1, (GalaxyManager.getGravityVector(head2.transform)));
    		Vector3 p2 = r2.GetPoint(Mathf.Cos(angle * Mathf.Deg2Rad) * 50f);
    		Vector3 p3 = p1 - p2;
    		Debug.DrawRay(head2.transform.position, p3, Color.cyan);
    		//Debug.Log(angle);
    		//Debug.Log(Mathf.Cos(angle * Mathf.Deg2Rad) * 50f);

    		//transform.LookAt(p3);

    		//GetComponent<Rigidbody>().AddForce(p3.normalized * speed * Time.fixedDeltaTime);
    		//head.AddForce(p3.normalized * speed);
    		/*Ray r3 = Camera.current.ScreenPointToRay(Input.mousePosition);
    		head.transform.LookAt(r3.GetPoint(1000f));*/
    		//head.transform.rotation = Quaternion.LookRotation(r3.GetPoint(1000f) - head.transform.position);
    		GetComponent<LineRenderer>().SetPosition(0, head2.transform.position);
    		GetComponent<LineRenderer>().SetPosition(1, r3.GetPoint(100f));

    		head2.transform.LookAt(r3.GetPoint(50f));
    		head2.transform.rotation = Quaternion.LookRotation(r3.GetPoint(50f), GalaxyManager.getGravityVector(head.transform));

    		if(Input.GetKey("w"))
    		{
    			Debug.Log("w");
    			//controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.forward * 5f, ForceMode.Impulse);
    			head.AddForce(p3.normalized * ((Time.fixedDeltaTime * 40f) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));



    		}





    		//head.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.fixedDeltaTime * s);


    	}
    	//float s = 1f;
    	//head.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * s);
    }

    public Rigidbody getHead()
    {
    	return head;
    }
}
