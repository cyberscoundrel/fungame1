using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

	//public GameObject gameObject;

	//TODO: all game time chaning fields to protected/private

	protected Weapon[] weapons;

	//TODO: all game time chaning fields to protected/private

	protected int weaponCount = 0;

	//TODO: all game time chaning fields to protected/private

	//protected List<Collectible> collectibles;





	public int maxItemCount;

	public int maxWeaponCount;

	//public static List<Player> playerPool;

	//public static Player player1;

	//public float baseSpd;

	//public float baseDmg;

	//public float baseTick;

	//protected bool tickActive = true;

	//public float baseRegen;

	//protected int level;

	//public int baseHealth;

	//protected int health;

	//public Thread tThread;

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
		//tickThread = new Thread(new ThreadStart(tickThread));


	}

	public void test()
	{
		Debug.Log("player test print");
	}


	public virtual void AddCollectible(Collectible c)
	{
		//collectibles.Add(c);
		//c.PickUp(this);
		base.AddCollectible(c);
		if(c.typeFlag == 0x02)
		{
			AddWeapon(c as Weapon);
		}
		//TODO:check item tFlag to see if it must be added from weapons
		/*if(c.typeFlag == 0x02)
		{
			gameObject.GetComponent<PlayerController>().selectedWeaponNum = AddWeapon(c as Weapon);
		}*/
	}

	/*public virtual void RemoveCollectible(Collectible c)
	{
		collectibles.Remove(c);
		c.Discard();
		//TODO:check item tFlag to see if it must be removed from weapons
		/*if(c.typeFlag == 0x02)
		{
			//int count = 0;
			for(int index0 = 0; index0 < weapons.Length; index0++)
			{
				if(weapons[index0] == c)
				{
					weapons[index0] = null;
					weaponCount--;
				}
				//coount++;
			}
		}*/
	//}

	/*public virtual void RemoveCollectible(int index)
	{
		collectibles.RemoveAt(index);
		//TODO:check item tFlag to see if it must be removed from weapons
	}

	public virtual void setLevel(int level)
	{
		this.level = level;
	}

	public virtual int getLevel()
	{
		return level;
	}

	public virtual void setHealth(int newHealth)
	{
		health = newHealth;
	}

	public virtual int getHealth()
	{
		return health;
	}*/

	public virtual int AddWeapon(Weapon w)
	{
		if(weaponCount < maxWeaponCount)
		{
			//AddCollectible(w);
			weaponCount++;
			for(int index0 = 0; index0 < weapons.Length; index0++)
			{
				if(weapons[index0] == null)
				{
					weapons[index0] = w;
					//gameObject.GetComponent<PlayerController>().selectWeapon(index0);
					return index0;
				}
			}
		}
		return -1;

	}

	public virtual int getWeaponCount()
	{
		return weaponCount;
	}

	public virtual Weapon getWeapon(int index)
	{
		return weapons[index];
	}

	//public 

	/*public void tickThread()
	{
		while(tickActive)
		{
			//tick thread logic
			Thread.Sleep(1000);//tick delay
		}



	}

	public void restartTick()
	{
		tickActive = true;
		t.Start();

	}

	public void ceaseTick()
	{
		tickActive = false;
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
