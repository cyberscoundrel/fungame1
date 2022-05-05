using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayerController : PlayerController
{

	Vector3 tempGunTransform = new Vector3();
	public GameObject tempParentObject;
    public GameObject targetTransformObject;

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(tempParentObject.transform.position, 2);

    }*/


    // Start is called before the first frame update
    void Start()
    {
        rds.IK[0].GetComponent<DitzelGames.FastIK.FastIKFabric>().enabled = false;
        rds.IK[1].GetComponent<DitzelGames.FastIK.FastIKFabric>().enabled = false;
        targetTransformObject = new GameObject();
        targetTransformObject.transform.position = rds.hips.transform.position;
        targetTransformObject.transform.rotation = rds.hips.transform.rotation;

        
    }

    // Update is called once per frame
    void Update()
    {
    	//tempParentObject.transform.position = Vector3.MoveTowards(tempParentObject.transform.position, tempParentObject.transform.forward, 1f * Time.deltaTime);
        
    }

    void FixedUpdate()
    {
        rds.hips.transform.position = Vector3.Lerp(rds.hips.transform.position, targetTransformObject.transform.position, 0.3f * Time.fixedDeltaTime);
        rds.hips.transform.rotation = Quaternion.Slerp(rds.hips.transform.rotation, targetTransformObject.transform.rotation, 0.3f * Time.fixedDeltaTime);
        //rds.hips.transform.position = targetTransformObject.transform.position;
        //rds.hips.transform.rotation = targetTransformObject.transform.rotation;

    }

    public void setPositionData(Vector3 playerPosition, Vector3 playerVelocity, Quaternion playerRotation, Vector3 gunPosition, Vector3 gunVelocity, Quaternion gunRotation)
    {
    	//rds.head.velocity = playerVelocity;
    	Debug.Log("got from server " + playerPosition);
    	Debug.Log("position before " + tempParentObject.transform.position);
    	tempParentObject.transform.position = playerPosition;
    	Debug.Log("after " + tempParentObject.transform.position);


    	//tempGunTransform.position = gunPosition;
    	tempGunTransform = gunPosition;

    }

    public override void setTargetTransform(Vector3 newVec, Quaternion newQuat)
    {
        targetTransformObject.transform.position = newVec;
        targetTransformObject.transform.rotation = newQuat;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetTransformObject.transform.position, 0.1f);
    }
}
