using System;
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

    public GameObject puppetStrings;

    public bool puppetteering = false;

    public bool puppetReady = false;

    public float movef = 25f;

    public float sen = 0.5f;

    public Quaternion cameraRotation;

    public float cx = 0f, cy = 0f;

    public GalaxyManager gm;

    public bool locked;







    void Start()
    {
    	Cursor.lockState = CursorLockMode.Locked;
    	//cameraRotation = new Quaternion(0f, 0f, 0f, 0f);
    	//controlledCamera.transform.position = controlledObject.transform.position;
    	//controlledCamera.transform.rotation = Quaternion.Slerp(controlledCamera.transform.rotation, controlledObject.transform.rotation, 1f);


        
    }

    // Update is called once per frame
    void Update()
    {

    	if(true)
    	{
    		if (Input.GetKeyDown("o")) 
    		{
	            Cursor.lockState = CursorLockMode.None;
	            locked = false;
        	}

	        if (!locked && Input.GetMouseButtonDown(0)) 
	        {
	            Cursor.lockState = CursorLockMode.Locked;
	            locked = true;
	        }


			/*if(puppetStrings && puppetStrings.activeSelf)
			{
				//puppetStrings.SetActive(true);
				if(Input.GetKeyDown("w"))
	    		{
	    			puppetStrings.transform.RotateAround(GalaxyManager.gravityCenter.gameObject.transform.position, Vector3.up, 2f * Time.deltaTime);
	    			//Debug.Log("w");



	    		}
	    		if(Input.GetKeyDown("a"))
	    		{
	    			//Debug.Log("a");

	    		}
	    		if(Input.GetKeyDown("s"))
	    		{
	    			//Debug.Log("s");

	    		}
	    		if(Input.GetKeyDown("d"))
	    		{
	    			//Debug.Log("d");

	    		}
				if(Input.GetKeyDown("c"))
				{
					topDown = !topDown;
				}



			}*/
			//else
			//bUpdate();
			{
				/*if(Input.GetKeyDown("w"))
	    		{
	    			Debug.Log("w");
	    			//controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.forward * 5f, ForceMode.Impulse);
	    			controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.forward * Time.deltaTime * movef);



	    		}*/
	    		if(Input.GetKeyDown("a"))
	    		{
	    			//Debug.Log("a");
	    			controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.right * -5f, ForceMode.Impulse);
	    		}
	    		if(Input.GetKeyDown("s"))
	    		{
	    			//Debug.Log("s");
	    			controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.forward * -5f, ForceMode.Impulse);
	    		}
	    		if(Input.GetKeyDown("d"))
	    		{
	    			//Debug.Log("d");
	    			controlledObject.GetComponent<Rigidbody>().AddForce(rb.transform.right * 5f, ForceMode.Impulse);
	    		}
				if(Input.GetKeyDown("c"))
				{
					topDown = !topDown;
				}
	    		//Quaternion r = Quaternion.FromToRotation(controlledObject.GetComponent<Rigidbody>().transform.up * 1000f, GalaxyManager.getGravityVector(controlledObject.GetComponent<Rigidbody>()));
	    		//Vector3 vr = new Vector3(r.x, r.y, r.z);

	    		//rb.AddTorque(vr * (10f* vr.magnitude));

	    		//rb.constraints = RigidbodyConstraints.FreezeRotationY;
	    	}

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
    		float xmouse = Input.GetAxis("Mouse X"), ymouse = Input.GetAxis("Mouse Y");
    		cx = xmouse;
    		cy += ymouse;
    		//cx = 0.01f;

    		//Vector3 objectEulerAngles = controlledObject.transform.eulerAngles;

    		cy = Mathf.Clamp(cy, -45f, 85f);

    		//Vector3 objectEulerAngles = controlledObject.transform.eulerAngles;

    		//controlledCamera.transform.rotation = Quaternion.FromToRotation(controlledCamera.transform.up, GalaxyManager.getGravityVector(controlledObject.transform));
    		//controlledCamera.transform.eulerAngles = objectEulerAngles;
    		//controlledCamera.transform.eulerAngles += new Vector3(cy, cx, 0);
    		//controlledCamera.transform.position = controlledObject.transform.position - (controlledCamera.transform.forward * 1f);

    		controlledCamera.transform.position = controlledObject.transform.position;
    		//controlledCamera.transform.rotation = Quaternion.FromToRotation(controlledCamera.transform.up, GalaxyManager.getGravityVector(controlledObject.transform));
    		//controlledCamera.transform.eulerAngles += new Vector3(cx, cy, 0);
    		//controlledCamera.transform.Rotate(Quaternion.FromToRotation(controlledCamera.transform.up, GalaxyManager.getGravityVector(controlledObject.transform)).eulerAngles);
    		Debug.Log("cam up b4" + controlledCamera.transform.up);
    		Debug.Log("obj up b4" + GalaxyManager.getGravityVector(controlledObject.transform));
    		Quaternion newq = Quaternion.FromToRotation(controlledCamera.transform.up.normalized, GalaxyManager.getGravityVector(controlledObject.transform).normalized);
    		Quaternion old = controlledCamera.transform.rotation;
    		controlledCamera.transform.rotation = Quaternion.FromToRotation(controlledCamera.transform.up.normalized, GalaxyManager.getGravityVector(controlledObject.transform).normalized) * controlledCamera.transform.rotation;
    		controlledCamera.transform.rotation *= Quaternion.Euler(cy, cx, 0);
    		Debug.Log("cx" + cx);
    		Debug.Log("cy" + cy);
    		Debug.Log("proposed output" + (old * newq) * Vector3.up);
    		Debug.Log("cam up" + controlledCamera.transform.up);
    		Debug.Log("obj up" + GalaxyManager.getGravityVector(controlledObject.transform));
    		Debug.Log("cam rot" + newq);
    		//controlledCamera.transform.RotateAround(controlledCamera.transform.position, controlledCamera.transform.up, cx * Time.deltaTime);
    		//controlledCamera.transform.RotateAround(controlledCamera.transform.position, controlledCamera.transform.right, cy * Time.deltaTime);

    		//controlledCamera.transform.eulerAngles += new Vector3(0, cx, 0);
    		if(cy < 0f)
    		{
    			Debug.DrawRay(controlledCamera.transform.position, -controlledCamera.transform.forward, Color.magenta);
    			RaycastHit[] hit = Physics.RaycastAll(controlledCamera.transform.position, -controlledCamera.transform.forward, controlledCamera.transform.forward.magnitude);
    			//Debug.Log(hit.collider.gameObject.tag);
    			//if(Physics.Raycast(controlledCamera.transform.position, -controlledCamera.transform.forward, out hit, controlledCamera.transform.forward.magnitude))
    			if(hit.Length > 0)
    			{
    				//Debug.Log("camray hit");

    				//Debug.Log(hit.collider.gameObject.tag);
    				//Debug.Log(hit.collider.gameObject.name);
    				foreach(RaycastHit h in hit)
    				{
	    				if(h.collider.gameObject.tag == "planet_object")
	    				{
	    					Debug.Log(h.collider.gameObject.tag);
    						Debug.Log(h.collider.gameObject.name);
	    					controlledCamera.transform.position -= 0.9f * (controlledCamera.transform.position - h.point);
	    					break;
	    				}
	    				else
	    				{
	    					controlledCamera.transform.position -= 0.9f * controlledCamera.transform.forward;
	    				}
    				}
    			}
    			else
    			{
    				controlledCamera.transform.position -= 0.9f * controlledCamera.transform.forward;
    			}
    		}
    		else
    		{
    			controlledCamera.transform.position -= 0.9f * controlledCamera.transform.forward;
    		}


    		Debug.DrawRay(controlledCamera.transform.position, controlledCamera.transform.up.normalized, Color.green);
    		Debug.DrawRay(controlledObject.transform.position, GalaxyManager.getGravityVector(controlledObject.transform).normalized);


    		//controlledCamera.transform.RotateAround(controlledObject.transform.position, );

    		//controlledCamera.transform.LookAt(controlledObject.transform);
    		//controlledCamera.transform.rotation = Quaternion.LookRotation(controlledObject.transform.position, GalaxyManager.getGravityVector(controlledObject.transform));

    		//controlledCamera.transform.rotation = 
    		/*controlledCamera.transform.position = controlledObject.transform.position - (controlledObject.transform.forward * 1f);
    		controlledCamera.transform.LookAt(controlledObject.transform.position);
    		Quaternion rot = Quaternion.FromToRotation(controlledCamera.transform.up * 1000f, GalaxyManager.getGravityVector(controlledCamera.transform) * -1f);
    		controlledCamera.transform.rotation = Quaternion.Slerp(controlledCamera.transform.rotation, rot, 1f);*/


    		//controlledCamera.transform.position = controlledObject.transform.position - (controlledObject.transform.forward * 1f);
    		//Vector3 lr = controlledObject.transform.localEulerAngles;
    		//lr.y += xmouse * sen;

    		//controlledCamera.transform.position = 

    		//controlledCamera.transform.LookAt(controlledObject.transform.position);
			//controlledCamera.transform.rotation = Quaternion.Slerp(controlledCamera.transform.rotation, controlledObject.transform.rotation, 1f);


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
