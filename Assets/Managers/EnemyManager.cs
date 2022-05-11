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

    	enemyPool.Add(createNewEnemy<Enemy>(prefabs[0]));

        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKeyDown("m"))
    	{
    		enemyPool.Add(createNewEnemy<Enemy>(prefabs[0]));
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
    }

    public static void DamageEnemy(Enemy e, int damage)
    {

    	e.setHealth(e.getHealth() - damage);
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
		
	}

	

	[MessageHandler((ushort)ServerToClientId.enemySpawned)]

	public static void clientNewEnemy(Message message)
	{
		Enemy newEnemy;
		ushort eUTag = message.GetUShort();
		newEnemy = instance.createNewEnemy<Enemy>(instance.prefabs[0]);
		newEnemy.uTag = eUTag;
		newEnemy.gameObject.transform.position = message.GetVector3();
		newEnemy.gameObject.transform.rotation = message.GetQuaternion();

		//Player newPlayer = instance.createNewPlayer<RemotePlayer>(remoteguy)
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
