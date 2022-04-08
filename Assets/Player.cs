using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

	public GameObject gameObject;

	public Weapon[] weapons;

	public int weaponCount = 0;

	public List<Collectible> collectibles;

	public int maxItemCount;

	public int maxWeaponCount;

	//public static List<Player> playerPool;

	//public static Player player1;

	public float baseSpd;

	public float baseDmg;

	public float baseTick;

	public float baseRegen;

	public int level;

	public int baseHealth;

	public int health;

	//TODO: these

	//public List<PlayerModifiers> playerModifiers;

	//public List<PlayerAttributes> playerAttributes;

	public Player(int maxWeaponCount, int maxItemCount, GameObject prefab)
	{
		weapons = new Weapon[maxWeaponCount];
		collectibles = new List<Collectible>();
		this.maxWeaponCount = maxWeaponCount;
		this.maxItemCount = maxItemCount;
		gameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;
		Component[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
		foreach(Rigidbody r in rigidbodies)
		{
			GalaxyManager.AddRb(r);
		}
		gameObject.GetComponent<PlayerController>().playerObject = this;
		baseDmg = 10f;
		baseSpd = 10f;
		baseTick = 1f;
		baseRegen = 1f;
		baseHealth = 100;


	}

	public void test()
	{
		Debug.Log("player test print");
	}


	public void AddCollectible(Collectible c)
	{
		collectibles.Add(c);
	}

	public void RemoveCollectible(Collectible c)
	{
		collectibles.Remove(c);
	}

	public void RemoveCollectible(int index)
	{
		collectibles.RemoveAt(index);
	}

	/*public static Player createNewPlayer()
	{
		Player p = new Player(2, -1);

		playerPool.Add(p);
		if(player1 == null)
		{
			player1 = p;
		}
		return p;
	}

	public static void setPlayerOne(int index)
	{
		if(index < playerPool.Count)
		{
			player1 = playerPool[index];
		}
	}*/
}
