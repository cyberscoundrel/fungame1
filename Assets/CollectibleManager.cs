using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{

	public Queue<Collectible> collectibleQueue;

	public int queueSize = 64;

	public static CollectibleManager instance;

	public List<GameObject> itemPrefabs;

	public List<GameObject> weaponPrefabs;



	public static bool toggle = false;


    // Start is called before the first frame update

    void Awake()
    {
    	if(instance == null)
    	{
    		instance = this;
    	}
    }
    void Start()
    {



    	if(collectibleQueue == null)
    	{
    		collectibleQueue = new Queue<Collectible>();
    	}

    	GenerateObject();
    	GenerateObject();


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void GenerateObject()
    {

    	//TODO: code for item and weapon scaling based on game parameters;

    	if(toggle)
    	{
    		//collectibleQueue.Enqueue(new )

    		instance.collectibleQueue.Enqueue(new Item(1, instance.itemPrefabs[0]));
    	}
    	else
    	{
    		instance.collectibleQueue.Enqueue(new Weapon(1, instance.weaponPrefabs[0]));
    	}
    	toggle = !toggle;




    	if(instance.collectibleQueue.Count >= instance.queueSize)
    	{
    		instance.collectibleQueue.Dequeue();
    	}


    }

    public static void EnqueuObject(Collectible c)
    {
    	instance.collectibleQueue.Enqueue(c);
    }

    public static void PurgeQueue()
    {
    	instance.collectibleQueue.Clear();

    }


}
