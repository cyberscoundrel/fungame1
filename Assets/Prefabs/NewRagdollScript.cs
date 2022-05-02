using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRagdollScript : MonoBehaviour
{

	public Rigidbody up;
	public Rigidbody downl, downr;

	//public GameObject shit;

	public float upFloat = 0;
	public float dFloat = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
    	up.AddForce(GalaxyManager.getGravityVector(up.transform) * upFloat);
    	Debug.DrawRay(up.gameObject.transform.position, GalaxyManager.getGravityVector(up.gameObject.transform.position), Color.magenta);
    	downr.AddForce(-GalaxyManager.getGravityVector(downr.transform) * dFloat);
    	downl.AddForce(-GalaxyManager.getGravityVector(downl.transform) * dFloat);
    	//shit.transform.position = up.gameObject.transform.position - GalaxyManager.gravityCenter.gameObject.transform.position;
    }

    void OnDrawGizmos()
    {
    	/*Gizmos.color = Color.red;
    	Gizmos.DrawLine(downr.gameObject.transform.position, -GalaxyManager.getGravityVector(downr));
    	Gizmos.DrawLine(downl.gameObject.transform.position, -GalaxyManager.getGravityVector(downl));
    	Gizmos.DrawSphere(up.transform.position, 0.1f);
    	Gizmos.color = Color.yellow;
    	Debug.Log("up " + up.gameObject.transform.position);
    	Debug.Log("planet " + GalaxyManager.gravityCenter.gameObject.transform.position);
    	//Debug.Log("shit " + shit.transform.position);
    	//Debug.Log("shit no planet" + (shit.transform.position + GalaxyManager.gravityCenter.gameObject.transform.position));
    	Gizmos.DrawLine(up.gameObject.transform.position, GalaxyManager.getGravityVector(up));
    	Gizmos.DrawLine(GalaxyManager.gravityCenter.gameObject.transform.position, up.gameObject.transform.position);
    	Gizmos.color = Color.blue;
    	Gizmos.DrawLine(up.gameObject.transform.position, up.gameObject.transform.position - GalaxyManager.gravityCenter.gameObject.transform.position);
    	Gizmos.DrawSphere(up.transform.position + GalaxyManager.gravityCenter.gameObject.transform.position.normalized, 0.1f);
    	Gizmos.color = Color.green;
    	Gizmos.DrawSphere(GalaxyManager.gravityCenter.gameObject.transform.position, 0.1f);
    	Gizmos.color = Color.white;
    	Gizmos.DrawSphere(gameObject.transform.position, 0.1f);*/
    }
}
