using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collectible
{

	//public GameObject gameObject;

	//public Mesh m;

	//public MeshFilter mf;

	//public MeshRenderer mr;

	//public static Dictionary<String, List<Mesh>> weaponMeshMap;

	//public static List<Weapon> aliveWeapons;

	//TODO: these

	//public WeaponModifiers[] weaponModifiers;

	//public WeaponAttributes[] weaponAttributes;

	public float dmgMult;

	public float baseFiringSpd;

	public float baseReloadSpd;

	public float baseProjectileVel;

	public Vector3 physicalOffset;







	public Weapon(int baselvl, GameObject prefab) : base(baselvl, prefab)
	{
		gameObject.GetComponent<WeaponController>().setManager(this);
    	
    	InitializePhysics();

    	typeFlag = 0x02;
		//gameObject = new GameObject();
		/*if(weaponMeshMap == null)
		{
			weaponMeshMap = new Dictionary<String, List<Mesh>>();
		}*/

	}

	/*public static Weapon generateWeapon(int seed)
	{
		return new Weapon(1);
	}*/
}
