using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

	public List<Enemy> enemyPool;

	public static EnemyManager instance;

	//public Dictionary<GameObject, int> prefabMap;

	public List<GameObject> prefabs;

	public int spawnNum = 0;
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
        
    }

    public E createNewEnemy<E>(GameObject prefab) where E : Enemy
	{
		Debug.Log("create newenemy");
		E e = (E)Activator.CreateInstance(typeof(E), new object[] {1, prefab});
		//P p = new P(2, -1, guy);

		enemyPool.Add(e);
		/*if(player1 == null)
		{
			//player1 = p;
			setPlayerOne(0);
		}*/
		return e;
	}


}
