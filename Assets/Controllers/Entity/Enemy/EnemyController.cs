using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{

	//public Enemy enemyObject;

	public Player aggro;

    public bool isAggro;

	public NewRagdollScript enemyRagdollScript;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
    	if(entityObject.getHealth() < 0)
    	{
    		//Debug.Log("enemy dead");
    		enemyRagdollScript.enabled = false;
    	}
    	else
    	{
    		enemyRagdollScript.enabled = true;
    	}
        
    }

    /*public E createNewEnemy<E>(GameObject prefab) where E : Enemy
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
		//return e;
	//}
}
