using RiptideNetworking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	public GameObject guy;
	public GameObject localguy;
	public GameObject remoteguy;

	public List<Player> playerPool;

	public LocalPlayer player1;

	public LocalPlayerController playerOneScript;
	public RagdollScript playerOneRDS;

	public static PlayerManager instance;

	public bool follow = true;

	void Awake()
	{
		playerPool = new List<Player>();
    	if(instance == null)
    	{
    		instance = this;
    	}
    	Debug.Log("start playercontroller");
	}
    // Start is called before the first frame update
    void Start()
    {
    	/*playerPool = new List<Player>();
    	if(instance == null)
    	{
    		instance = this;
    	}
    	Debug.Log("start playercontroller");*/
        //createNewPlayer<LocalPlayer>(localguy);
        //setPlayerOne(0);
        //createNewPlayer<Player>(guy);
        //createNewPlayer<Player>(guy);
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
	    		/*if(g.tag == "weapon")
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
	    		}*/
	    		c = g.GetComponent<CollectibleController>().getManager().DeactivatePhysicalInstance();
	    		if(c != null)
	    		{

	    			player1.AddCollectible(c);
	    			c.PickUp(player1);
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
    		//Debug.Log("chest transform" + playerOneRDS.head.transform.position);
    		//Debug.Log("weapon point transform" + playerOneScript.calculateWeaponPoint());
    		if(CliManager.instance != null)
    		{
	    		Debug.DrawLine(playerOneRDS.head.transform.position, playerOneScript.calculateWeaponPoint(), Color.gray);
	    		Message m = Message.Create(MessageSendMode.unreliable, (ushort)ClientToServerId.move);
	    		//m.AddUShort(player1.uTag);
	    		m.AddVector3(playerOneRDS.head.transform.position);
	    		m.AddQuaternion(playerOneRDS.head.transform.rotation);
	    		CliManager.client.Send(m);
    		}

    	}

    }

    public P createNewPlayer<P>(GameObject prefab) where P : Player
	{
		Debug.Log("create newplayer");
		P p = (P)Activator.CreateInstance(typeof(P), new object[] {2, -1, prefab});
		//P p = new P(2, -1, guy);

		playerPool.Add(p);
		/*if(player1 == null)
		{
			//player1 = p;
			setPlayerOne(0);
		}*/
		return p;
	}

	[MessageHandler((ushort)ClientToServerId.name)]

	public static void serverNewPlayer(ushort clientId, Message message)
	{
		Debug.Log("server name");
		Player newPlayer = instance.createNewPlayer<RemotePlayer>(instance.remoteguy);

		newPlayer.uTag = clientId;
		Message m = Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.playerSpawned);
		m.AddUShort(clientId);
		NetManager.instance.server.SendToAll(m);
		//return clientId;

	}

	[MessageHandler((ushort)ServerToClientId.playerSpawned)]

	public static void clientNewPlayer(Message message)
	{
		Player newPlayer;
		ushort clientId = message.GetUShort();
		if(clientId == CliManager.client.Id)
		{
			newPlayer = instance.createNewPlayer<LocalPlayer>(instance.localguy);
			newPlayer.uTag = clientId;
			instance.setPlayerOne(newPlayer);
			Player newPlayerHost = instance.createNewPlayer<RemotePlayer>(instance.remoteguy);
			newPlayerHost.uTag = 0;

		}
		else
		{
			newPlayer = instance.createNewPlayer<RemotePlayer>(instance.remoteguy);

			newPlayer.uTag = clientId;
			//serverNewPlayer(clientId, message);
		}
		//Player newPlayer = instance.createNewPlayer<RemotePlayer>(remoteguy)
	}

	[MessageHandler((ushort)ServerToClientId.playerMovement)]

	public static void clientPlayerMove(Message message)
	{
		Player movePlayer = instance.getPlayerByUTag(message.GetUShort());
		if(movePlayer != null)
		{
			movePlayer.gameObject.GetComponent<PlayerController>().rds.head.transform.position = message.GetVector3();
			movePlayer.gameObject.GetComponent<PlayerController>().rds.head.transform.rotation = message.GetQuaternion();
		}
	}

	[MessageHandler((ushort)ClientToServerId.move)]

	public static void serverPlayerMove(ushort clientId, Message message)
	{
		Player movePlayer = instance.getPlayerByUTag(clientId);
		if(movePlayer != null)
		{
			Vector3 newPos = message.GetVector3();
			Quaternion newRot = message.GetQuaternion();
			movePlayer.gameObject.GetComponent<PlayerController>().rds.head.transform.position = newPos;
			movePlayer.gameObject.GetComponent<PlayerController>().rds.head.transform.rotation = newRot;
			Message m = Message.Create(MessageSendMode.unreliable, (ushort)ServerToClientId.playerMovement);
			m.AddUShort(clientId);
			m.AddVector3(newPos);
			m.AddQuaternion(newRot);
		}
	}

	

	public Player getPlayerByUTag(int uTag)
	{
		foreach(Player p in playerPool)
		{
			if(p.uTag == uTag)
			{
				return p;
			}
		}
		return null;
	}

	public Player getPlayerByUTag(ushort uTag)
	{
		foreach(Player p in playerPool)
		{
			if(p.uTag == uTag)
			{
				return p;
			}
		}
		return null;
	}

	public void setPlayerOne(int index)
	{
		Debug.Log("set playerone");
		if(index < playerPool.Count)
		{
			Debug.Log("setting playerone");
			try
			{
				player1 = playerPool[index] as LocalPlayer;
			}
			catch(InvalidCastException e)
			{
				Debug.Log("index " + index + " is not a LocalPlayer");
				return;
			}
			playerOneScript = player1.gameObject.GetComponent<PlayerController>() as LocalPlayerController;
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

	public void setPlayerOne(Player p)
	{
		Debug.Log("set playerone");
		Debug.Log("setting playerone");
		try
		{
			player1 = p as LocalPlayer;
		}
		catch(InvalidCastException e)
		{
			Debug.Log("index " + p + " is not a LocalPlayer");
			return;
		}
		playerOneScript = player1.gameObject.GetComponent<PlayerController>() as LocalPlayerController;
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
