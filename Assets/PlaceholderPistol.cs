using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderPistol : PistolController
{

	public Animation a;

    //variables that don't change through out code
    [Header("Gun Settings")]
    public float fireRate = .1f;
    public int clipSize = 30; //not nec
    public int reservedAmmo = 270; //not nec
    //variables that change through out code
    bool canShoot;
    int currentAmmo;
    //int currentAmmo;
    int ammoInReserve; //not in use yet

    public int count = 0;
    // Start is called before the first frame update

    public GameObject temp;

    void Start()
    {
    	Debug.Log("pistol controller is here");
        currentAmmo = clipSize;
        ammoInReserve = reservedAmmo;
        canShoot = true;

    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("shoot update");
    	if(Input.GetKeyDown("p"))
    	{
    		Debug.Log("play shoot");
    		count++;
    		a.Play("discharge");
    	}*/

        if (Input.GetKeyDown("f") && canShoot && currentAmmo > 0) //if left mouse button, canshoot, and there is ammo
        {
            canShoot = false;
            currentAmmo--;
            StartCoroutine(ShootGun()); //coroutine runs off of "main thread" which means we can work with timers without freezing unity
        }
        else if (Input.GetKeyDown(KeyCode.R) && currentAmmo < clipSize && ammoInReserve > 0)
        {
            int amountNeeded = clipSize - currentAmmo; //holds amount we can add to clip, ex: 30 - 27, so we need 3 bullets
            if (amountNeeded >= ammoInReserve) //is the amount we need is larger than what we have
            {
                currentAmmo += ammoInReserve;
                ammoInReserve -= amountNeeded;
            }
            else //amount needed is less then reserve
            {
                currentAmmo = clipSize;
                ammoInReserve -= amountNeeded;
            }

        }

    }

    public override void onFire()
    {
    	a.Play("discharge");
    }

    IEnumerator ShootGun() //necessary for coroutines
    {
        //StartCoroutine(MuzzleFlash());
        //add a laser?
        //DetermineRecoil();

        RayCastForEnemy(); // for enemy
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }


    void RayCastForEnemy()
    {
        RaycastHit hit;

        //if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 1 << LayerMask.NameToLayer("shootable"))) //the transform parts make it so that the it detects the very middle of the camera
        if (Physics.Raycast(temp.transform.position, gameObject.transform.forward, out hit, LayerMask.GetMask("shootable"))) //the transform parts make it so that the it detects the very middle of the camera
        {
            try
            {
                Debug.Log("Hit enemy");

                //knockback
                //Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                //rb.constraints = RigidbodyConstraints.None;
                //rb.AddForce(transform.parent.transform.forward * 500); //adds force away from player, adjust 500 to be a variable dependant on weapon
            }
            catch
            {
                //Debug.Log("Miss!");
            }
        }
    }

}
