using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaceholderPistol : PistolController
{

	public Animation a;
    public int clipSize = 30; //not nec
    public int reservedAmmo = 270; //not nec
    //variables that change through out code
    bool canShoot;
    int currentAmmo;
    int ammoInReserve;

    // Start is called before the first frame update
    public LineRenderer laserLine; //awake function?
    public float laserDuration = .05f;
    public Transform laserorigin;

    public int count = 0;

    public GameObject projectileSource;

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
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
        Debug.DrawRay(projectileSource.transform.position, gameObject.transform.forward, Color.white);
    	//Debug.Log("shoot update");
    	/*if(Input.GetKeyDown("p"))
    	{
    		Debug.Log("play shoot");
    		count++;
    		onFire();
    		//a.Play("discharge");
    	}*/
        if(Input.GetMouseButtonDown(0) && canShoot && currentAmmo > 0)
        {
            Debug.Log("play shoot");
            count++;
            //canShoot = false;//for fully automatic
            currentAmmo--;
            GameObject.Find("AmmunitionText").GetComponent<AmmoController>().UpdateAmmoTexts(currentAmmo, ammoInReserve);
            //UpdateAmmoText();
            onFire();
            StartCoroutine(ShootLaser());//if shootlaster goes here, we need to set position to the gun

        }
        else if (Input.GetKeyDown(KeyCode.R) && currentAmmo < clipSize && ammoInReserve > 0) //reload
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
            GameObject.Find("AmmunitionText").GetComponent<AmmoController>().UpdateAmmoTexts(currentAmmo, ammoInReserve);
            //UpdateAmmoText();

        }

    }

    public override void onFire()
    {
    	a["discharge"].speed = 20f;
    	a.Play("discharge");
        RaycastHit rch;
        laserLine.SetPosition(0, laserorigin.position);
        laserLine.SetPosition(1, transform.forward * 5000);
        if (Physics.Raycast(projectileSource.transform.position, gameObject.transform.forward, out rch))
        {
            Debug.Log("hit a thing " + rch.transform.gameObject.name);
            /*EntityController e;
            e = rch.transform.root.gameObject.GetComponent<EntityController>();
            if(e != null)
            {
                Debug.Log("hit entity");
                Entity entity = e.entityObject;
                Debug.Log("entity health " + entity.getHealth());
                entity.setHealth(entity.getHealth() - 10);
                Debug.Log("entity new health " + entity.getHealth());


            }*/

            EnemyController e;
            e = rch.transform.root.gameObject.GetComponent<EnemyController>();
            if(e != null)
            {
                Debug.Log("hit enemy");
                EnemyManager.DamageEnemy(getManager().holder.uTag, e.entityObject.uTag, 10);

                //knockback
                Rigidbody rb = rch.transform.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(transform.forward * 300);
            }
        }
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;

    }

    /*
    private void UpdateAmmoText()
    {
        ammo_text = GameObject.Find("AmmoText").GetComponent<Text>();
        //ammo_text = GameObject.Find("AmmunitionText").GetComponent<Text>();
        //GameObject.Find("AmmoText").GetComponent<Text>() = $"{currentAmmo}/{ammoInReserve}";
        ammo_text.text = $"{currentAmmo}/{ammoInReserve}";

    }
    */

    /*
 *     IEnumerator ShootGun() //for fully automatic weapons
{
    //StartCoroutine(MuzzleFlash());
    //DetermineRecoil();
    //animation for shooting
    //add line renderer

    RayCastForEnemy(); // for enemy
    yield return new WaitForSeconds(fireRate);
    canShoot = true;
}
 * 
 */
}
