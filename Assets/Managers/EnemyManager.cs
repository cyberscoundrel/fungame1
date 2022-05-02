using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

	public List<Enemy> enemyPool;
    public List<GameObject> prefabs;
	public static EnemyManager instance;

	


    // LOCATION TO SPAWN ENEMY
    //public Vector3 spawnLocation = (0,0,0);

	public int spawnNum = 0;
    // Start is called before the first frame update
    void Start()
    {
    	enemyPool = new List<Enemy>();
    	if(instance == null)
    	{
    		instance = this;
    	}

    	
        //enemyPool.Add(createNewEnemy<Enemy>(prefabs[0]));
        //enemyPool.Add(createNewEnemy<Enemy>(prefabs[0]));
        for(int i = 0; i < spawnNum; i++) enemyPool.Add(createNewEnemy<Enemy>(prefabs[0]));


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public E createNewEnemy<E>(GameObject prefab) where E : Enemy
	{
        //TEST CODE
        E newEnemy = (E)Activator.CreateInstance(typeof(E), new object[] {1, prefab});

        enemyPool.Add(newEnemy);
        return newEnemy;

        //END TEST CODE





        /*
        ORIGINAL
		Debug.Log("create newenemy");
		E e = (E)Activator.CreateInstance(typeof(E), new object[] {1, prefab});
		enemyPool.Add(e);		
		return e;
        */

	}


}
