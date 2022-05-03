using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

	public List<Enemy> enemyPool;
    public List<GameObject> prefabs;
	public static EnemyManager instance;

    public GameObject currentWorld;
    public Vector3 offset;




    // LOCATION TO SPAWN ENEMY
   // public Vector3 spawnLocation = new Vector3(current);

	public int spawnNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentWorld = GameObject.Find("gravityCenter");
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

        if (Input.GetKeyDown("g"))
        {
            enemyPool.Add(createNewEnemy<Enemy>(prefabs[0]));
        }
        if (Input.GetKeyDown("space"))
        {
            currentWorld = GameObject.Find("gravityCenter");
            enemyPool.Add(createNewEnemy<Enemy>(prefabs[0]));
            Debug.Log("*******************************************");
            Debug.Log("*******************************************");
            Debug.Log(currentWorld.transform.position);
            Debug.Log("*******************************************");
            Debug.Log("*******************************************");
            Debug.Log("*******************************************");
        }
    }




    public E createNewEnemy<E>(GameObject prefab) where E : Enemy
	{
        //TEST CODE
        E newEnemy = (E)Activator.CreateInstance(typeof(E), new object[] {1, prefab, (currentWorld.transform.position + offset)});

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
