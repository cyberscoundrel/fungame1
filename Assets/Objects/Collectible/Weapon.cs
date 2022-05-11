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

	public override String prefabDir { get {return "Prefabs/weapons";}}







	//public Weapon(int baselvl, GameObject prefab) : base(baselvl, prefab)
	public Weapon(int baselvl, uint prefab) : base(baselvl, prefab)
	{
		gameObject.GetComponent<WeaponController>().setManager(this);
    	
    	CollectibleManager.InitializePhysics(this);

    	typeFlag = 0x02;
		//gameObject = new GameObject();
		/*if(weaponMeshMap == null)
		{
			weaponMeshMap = new Dictionary<String, List<Mesh>>();
		}*/

	}

	public static new Collectible generateCollectible(int baselvl, uint prefab)
    {
    	return new Weapon(baselvl, prefab);
    }

	//void Start()
    //{
    	//Collectible.funcMap.Add(2,this.generateCollectible);
    //}

	/*public static Weapon generateWeapon(int seed)
	{
		return new Weapon(1);
	}*/
}
