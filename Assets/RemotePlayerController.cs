using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayerController : PlayerController
{

	Vector3 tempGunTransform = new Vector3();
	public GameObject tempParentObject;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(tempParentObject.transform.position, 2);

    }


    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
    	//tempParentObject.transform.position = Vector3.MoveTowards(tempParentObject.transform.position, tempParentObject.transform.forward, 1f * Time.deltaTime);
        
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
}
