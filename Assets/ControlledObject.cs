using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledObject : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject controlledObject;

    public Rigidbody rb;

    public Camera controlledCamera;

    public bool firstPerson;

    public Vector3 camOffset;

    public float camDistance = 1000f;

    public bool camFollow;

    public Vector3 camDirection;

    public bool topDown = false;






    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

    	if(true)
    	{


    		if(Input.GetKeyDown("w"))
    		{
    			//Debug.Log("w");
    			controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.forward * 1f, ForceMode.Impulse);


    		}
    		if(Input.GetKeyDown("a"))
    		{
    			//Debug.Log("a");
    			controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.right * -1f, ForceMode.Impulse);
    		}
    		if(Input.GetKeyDown("s"))
    		{
    			//Debug.Log("s");
    			controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.forward * -1f, ForceMode.Impulse);
    		}
    		if(Input.GetKeyDown("d"))
    		{
    			//Debug.Log("d");
    			controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.right * 1f, ForceMode.Impulse);
    		}
    		Quaternion r = Quaternion.FromToRotation(controlledObject.GetComponent<Rigidbody>().transform.up * 1000f, GalaxyManager.getGravityVector(controlledObject.GetComponent<Rigidbody>()));
    		Vector3 vr = new Vector3(r.x, r.y, r.z);

    		rb.AddTorque(vr * (10f* vr.magnitude));

    		rb.constraints = RigidbodyConstraints.FreezeRotationY;

    		/*controlledCamera.transform.position = controlledObject.transform.position - (controlledObject.transform.forward * 2000f);

    		Debug.Log(controlledObject.transform.forward * 20f);
    		Debug.Log( "object " + controlledObject.transform.position + " camera " + controlledCamera.transform.position);

    		controlledCamera.transform.LookAt(controlledObject.transform.position);

    		Debug.Log(controlledObject.transform.forward * 20f);
    		Debug.Log( "lookat object " + controlledObject.transform.position + " camera " + controlledCamera.transform.position);

    		controlledCamera.transform.position = new Vector3(controlledObject.transform.position.x, controlledObject.transform.position.y, controlledObject.transform.position.z);*/

    		//Quaternion r = Quaternion.LookRotation(controlledObject.GetComponent<Rigidbody>().transform.right, GalaxyManager.getGravityVector(controlledObject.GetComponent<Rigidbody>()));
    		//controlledObject.GetComponent<Rigidbody>().MoveRotation(r * 0.00001f);




    	}

        
    }

    void LateUpdate()
    {
    	if(topDown)
    	{
    		controlledCamera.transform.position = controlledObject.transform.position + (controlledObject.transform.up * 1f);
    		controlledCamera.transform.LookAt(controlledObject.transform.position);
    	}
    	else
    	{
    		controlledCamera.transform.position = controlledObject.transform.position - (controlledObject.transform.forward * 1f);

    		controlledCamera.transform.LookAt(controlledObject.transform.position);
			controlledCamera.transform.rotation = Quaternion.Slerp(controlledCamera.transform.rotation, controlledObject.transform.rotation, 1f);
    	}

		//Debug.Log(controlledObject.transform.forward * 20f);
		//Debug.Log( "object " + controlledObject.transform.position + " camera " + controlledCamera.transform.position);

		//controlledCamera.transform.LookAt(controlledObject.transform.position);
		//controlledCamera.transform.rotation = Quaternion.Slerp(controlledCamera.transform.rotation, controlledObject.transform.rotation, 1f);

		//Debug.Log(controlledObject.transform.forward * 20f);
		//Debug.Log( "lookat object " + controlledObject.transform.position + " camera " + controlledCamera.transform.position);

		//controlledCamera.transform.position = new Vector3(controlledCamera.transform.position.x, controlledCamera.transform.position.y, controlledCamera.transform.position.z);
    }

    public void setControlledObject(GameObject newGameObject)
    {
    	controlledObject = newGameObject;

    }


}
