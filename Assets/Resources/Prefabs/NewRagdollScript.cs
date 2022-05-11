using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRagdollScript : MonoBehaviour
{

	public Rigidbody up;
	public Rigidbody downl, downr;

    public Rigidbody hips;

    public EnemyController ec;

	//public GameObject shit;

	public float upFloat = 0;
	public float dFloat = 0;
    public float moveFloat = 10f;
    // Start is called before the first frame update
    void Start()
    {
        ec = GetComponent<EnemyController>();
        
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

        if(ec.aggro != null)
        {
            float a = Vector3.Angle((GalaxyManager.getGravityVector(hips.transform.position)).normalized, (GalaxyManager.getGravityVector(ec.aggro.gameObject.GetComponent<RagdollScript>().hips.transform.position)).normalized);

            //Debug.Log("angle " + a);

            float s = Mathf.Abs((1f/Mathf.Cos((a * Mathf.PI) / 180f)) * (GalaxyManager.getGravityVector(hips.transform.position)).magnitude);

            //Debug.Log("mag " + s);

            Vector3 p = (((GalaxyManager.getGravityVector(ec.aggro.gameObject.GetComponent<RagdollScript>().hips.transform.position)).normalized) * s) + GalaxyManager.gravityCenter.gameObject.transform.position;


            hips.AddForce((p - hips.transform.position).normalized * moveFloat);
            //float a = Vector3.Angle((GalaxyManager.getGravityVector(hips.transform.position) - GalaxyManager.gravityCenter.gameObject.transform.position).normalized, (GalaxyManager.getGravityVector(ec.aggro.gameObject.transform.position) - GalaxyManager.gravityCenter.gameObject.transform.position).normalized);

            //float s = (1f/Mathf.Cos(a)) * ((GalaxyManager.getGravityVector(hips.transform.position) - GalaxyManager.gravityCenter.gameObject.transform.position).magnitude);

            //Vector3 p = (((GalaxyManager.getGravityVector(ec.aggro.gameObject.transform.position) - GalaxyManager.gravityCenter.gameObject.transform.position).normalized) * s) + GalaxyManager.gravityCenter.gameObject.transform.position;




        }
    	//shit.transform.position = up.gameObject.transform.position - GalaxyManager.gravityCenter.gameObject.transform.position;
    }

    void OnDrawGizmos()
    {

        if(ec.aggro != null)
        {
            float a = Vector3.Angle((GalaxyManager.getGravityVector(hips.transform.position)).normalized, (GalaxyManager.getGravityVector(ec.aggro.gameObject.GetComponent<RagdollScript>().hips.transform.position)).normalized);

            Debug.Log("angle " + a);

            float s = Mathf.Abs((1f/Mathf.Cos((a * Mathf.PI) / 180f)) * (GalaxyManager.getGravityVector(hips.transform.position)).magnitude);

            Debug.Log("mag " + s/(GalaxyManager.getGravityVector(hips.transform.position)).magnitude);

            Vector3 p = (((GalaxyManager.getGravityVector(ec.aggro.gameObject.GetComponent<RagdollScript>().hips.transform.position)).normalized) * s) + GalaxyManager.gravityCenter.gameObject.transform.position;

            Gizmos.color = Color.red;

            Gizmos.DrawLine(hips.transform.position, p);




        }

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
