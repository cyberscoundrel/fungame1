using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbIK : MonoBehaviour
{
	public Transform t;

	public List<GameObject> bones;
	public GameObject target;

	public List<float> boneLengths;
	public List<Vector3> jointLocations;
	public List<ConfigurableJoint> cJoints;

	void Start()
	{
		boneLengths = new List<float>();
		jointLocations = new List<Vector3>();
		cJoints = new List<ConfigurableJoint>();
		calculateBonesAndJoints();

	}

	void Update()
	{


	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach(GameObject g in bones)
		{
			ConfigurableJoint c = g.GetComponent<ConfigurableJoint>();
			if(c != null)
			{
				Gizmos.DrawSphere(c.transform.position, 0.1f);
				Debug.Log(c.transform.position);
			}
		}

	}

	void calculateBoneLenths()
	{

	}

	void calculateBonesAndJoints()
	{
		/*foreach(GameObject g in bones)
		{

			if(g.transform.childCount > 0)
			{
				boneLengths.add(g.transform.GetChild(0).localPosition.magntude);
			}



		}*/

	}


    
}
