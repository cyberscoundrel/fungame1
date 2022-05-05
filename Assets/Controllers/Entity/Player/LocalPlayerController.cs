using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerController : PlayerController
{
    
    

    

    void LateUpdate()
    {
    	if(selectedWeaponNum >= 0)
    	{

    		rds.IK[0].GetComponent<DitzelGames.FastIK.FastIKFabric>().enabled = true;
    		//Debug.Log("selecwep");
    	    Vector3 x = calculateWeaponPoint();
    	    //Debug.Log("calculated point" + (x));
    	    selectedWeapon.gameObject.transform.position = x;
    	    selectedWeapon.gameObject.transform.rotation = Quaternion.LookRotation(rds.getMouseVector(), GalaxyManager.getGravityVector(selectedWeapon.gameObject.transform));
    	    selectedWeapon.gameObject.transform.position += selectedWeapon.gameObject.transform.up * 0.05f;
    	    selectedWeapon.gameObject.transform.rotation = Quaternion.LookRotation(rds.getMouseVector(), GalaxyManager.getGravityVector(selectedWeapon.gameObject.transform));
    	    rds.IK[0].GetComponent<DitzelGames.FastIK.FastIKFabric>().Target.position = calculateWeaponPoint();


    	    //Debug.Log("set point" + selectedWeapon.gameObject.transform.position);


    	}
    	else
    	{
    		rds.IK[0].GetComponent<DitzelGames.FastIK.FastIKFabric>().enabled = false;
    		rds.IK[1].GetComponent<DitzelGames.FastIK.FastIKFabric>().enabled = false;
    	}


    }

    void FixedUpdate()
    {
        if(CliManager.instance != null)
        {
            //Debug.DrawLine(head.transform.position, PlayerManager.instance.playerOneScript.calculateWeaponPoint(), Color.gray);
            Message m = Message.Create(MessageSendMode.unreliable, (ushort)ClientToServerId.move);
            //m.AddUShort(player1.uTag);
            m.AddVector3(rds.hips.transform.position);
            m.AddQuaternion(rds.hips.transform.rotation);
            Debug.Log("sending pos " + rds.hips.transform.position);
            Debug.Log("sending rot " + rds.hips.transform.rotation);
            CliManager.client.Send(m);
        }
        else if(NetManager.instance != null)
        {
            Message m = Message.Create(MessageSendMode.unreliable, (ushort)ServerToClientId.playerMovement);
            m.AddUShort(PlayerManager.instance.player1.uTag);
            m.AddVector3(rds.hips.transform.position);
            m.AddQuaternion(rds.hips.transform.rotation);
            Debug.Log("sending pos " + rds.hips.transform.position);
            Debug.Log("sending rot " + rds.hips.transform.rotation);
            NetManager.instance.server.SendToAll(m);

        }
        /*if(CliManager.instance != null)
        {
            //Debug.DrawLine(head.transform.position, PlayerManager.instance.playerOneScript.calculateWeaponPoint(), Color.gray);
            Message m = Message.Create(MessageSendMode.unreliable, (ushort)ClientToServerId.move);
            //m.AddUShort(player1.uTag);
            m.AddVector3(gameObject.transform.position);
            m.AddQuaternion(gameObject.transform.rotation);
            CliManager.client.Send(m);
        }*/
    }

    public Vector3 calculateWeaponPoint(int wepDist)
    {

    	Ray r = new Ray(rds.head.transform.position, rds.getMouseVector());
    	return r.GetPoint(wepDist);




    	//return new Vector3(0,0,0);

    }

    public Vector3 calculateWeaponPoint()
    {
    	//Debug.Log("local rds " + rds);
    	Ray r = new Ray(rds.head.transform.position, rds.getMouseVector());
    	return r.GetPoint(weaponDistance);

    }


    //raycast interact

    public GameObject interactForward()
    {
    	RaycastHit hit;

    	Debug.Log("interact forward");

    	int layermask = LayerMask.GetMask("collectible");

    	if(Physics.Raycast(new Ray(rds.head.transform.position, rds.getMouseVector()), out hit, 1000f, layermask))
    	{
    		Debug.Log("hit");
    		return hit.transform.root.gameObject;

    	}
    	Debug.Log("ignored");
    	return null;
    	//return null;

    }

    public override void selectWeapon(int index)
    {
    	/*if(selectedWeapon != null)
    	{
    		selectedWeapon.DeactivatePhysicalInstance();

    		selectedWeapon = null;
    	}*/
    	Debug.Log("select weapoon " + index);
    	if(index < playerObject.getWeaponCount())
    	{
    		Debug.Log("index less than wep count");
    		if(selectedWeapon != null)
	    	{
	    		//selectedWeapon.DeactivatePhysicalInstance();
                CollectibleManager.DeactivatePhysicalInstance(selectedWeapon);

	    		selectedWeapon = null;
	    	}
    		selectedWeaponNum = index;
    		if(index >= 0)
    		{
                Debug.Log("select num " + index);
    			selectedWeapon = playerObject.getWeapon(index);
                CollectibleManager.InitializePhysics(selectedWeapon);
    			//selectedWeapon.InitializePhysics();
    			selectedWeapon.disableBigCollider();
    			//selectedWeapon.gameObject.transform.Find("bigcollider").gameObject.SetActive(false);
    		}
    	}
    }
}
