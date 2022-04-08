using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	public GameObject guy;

	public List<Player> playerPool;

	public Player player1;

	public PlayerController playerOneScript;
	public RagdollScript playerOneRDS;

	public static PlayerManager instance;

	public bool follow = true;
    // Start is called before the first frame update
    void Start()
    {
    	playerPool = new List<Player>();
    	if(instance == null)
    	{
    		instance = this;
    	}
    	Debug.Log("start playercontroller");
        createNewPlayer();
        createNewPlayer();
        createNewPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e"))
    	{
    		Debug.Log("interact e");
    		GameObject g = playerOneScript.interactForward();
    		Collectible c = null;
    		if(g != null)
    		{
	    		if(g.tag == "weapon")
	    		{
	    			Debug.Log("g is null" + (g == null));
	    			Debug.Log("WeaponController is null" + (g.GetComponent<WeaponController>() == null));
	    			Debug.Log(g.name);
	    			Debug.Log("weaponObject is null" + (g.GetComponent<WeaponController>().weaponObject));
	    			c = g.GetComponent<WeaponController>().weaponObject.DeactivatePhysicalInstance();
	    			player1.weapons[player1.weaponCount] = c as Weapon;
	    			player1.weaponCount += 1;
	    			if(player1.weaponCount == 1)
	    			{
	    				Debug.Log("selectWeapon");
	    				playerOneScript.selectWeapon(0);
	    			}
	    		}
	    		if(g.tag == "item")
	    		{
	    			Debug.Log("item item");
	    			c = g.GetComponent<ItemController>().itemObject.DeactivatePhysicalInstance();
	    		}
	    		if(c != null)
	    		{

	    			player1.AddCollectible(c);
	    		}

	    	}
	    	else
	    	{
	    		Debug.Log("filtered");
	    	}

    	}
    	if(Input.GetMouseButtonDown(0))
    	{
    		//shoot go here

    	}
    }

    void FixedUpdate()
    {
    	if(player1 != null && follow)
    	{
    		//if(player1.transform.Find())
    		playerOneRDS.HeadMouseFollow();
    		Debug.DrawLine(playerOneRDS.head.transform.position, playerOneScript.calculateWeaponPoint(), Color.gray);
    	}

    }

    public Player createNewPlayer()
	{
		Debug.Log("create newplayer");
		Player p = new Player(2, -1, guy);

		playerPool.Add(p);
		if(player1 == null)
		{
			//player1 = p;
			setPlayerOne(0);
		}
		return p;
	}

	public void setPlayerOne(int index)
	{
		Debug.Log("set playerone");
		if(index < playerPool.Count)
		{
			Debug.Log("setting playerone");
			player1 = playerPool[index];
			playerOneScript = player1.gameObject.GetComponent<PlayerController>();
			playerOneRDS = player1.gameObject.GetComponent<RagdollScript>();
			if(playerOneRDS == null)
			{
				Debug.Log("no rds");
			}
			if(playerOneRDS.getHead() == null)
			{
				Debug.Log("no get head");
			}
			player1.test();
			playerOneRDS.getHead();
			if(ControlledObject.instance == null)
			{
				Debug.Log("instance null");
			}
			if(ControlledObject.instance.controlledObject = null)
			{
				Debug.Log("no object");
			}
			//ControlledObject.instance.controlledObject = playerOneRDS.getHead();
			ControlledObject.setControlledObject(playerOneRDS.getHead());
			playerOneRDS.c = ControlledObject.instance.controlledCamera;
		}
	}
}
