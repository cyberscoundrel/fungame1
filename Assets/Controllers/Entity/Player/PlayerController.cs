using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//public GameObject ragdollObject;

	public Player playerObject;
	public RagdollScript rds;
	public int selectedWeaponNum = -1;

	public Weapon selectedWeapon;

	public float weaponDistance = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
    	rds = gameObject.GetComponent<RagdollScript>();
    	/*if(playerObject == null)
    	{
    		playerObject = Player.createNewPlayer;
    	}*/
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    void LateUpdate()
    {
    	/*if(selectedWeaponNum >= 0)
    	{
    		//Debug.Log("selecwep");
    	    Vector3 x = calculateWeaponPoint();
    	    //Debug.Log("calculated point" + (x));
    	    selectedWeapon.gameObject.transform.position = x;
    	    selectedWeapon.gameObject.transform.rotation = Quaternion.LookRotation(rds.getMouseVector(), GalaxyManager.getGravityVector(selectedWeapon.gameObject.transform));

    	    //Debug.Log("set point" + selectedWeapon.gameObject.transform.position);


    	}*/


    }

    public void SetPlayerObject(Player p)
    {
    	playerObject = p;
    }


    //raycast fire
    public void PrimativeFire()
    {


    }

    /*public Vector3 calculateWeaponPoint(int wepDist)
    {

    	Ray r = new Ray(rds.head.transform.position, rds.getMouseVector());
    	return r.GetPoint(wepDist);




    	//return new Vector3(0,0,0);

    }

    public Vector3 calculateWeaponPoint()
    {
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

    public void selectWeapon(int index)
    {
    	/*if(selectedWeapon != null)
    	{
    		selectedWeapon.DeactivatePhysicalInstance();

    		selectedWeapon = null;
    	}*/
    	/*Debug.Log("select weapoon " + index);
    	if(index < playerObject.getWeaponCount())
    	{
    		Debug.Log("index less than wep count");
    		if(selectedWeapon != null)
	    	{
	    		selectedWeapon.DeactivatePhysicalInstance();

	    		selectedWeapon = null;
	    	}
    		selectedWeaponNum = index;
    		if(index >= 0)
    		{
    			selectedWeapon = playerObject.getWeapon(index);
    			selectedWeapon.InitializePhysics();
    			selectedWeapon.disableBigCollider();
    			//selectedWeapon.gameObject.transform.Find("bigcollider").gameObject.SetActive(false);
    		}
    	}
    }*/
}
