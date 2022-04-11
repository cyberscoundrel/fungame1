using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{


	public Rigidbody head, feetl, feetr, head2;

	public GameObject headGameObject;

	public List<Rigidbody> holdingAppendages;

	public bool animate = true;

	public float speed = 10f;

	public float boyant, fdown;

	public Camera c;

	public float slow = 0.001f;

	public LimbIK IKInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    IEnumerator waiter()
    {
    	yield return new WaitForSeconds(3);
    }

    void FixedUpdate()
    {
    	if(true)
    	{

    		head.AddForce((GalaxyManager.getGravityVector(head.gameObject.transform)) * ((Time.fixedDeltaTime * boyant) / Mathf.Pow((Vector3.Distance(head.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		feetl.AddForce((-GalaxyManager.getGravityVector(head.gameObject.transform)) * ((Time.fixedDeltaTime * fdown) / Mathf.Pow((Vector3.Distance(head.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		feetr.AddForce((-GalaxyManager.getGravityVector(head.gameObject.transform)) * ((Time.fixedDeltaTime * fdown) / Mathf.Pow((Vector3.Distance(head.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));
    		/*Ray r3 = c.ScreenPointToRay(Input.mousePosition);
    		Ray r = new Ray(head2.transform.position, head2.transform.forward);
    		Ray r2 = new Ray(head2.transform.position, (GalaxyManager.getGravityVector(head2.transform)));
    		Debug.DrawRay(head2.transform.position, head2.transform.forward, Color.yellow);
    		Debug.DrawRay(head2.transform.position, (GalaxyManager.getGravityVector(head2.transform)), Color.red);
    		Vector3 p1 = r.GetPoint(50f);
    		float angle = Vector3.Angle(p1, (GalaxyManager.getGravityVector(head2.transform)));
    		Vector3 p2 = r2.GetPoint(Mathf.Cos(angle * Mathf.Deg2Rad) * 50f);
    		Vector3 p3 = p1 - p2;
    		Debug.DrawRay(head2.transform.position, p3, Color.cyan);
    		GetComponent<LineRenderer>().SetPosition(0, head2.transform.position);
    		GetComponent<LineRenderer>().SetPosition(1, r3.GetPoint(100f));

    		head2.transform.LookAt(r3.GetPoint(50f));
    		head2.transform.rotation = Quaternion.LookRotation(r3.GetPoint(50f), GalaxyManager.getGravityVector(head.transform));

    		if(Input.GetKey("w"))
    		{
    			Debug.Log("w");
    			head.AddForce(p3.normalized * ((Time.fixedDeltaTime * 40f) / Mathf.Pow((Vector3.Distance(gameObject.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));



    		}*/



    	}
    }

    public void HeadMouseFollow()
    {
    	Ray r3 = c.ScreenPointToRay(Input.mousePosition);
		Ray r = new Ray(head2.transform.position, head2.transform.forward);
		Ray r2 = new Ray(head2.transform.position, (GalaxyManager.getGravityVector(head2.transform)));
		Debug.DrawRay(head2.transform.position, head2.transform.forward, Color.yellow);
		Debug.DrawRay(head2.transform.position, (GalaxyManager.getGravityVector(head2.transform)), Color.red);
		Vector3 p1 = r.GetPoint(50f);
		float angle = Vector3.Angle(p1, (GalaxyManager.getGravityVector(head2.transform)));
		Vector3 p2 = r2.GetPoint(Mathf.Cos(angle * Mathf.Deg2Rad) * 50f);
		Vector3 p3 = p1 - p2;
		Debug.DrawRay(head2.transform.position, p3, Color.cyan);
		GetComponent<LineRenderer>().SetPosition(0, head2.transform.position);
		GetComponent<LineRenderer>().SetPosition(1, r3.GetPoint(100f));

		head2.transform.LookAt(r3.GetPoint(50f));
		//head2.transform.rotation = head2.transform.rotation * Quaternion.Slerp(head2.transform.rotation, Quaternion.LookRotation(r3.GetPoint(50f), GalaxyManager.getGravityVector(head.transform)), 0.1f);
		//head2.transform.rotation = Quaternion.LookRotation(r3.GetPoint(50f), GalaxyManager.getGravityVector(head.transform));

		if(Input.GetKey("w"))
		{
			Debug.Log("w");
			head.AddForce(p3.normalized * ((Time.fixedDeltaTime * 40f) / Mathf.Pow((Vector3.Distance(head.transform.position, GalaxyManager.gravityCenter.gameObject.transform.position)), 2)));



		}
    }

    public void LegIK()
    {

    	//walking ik

    }

    public Vector3 getMouseVector()
    {
    	Ray r3 = c.ScreenPointToRay(Input.mousePosition);
    	return r3.GetPoint(1000f);

    }

    //public GameObject get

    public GameObject getHead()
    {
    	Debug.Log("get head");
    	return headGameObject;
    }

    public Rigidbody[] getHoldingAppendages(int max)
    {

    	return holdingAppendages.ToArray();



    } 
}
