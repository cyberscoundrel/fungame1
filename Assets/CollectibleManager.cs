using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{

	public Queue<Collectible> collectibleQueue;
	public Dictionary<int, Collectible> collectibleDict;

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


    //DEPRECATED
    [ObsoleteAttribute("this method for generating objects cannot be used for syncronized sessions, do not use in the future", false)]
    public static void GenerateObject()
    {

    	//TODO: code for item and weapon scaling based on game parameters;

    	if(toggle)
    	{
    		//collectibleQueue.Enqueue(new )
    		Item i = new Item(1, instance.itemPrefabs[0]);
    		i.uTag = instance.collectibleQueue.Count;
    		instance.collectibleQueue.Enqueue(i);
    		instance.collectibleDict.Add(i.uTag, i);



    		//instance.collectibleQueue.Enqueue(new Item(1, instance.itemPrefabs[0]));

    	}
    	else
    	{
    		Weapon w = new Weapon(1, instance.weaponPrefabs[0]);
    		w.uTag = instance.collectibleQueue.Count;
    		instance.collectibleQueue.Enqueue(w);
    		instance.collectibleDict.Add(w.uTag, w);
    		//instance.collectibleQueue.Enqueue(new Weapon(1, instance.weaponPrefabs[0]));
    	}
    	toggle = !toggle;




    	if(instance.collectibleQueue.Count >= instance.queueSize)
    	{
    		//TODO: eliminate item from gamespace if unowned

    		Collectible c = instance.collectibleQueue.Dequeue();
    		instance.collectibleDict.Remove(c.uTag);
    	}


    }

    //TODO: impliment generate object methods for syncronized games
    public static void GenerateObject(int baselvl, int tFlag, GameObject prefab)
    {

    }

    public static void GenerateObject(int baselvl, int tFlag, int seed, GameObject prefab)
    {

    }

    //TODO: create stat objects
    //public static void GenerateObject(int baselvl, int tFlag, weaponStats wepstats, GameObject prefab)

    public static void EnqueuObject(Collectible c)
    {
    	instance.collectibleQueue.Enqueue(c);
    }

    public static void PurgeQueue()
    {
    	//TODO: eliminate items from gamespace
    	instance.collectibleDict.Clear();
    	instance.collectibleQueue.Clear();

    }


}
