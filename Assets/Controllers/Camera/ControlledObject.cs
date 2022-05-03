using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledObject : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject controlledObject;

    public static ControlledObject instance;

    public Camera controlledCamera;

    public Vector3 camOffset;

    public float camDistance = 1000f;

    public bool camFollow;

    public Vector3 camDirection;

    public bool topDown = false;

    public GameObject puppetStrings;

    public float movef = 25f;

    public float sen = 0.5f;

    public Quaternion cameraRotation;

    public Quaternion targetCameraRotation;

    public Vector3 targetCameraPosition;

    public GameObject currentCameraTransform;

    public GameObject targetCameraTransform;

    public Quaternion initCameraRotation;

    public float cx = 0f, cy = 0f;

    public GalaxyManager gm;

    public bool locked, firstPerson;

    public bool planetWatch;

    public bool regularCamera = false;

    public float camFactor = 0.01f;







    void Awake()
    {
    	Debug.Log("controlled object start");
    	Cursor.lockState = CursorLockMode.Locked;
    	instance = this;
        currentCameraTransform = new GameObject();
        targetCameraTransform = new GameObject();
        //initCameraRotation = null;


        
    }

    void start()
    {
        Debug.Log("controlledObject start");
    	firstPerson = false;
        initCameraRotation = controlledCamera.transform.rotation;
        //currentCameraTransform = new GameObject();

    	//controlledCamera.enabled = true;
    	//Camera.main.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(locked == false && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if(locked == true && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    	if(true)
    	{
    		if (Input.GetKeyDown("o")) 
    		{
	            Cursor.lockState = CursorLockMode.None;
	            locked = false;
        	}
        	if(Input.GetKeyDown("f"))
        	{
        		firstPerson = !firstPerson;

        	}

	        if (!locked && Input.GetMouseButtonDown(0)) 
	        {
                if(planetWatch != true)
                {
    	            Cursor.lockState = CursorLockMode.Locked;
    	            locked = true;
                }
	        }





    	}

        
    }

    void LateUpdate()
    {

    	if(topDown)
    	{
    		controlledCamera.transform.position = controlledObject.transform.position + (controlledObject.transform.up * 1f);
    		controlledCamera.transform.LookAt(controlledObject.transform.position);
    	}
    	else if(firstPerson)
    	{
    		float xmouse = Input.GetAxis("Mouse X"), ymouse = Input.GetAxis("Mouse Y");
    		cx = xmouse;
    		cy += ymouse;


    		cy = Mathf.Clamp(cy, -45f, 85f);


    		controlledCamera.transform.position = controlledObject.transform.position;

    		/*Debug.Log("cam up b4" + controlledCamera.transform.up);
    		Debug.Log("obj up b4" + GalaxyManager.getGravityVector(controlledObject.transform));
    		Debug.Log("obj pos" + controlledObject.transform.position);*/
    		Quaternion newq = Quaternion.FromToRotation(controlledCamera.transform.up.normalized, GalaxyManager.getGravityVector(controlledObject.transform).normalized);
    		Quaternion old = controlledCamera.transform.rotation;
    		controlledCamera.transform.rotation = Quaternion.FromToRotation(controlledCamera.transform.up.normalized, GalaxyManager.getGravityVector(controlledObject.transform).normalized) * controlledCamera.transform.rotation;
    		controlledCamera.transform.rotation *= Quaternion.Euler(-cy, cx, 0);
    		controlledCamera.transform.position += controlledCamera.transform.forward * 0.03f;
    	}
        else if(planetWatch)
        {

            float xmouse = Input.GetAxis("Mouse X"), ymouse = Input.GetAxis("Mouse Y");
            cx = xmouse;
            cy = ymouse;
            //Debug.Log("cx " + cx + "cy " + cy);
            //Debug.Log("planetWatch");

            controlledCamera.transform.position = controlledObject.transform.position;
            controlledCamera.transform.rotation *= (Quaternion.Euler(-cy, cx, 0));
            controlledCamera.transform.position -= controlledCamera.transform.forward * 3f;
            controlledCamera.transform.position += -controlledCamera.transform.right * 3f;
        }
    	else if(regularCamera)
    	{
    		if(controlledObject != null)
    		{
    			//Debug.Log("not null");
	    		float xmouse = Input.GetAxis("Mouse X"), ymouse = Input.GetAxis("Mouse Y");
	    		cx = xmouse;
	    		cy += ymouse;


	    		cy = Mathf.Clamp(cy, -45f, 85f);


	    		controlledCamera.transform.position = controlledObject.transform.position;

	    		/*Debug.Log("cam up b4" + controlledCamera.transform.up);
	    		Debug.Log("obj up b4" + GalaxyManager.getGravityVector(controlledObject.transform));
	    		Debug.Log("obj pos" + controlledObject.transform.position);*/
	    		Quaternion newq = Quaternion.FromToRotation(controlledCamera.transform.up.normalized, GalaxyManager.getGravityVector(controlledObject.transform).normalized);
	    		Quaternion old = controlledCamera.transform.rotation;
	    		controlledCamera.transform.rotation = Quaternion.FromToRotation(controlledCamera.transform.up.normalized, GalaxyManager.getGravityVector(controlledObject.transform).normalized) * controlledCamera.transform.rotation;
	    		controlledCamera.transform.rotation *= Quaternion.Euler(cy, cx, 0);
	    		controlledCamera.transform.position += Vector3.Scale(GalaxyManager.getGravityVector(controlledObject.transform).normalized, new Vector3(0.2f,0.2f,0.2f));
	    		controlledCamera.transform.position += Vector3.Scale(-controlledCamera.transform.right, new Vector3(0.1f,0.1f,0.1f));
	    		/*Debug.Log("cx" + cx);
	    		Debug.Log("cy" + cy);
	    		Debug.Log("proposed output" + (old * newq) * Vector3.up);
	    		Debug.Log("cam up" + controlledCamera.transform.up);
	    		Debug.Log("obj up" + GalaxyManager.getGravityVector(controlledObject.transform));
	    		Debug.Log("cam rot" + newq);*/

	    		if(cy < 0f)
	    		{
	    			Debug.DrawRay(controlledCamera.transform.position, -controlledCamera.transform.forward, Color.magenta);
	    			RaycastHit[] hit = Physics.RaycastAll(controlledCamera.transform.position, -controlledCamera.transform.forward, controlledCamera.transform.forward.magnitude);

	    			if(hit.Length > 0)
	    			{

	    				foreach(RaycastHit h in hit)
	    				{
		    				if(h.collider.gameObject.tag == "planet_object")
		    				{
		    					//Debug.Log(h.collider.gameObject.tag);
	    						//Debug.Log(h.collider.gameObject.name);
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


	    	}



    	}
        else
        {
            if(controlledObject != null)
            {
                //Debug.Log("not null");
                float xmouse = Input.GetAxis("Mouse X"), ymouse = Input.GetAxis("Mouse Y");
                cx = xmouse;
                cy += ymouse;


                cy = Mathf.Clamp(cy, -45f, 85f);


                //controlledCamera.transform.position = controlledObject.transform.position;
                currentCameraTransform.transform.position = controlledObject.transform.position;

                /*Debug.Log("cam up b4" + controlledCamera.transform.up);
                Debug.Log("obj up b4" + GalaxyManager.getGravityVector(controlledObject.transform));
                Debug.Log("obj pos" + controlledObject.transform.position);*/
                Quaternion newq = Quaternion.FromToRotation(controlledCamera.transform.up.normalized, GalaxyManager.getGravityVector(controlledObject.transform).normalized);
                Quaternion old = controlledCamera.transform.rotation;
                currentCameraTransform.transform.rotation = Quaternion.FromToRotation(currentCameraTransform.transform.up.normalized, GalaxyManager.getGravityVector(controlledObject.transform).normalized) * currentCameraTransform.transform.rotation;
                currentCameraTransform.transform.rotation *= Quaternion.Euler(cy, cx, 0);
                currentCameraTransform.transform.position += Vector3.Scale(GalaxyManager.getGravityVector(controlledObject.transform).normalized, new Vector3(0.2f,0.2f,0.2f));
                currentCameraTransform.transform.position += Vector3.Scale(-currentCameraTransform.transform.right, new Vector3(0.1f,0.1f,0.1f));
                //controlledCamera.transform.rotation = Quaternion.FromToRotation(controlledCamera.transform.up.normalized, GalaxyManager.getGravityVector(controlledObject.transform).normalized) * controlledCamera.transform.rotation;
                //controlledCamera.transform.rotation *= Quaternion.Euler(cy, cx, 0);
                //controlledCamera.transform.position += Vector3.Scale(GalaxyManager.getGravityVector(controlledObject.transform).normalized, new Vector3(0.2f,0.2f,0.2f));
                //controlledCamera.transform.position += Vector3.Scale(-controlledCamera.transform.right, new Vector3(0.1f,0.1f,0.1f));
                /*Debug.Log("cx" + cx);
                Debug.Log("cy" + cy);
                Debug.Log("proposed output" + (old * newq) * Vector3.up);
                Debug.Log("cam up" + controlledCamera.transform.up);
                Debug.Log("obj up" + GalaxyManager.getGravityVector(controlledObject.transform));
                Debug.Log("cam rot" + newq);*/

                if(cy < 0f)
                {
                    Debug.DrawRay(controlledCamera.transform.position, -controlledCamera.transform.forward, Color.magenta);
                    RaycastHit[] hit = Physics.RaycastAll(currentCameraTransform.transform.position, -currentCameraTransform.transform.forward, currentCameraTransform.transform.forward.magnitude);

                    if(hit.Length > 0)
                    {

                        foreach(RaycastHit h in hit)
                        {
                            if(h.collider.gameObject.tag == "planet_object")
                            {
                                //Debug.Log(h.collider.gameObject.tag);
                                //Debug.Log(h.collider.gameObject.name);
                                //controlledCamera.transform.position -= 0.9f * (controlledCamera.transform.position - h.point);
                                currentCameraTransform.transform.position -= 0.9f * (currentCameraTransform.transform.position - h.point);
                                break;
                            }
                            else
                            {
                                currentCameraTransform.transform.position -= 0.9f * currentCameraTransform.transform.forward;
                            }
                        }
                    }
                    else
                    {
                        currentCameraTransform.transform.position -= 0.9f * currentCameraTransform.transform.forward;
                    }
                }
                else
                {
                    currentCameraTransform.transform.position -= 0.9f * currentCameraTransform.transform.forward;
                }


                Debug.DrawRay(controlledCamera.transform.position, controlledCamera.transform.up.normalized, Color.green);
                Debug.DrawRay(controlledObject.transform.position, GalaxyManager.getGravityVector(controlledObject.transform).normalized);
                targetCameraTransform.transform.position = Vector3.Lerp(targetCameraTransform.transform.position, currentCameraTransform.transform.position, camFactor);
                targetCameraTransform.transform.rotation = Quaternion.Slerp(targetCameraTransform.transform.rotation, currentCameraTransform.transform.rotation, camFactor);
                controlledCamera.transform.position = targetCameraTransform.transform.position;
                controlledCamera.transform.rotation = targetCameraTransform.transform.rotation;
                //controlledCamera.transform.position = currentCameraTransform.transform.position;
                //controlledCamera.transform.rotation = currentCameraTransform.transform.rotation;


            }
        }


    }

    public static void setControlledObject(GameObject newGameObject)
    {
    	instance.controlledObject = newGameObject;

    }


}
