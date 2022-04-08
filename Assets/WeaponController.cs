using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

	public Weapon weaponObject;

	public List<Rigidbody> limbs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void onFire()
    {
    	Debug.Log("default behaviour");
    }
}
