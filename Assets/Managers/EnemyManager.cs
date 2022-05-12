using RiptideNetworking;
using RiptideNetworking.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

	public List<Enemy> enemyPool;
	public Dictionary<ushort, Enemy> enemyDict;

	public static EnemyManager instance;

	//public Dictionary<GameObject, int> prefabMap;

	public List<GameObject> prefabs;

	public int spawnNum = 0;

	ushort nextUTag = 0;
    // Start is called before the first frame update
    void Start()
    {
    	enemyPool = new List<Enemy>();
    	if(instance == null)
    	{
    		instance = this;
    	}

    	//enemyPool.Add(createNewEnemy<Enemy>(prefabs[0]));

        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKeyDown("m"))
    	{
    		GenerateEnemy(1, 69);
    		//enemyPool.Add(createNewEnemy<Enemy>(prefabs[0]));
    	}
        
    }

    /*public static void getEnemyByUTag(ushort utag)
    {

    }*/

    public static void DamageEnemy(ushort utag, int damage)
    {

    	Enemy e = instance.getEnemyByUTag(utag);
    	DamageEnemy(e, damage);

    }

    public static void DamageEnemy(ushort pUTag, ushort eUTag, int damage)
    {
    	DamageEnemy(eUTag, damage);
    	if(NetManager.instance != null)
    	{

    		Enemy e = instance.getEnemyByUTag(eUTag);

    		e.gameObject.GetComponent<EnemyController>().aggro = PlayerManager.instance.player1;
			e.gameObject.GetComponent<EnemyController>().isAggro = true;
    		Message m = Message.Create(MessageSendMode.unreliable, (ushort)ServerToClientId.enemyHit);
    		m.AddUShort(PlayerManager.instance.player1.uTag);
    		m.AddUShort(eUTag);
    		m.AddInt(damage);
    		NetManager.instance.server.SendToAll(m);
    	}
    	else if(CliManager.client != null)
    	{
    		Message m = Message.Create(MessageSendMode.unreliable, (ushort)ClientToServerId.hitEnemy);
    		m.AddUShort(PlayerManager.instance.player1.uTag);
    		m.AddUShort(eUTag);
    		m.AddInt(damage);
    		CliManager.client.Send(m);

    	}
    	else if(CliManager.client == null && NetManager.instance == null)
    	{
    		Enemy e = instance.getEnemyByUTag(eUTag);

    		e.gameObject.GetComponent<EnemyController>().aggro = PlayerManager.instance.player1;
			e.gameObject.GetComponent<EnemyController>().isAggro = true;
    	}
    }

    [MessageHandler((ushort)ClientToServerId.hitEnemy)]

    public static void serverDamageEnemy(ushort clientId, Message message)
    {
    	Message m = Message.Create(MessageSendMode.unreliable, (ushort)ServerToClientId.enemyHit);
    	ushort pUTag = message.GetUShort();
    	ushort eUTag = message.GetUShort();
    	int damage = message.GetInt();
    	DamageEnemy(eUTag, damage);
    	m.AddUShort(pUTag);
		m.AddUShort(eUTag);
		m.AddInt(damage);

		NetManager.instance.server.SendToAll(m);

		//TODO: new agro system

		Enemy e = instance.getEnemyByUTag(eUTag);
		if(e != null)
		{
			Player p = PlayerManager.instance.getPlayerByUTag(message.GetUShort());
			if(p != null)
			{
				e.gameObject.GetComponent<EnemyController>().aggro = p;
				e.gameObject.GetComponent<EnemyController>().isAggro = true;
			}
		}

		m = Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.enemyAggro);

		m.AddUShort(eUTag);
		m.AddUShort(pUTag);


    } 

    [MessageHandler((ushort)ServerToClientId.enemyHit)]

    public static void clientDamageEnemy(Message message)
    {
    	ushort pUTag = message.GetUShort();
    	ushort eUTag = message.GetUShort();
    	int damage = message.GetInt();
    	DamageEnemy(eUTag, damage);
    }

    public static void DamageEnemy(Enemy e, int damage)
    {
		Debug.Log("DAMAGE:" + damage);
		int health = e.getHealth() - damage;
		e.setHealth(health);
		e.gameObject.GetComponent<InstantiateHealthbar>().healthbarInstance.GetComponent<HealthBarController>().UpdateHealth(health);

		//e.healh -= damage;
	}

    public E createNewEnemy<E>(GameObject prefab) where E : Enemy
	{
		Debug.Log("create newenemy");
		E e = (E)Activator.CreateInstance(typeof(E), new object[] {1, prefab});
		e.uTag = nextUTag;
		nextUTag++;
		//P p = new P(2, -1, guy);

		enemyPool.Add(e);
		/*if(player1 == null)
		{
			//player1 = p;
			setPlayerOne(0);
		}*/
		return e;
	}

	public static void GenerateEnemy(int baselvl, int seed)
	{
		Enemy e = instance.createNewEnemy<Enemy>(instance.prefabs[0]);
		e.gameObject.GetComponent<NewRagdollScript>().hips.isKinematic = true;
		e.gameObject.GetComponent<NewRagdollScript>().hips.transform.position = GalaxyManager.gravityCenter.gameObject.transform.position + (UnityEngine.Random.onUnitSphere * (GalaxyManager.gravityCenter.glObject.maxRadius * 1.2f));
		e.gameObject.GetComponent<NewRagdollScript>().hips.isKinematic = false;
		if(NetManager.instance != null)
		{
			Message m = Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.enemySpawned);
			m.AddUShort(e.uTag);
			m.AddVector3(e.gameObject.GetComponent<NewRagdollScript>().hips.transform.position);
			//m.AddQuaternion(e.gameObject.transform.rotation);
		}
		//enemyPool.Add(e);
	}

	

	[MessageHandler((ushort)ServerToClientId.enemySpawned)]

	public static void clientNewEnemy(Message message)
	{
		Enemy newEnemy;
		ushort eUTag = message.GetUShort();
		newEnemy = instance.createNewEnemy<Enemy>(instance.prefabs[0]);
		newEnemy.uTag = eUTag;
		newEnemy.gameObject.GetComponent<NewRagdollScript>().hips.isKinematic = true;
		newEnemy.gameObject.GetComponent<NewRagdollScript>().hips.transform.position = message.GetVector3();
		newEnemy.gameObject.GetComponent<NewRagdollScript>().hips.isKinematic = true;
		//newEnemy.gameObject.transform.rotation = message.GetQuaternion();

		//Player newPlayer = instance.createNewPlayer<RemotePlayer>(remoteguy)
	}

	[MessageHandler((ushort)ServerToClientId.enemyAggro)]

	public static void clientNewAgro(Message message)
	{
		Enemy e = instance.getEnemyByUTag(message.GetUShort());
		if(e != null)
		{
			Player p = PlayerManager.instance.getPlayerByUTag(message.GetUShort());
			if(p != null)
			{
				e.gameObject.GetComponent<EnemyController>().aggro = p;
				e.gameObject.GetComponent<EnemyController>().isAggro = true;
			}
		}
	}

	public Enemy getEnemyByUTag(int uTag)
	{
		foreach(Enemy e in enemyPool)
		{
			if(e.uTag == uTag)
			{
				return e;
			}
		}
		return null;
	}

	public Enemy getEnemyByUTag(ushort uTag)
	{
		foreach(Enemy e in enemyPool)
		{
			if(e.uTag == uTag)
			{
				return e;
			}
		}
		return null;
	}


}
