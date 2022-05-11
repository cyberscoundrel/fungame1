using System;
using System.Security.Cryptography;
using RiptideNetworking;
using RiptideNetworking.Utils;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{

    public enum CollectibleId : ushort
    {
        weapon = 1,
        item = 2,
    }

	//public Queue<Collectible> collectibleQueue;
    public List<Collectible> collectibleQueue;
	public Dictionary<int, Collectible> collectibleDict;

	public int queueSize = 64;

	public static CollectibleManager instance;

	public List<GameObject> itemPrefabs;

	public List<GameObject> weaponPrefabs;

    public int nextUTag = 0;





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
    		//collectibleQueue = new Queue<Collectible>();
            collectibleQueue = new List<Collectible>();
            collectibleDict = new Dictionary<int, Collectible>();
    	}

    	TestGenerateObject();
    	TestGenerateObject();


        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("n"))
        {
            //GenerateObject(GalaxyManager.gameLvl, GalaxyManager.instance.seed);
            TestGenerateObject();
        }
        
    }

    void FixedUpdate()
    {
        /*if(NetManager.instance != null)
        {
            Message m = Message.Create(MessageSendMode.unreliable, (ushort)ServerToClientId.collectibleMovement);
            m.AddUShort((ushort)collectibleQueue.Count);
            foreach(Collectible c in collectibleQueue)
            {
                m.AddUShort(unchecked((ushort)c.uTag));
                m.AddVector3(c.gameObject.transform.position);
                m.AddQuaternion(c.gameObject.transform.rotation);
            }
        }*/
        //collectibleMovementRefresh
    }


    //DEPRECATED
    [ObsoleteAttribute("this method for generating objects cannot be used for syncronized sessions, do not use in the future", false)]
    public static void TestGenerateObject()
    {

    	//TODO: code for item and weapon scaling based on game parameters;
        //Collectible c;

    	if(toggle)
    	{
    		//collectibleQueue.Enqueue(new )
    		//Item i = new Item(1, instance.itemPrefabs[0]);
    		//i.uTag = unchecked((ushort)instance.collectibleQueue.Count);

    		//instance.collectibleQueue.Enqueue(i);
            //instance.collectibleQueue.Insert(0, i);
    		//instance.collectibleDict.Add(i.uTag, i);
            //c = GenerateObject<Item>(GalaxyManager.gameLvl, 69);



    		//c = EnqueueObject(new Item(1, instance.itemPrefabs[0]));
            Debug.Log("collectibleType item");
            ushort newUTag = unchecked((ushort)instance.nextUTag);
            instance.nextUTag++;
            GenerateObject(1, GalaxyManager.instance.seed, newUTag, (ushort)1);

    	}
    	else
    	{
            //c = GenerateObject<Weapon>(GalaxyManager.gameLvl, 69);
    		//Weapon w = new Weapon(1, instance.weaponPrefabs[0]);
    		//w.uTag = unchecked((ushort)instance.collectibleQueue.Count);
            //instance.collectibleQueue.Insert(0, w);
    		//instance.collectibleQueue.Enqueue(w);
    		//instance.collectibleDict.Add(w.uTag, w);
    		//c = EnqueueObject(new Weapon(1, instance.weaponPrefabs[0]));
            Debug.Log("collectibleType weapon");
            ushort newUTag = unchecked((ushort)instance.nextUTag);
            instance.nextUTag++;
            GenerateObject(1, (GalaxyManager.instance.seed + newUTag), newUTag, (ushort)2);
    	}
        //c.uTag = unchecked((ushort)instance.collectibleQueue.Count);

        //instance.EnqueueObject(c);
        //instance.collectibleQueue.Insert(0, c);
        //instance.collectibleDict.Add(c.uTag, c);
    	toggle = !toggle;




    	/*if(instance.collectibleQueue.Count >= instance.queueSize)
    	{
    		//TODO: eliminate item from gamespace if unowned

    		Collectible c = instance.collectibleQueue.Dequeue();
    		instance.collectibleDict.Remove(c.uTag);
    	}*/


    }

    //TODO: impliment generate object methods for syncronized games
    /*public static void GenerateObject(int baselvl, int tFlag, GameObject prefab)
    {

    }

    public static void GenerateObject(int baselvl, int tFlag, int seed, GameObject prefab)
    {


    }*/
    //public static Collectible GenerateObject<C>(int baselvl, int seed, ushort newUTag, ushort collectibleType) where C : Collectible
    public static Collectible GenerateObject(int baselvl, int seed, ushort newUTag, ushort collectibleType)
    {
        SHA256 hasher = SHA256.Create();
        uint fromSeed = BitConverter.ToUInt32(hasher.ComputeHash(BitConverter.GetBytes(seed)), 0);
        //float f = unchecked((int)fromSeed) / 4294967296f;
        Collectible c;
        //C c;
        /*if(collectibleType == (ushort)CollectibleId.item)
        {
            c = new Item(baselvl, instance.itemPrefabs[(fromSeed) % instance.itemPrefabs.Count]);

        }
        else
        {
            c = new Weapon(baselvl, instance.weaponPrefabs[0]);
        }*/
        c = CollectibleMap.funcMap[collectibleType](baselvl, fromSeed);
        //c.seed = unchecked((uint)(seed + newUTag));
        c.seed = unchecked((uint)seed);
        //c = new C(baselvl, instance.itemPrefabs[(fromSeed) % instance.itemPrefabs.Count]);
        //c = (C)Activator.CreateInstance(typeof(C), new object[] {baselvl, instance.itemPrefabs[(fromSeed) % instance.itemPrefabs.Count]});
        c.typeFlag = unchecked((int)collectibleType);
        c.uTag = newUTag;
        //c.gameObject.transform.position = 
        if(NetManager.instance != null)
        {
            c.gameObject.transform.position = GalaxyManager.gravityCenter.gameObject.transform.position + (UnityEngine.Random.onUnitSphere * GalaxyManager.gravityCenter.glObject.maxRadius);
            Message m = Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.collectibleSpawned);
            m.AddUShort(unchecked((ushort)c.typeFlag));
            m.AddInt(c.baselvl);
            m.AddInt(unchecked((int)c.seed));
            m.AddVector3(c.gameObject.transform.position);
            m.AddQuaternion(c.gameObject.transform.rotation);
            m.AddUShort(c.uTag);
            NetManager.instance.server.SendToAll(m);
        }
        EnqueueObject(c);
        return c;

        //return null;


    }

    //public static Collectible GenerateObject<C>(int baselvl, int seed) where C : Collectible
    public static Collectible GenerateObject(int baselvl, int seed)
    {
        
        ushort newUTag = unchecked((ushort)instance.nextUTag);
        instance.nextUTag++;
        SHA256 hasher = SHA256.Create();
        uint fromSeed = BitConverter.ToUInt32(hasher.ComputeHash(BitConverter.GetBytes(seed + newUTag)), 0);
        //float f = unchecked((int)fromSeed) / 4294967296f;
        //Collectible c = GenerateObject(baselvl, seed, newUTag, unchecked((ushort)UnityEngine.Random.Range(1,2)));
        //C c =  GenerateObject<C>(baselvl, seed, newUTag, unchecked((ushort)UnityEngine.Random.Range(1,2))) as C;
        //int r = UnityEngine.Random.Range(0, CollectibleMap.funcMap.Count);
        uint r = fromSeed % unchecked((uint)CollectibleMap.keys.Length);
        Debug.Log("CollectibleMap keys " + r);
        Collectible c = GenerateObject(baselvl, (seed + newUTag), newUTag, unchecked((ushort)CollectibleMap.keys[r]));

        /*if(NetManager.instance != null)
        {
            Message m = Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.collectibleSpawned);
            m.AddUShort(unchecked((ushort)c.typeFlag));
            m.AddInt(baselvl);
            m.AddInt(seed + newUTag);
            m.AddVector3(c.gameObject.transform.position);
            m.AddQuaternion(c.gameObject.transform.rotation);
            m.AddUShort(c.uTag);
            NetManager.instance.server.SendToAll(m);
        }*/
        return c;

        //return null;

    }

    [MessageHandler((ushort)ServerToClientId.collectibleSpawned)]

    public static void clientAddObject(Message message)
    {
        Debug.Log("clientAddObject");
        ushort collectibleType = message.GetUShort();
        int newBaseLvl = message.GetInt();
        int newSeed = message.GetInt();

        //ushort prefabIndex = message.GetUShort();
        Vector3 newPos = message.GetVector3();
        Quaternion newRot = message.GetQuaternion();
        ushort collectibleUTag = message.GetUShort();
        Collectible c = GenerateObject(newBaseLvl, newSeed, collectibleUTag, collectibleType);
        c.gameObject.transform.position = newPos;
        c.gameObject.transform.rotation = newRot;
       //Message m = Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.collectibleSpawned)





    }

    public static Collectible InitializePhysics(Collectible c)
    {
        if(!c.activeInWorld)
        {
            Debug.Log("InitializePhysics");
            Rigidbody[] rigidbodies = c.gameObject.GetComponentsInChildren<Rigidbody>();
            Rigidbody p = c.gameObject.GetComponent<Rigidbody>();
            foreach(Rigidbody r in rigidbodies)
            {
                GalaxyManager.AddRb(r);
            }
            foreach(Transform t in c.gameObject.transform)
            {
                Debug.Log("transform in gameObject " + t.gameObject.name + "SetActive true");
                t.gameObject.SetActive(true);
                //t.gameObject.enable = true;
                //t.gameObject.activeSelf = true;
            }
            foreach(Transform t in c.gameObject.transform)
            {
                Debug.Log("transform in gameObject " + t.gameObject.name + " is " + t.gameObject.activeSelf);
                //t.gameObject.enable = true;
                //t.gameObject.activeSelf = true;
            }
            if(p != null)
            {
                GalaxyManager.AddRb(p);
            }
            c.activeInWorld = true;
        }
        return c;
    }

    public static Collectible DeactivatePhysicalInstance(Collectible c)
    {
        if(c.activeInWorld)
        {
            Debug.Log("DeactivatePhysicalInstance");
            Rigidbody[] rigidbodies = c.gameObject.GetComponentsInChildren<Rigidbody>();
            Rigidbody p = c.gameObject.GetComponent<Rigidbody>();
            foreach(Rigidbody r in rigidbodies)
            {
                GalaxyManager.RemoveRb(r);
            }
            foreach(Transform t in c.gameObject.transform)
            {
                t.gameObject.SetActive(false);
            }
            if(p != null)
            {
                GalaxyManager.RemoveRb(p);

            }
            c.activeInWorld = false;
        }
        return c;
    }

    /*[MessageHandler((ushort)ServerToClientId.collectiblePickedUp)]

    public static void clientItemPickedUp(Message message)
    {
        ushort id = message.GetUShort();
        ushort collectibleId = message.GetUShort();
        Collectible c = CollectibleManager.instance.getCollectibleByUTag(collectibleUTag);
        if(c != null)
        {
            Player p = PlayerManager.instance.getPlayerByUTag(id);
            if(p != null)
            {
                p.AddCollectible(c);
                c.PickUp();
            }
        }

    }*/

    //[MessageHandler((ushort)ClientToServerId.pickup)]

    /*public static void collectiblePickedUp(ushort clientId, Message message)
    {





    }*/


    [MessageHandler((ushort)ServerToClientId.collectibleMovement)]

    public static void clientCollectibleMovement(Message message)
    {
        //loop through the number of updated item positions and rotations
        ushort count = message.GetUShort();
        Collectible c;
        for(int index0 = 0; index0 < count; ++index0)
        {
            c = instance.getCollectibleByUTag(message.GetUShort());
            if(c != null)
            {
                c.gameObject.transform.position = message.GetVector3();
                c.gameObject.transform.rotation = message.GetQuaternion();
            }
        }

    }

    [MessageHandler((ushort)ServerToClientId.playerPickedUp)]

    public static void clientCollectiblePickedUp(Message message)
    {
        ushort utag = message.GetUShort();
        Collectible c = instance.getCollectibleByUTag(utag);
        if(c != null)
        {
            ushort playerUtag = message.GetUShort();
            Player p = PlayerManager.instance.getPlayerByUTag(playerUtag);
            if(p != null)
            {
                c.PickUp(p);
                p.AddCollectible(c);
            }
        }

    }

    [MessageHandler((ushort)ClientToServerId.pickup)]

    public static void serverCollectiblePickedUp(ushort clientId, Message message)
    {
        ushort utag = message.GetUShort();
        Collectible c = instance.getCollectibleByUTag(utag);
        if(c != null)
        {
            Player p = PlayerManager.instance.getPlayerByUTag(clientId);
            if(p != null)
            {
                c.PickUp(p);
                p.AddCollectible(c);
            }
        }
        Message m = Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.playerPickedUp);
        m.AddUShort(utag);
        m.AddUShort(clientId);
        NetManager.instance.server.SendToAll(m);
    }

    //TODO: create stat objects
    //public static void GenerateObject(int baselvl, int tFlag, weaponStats wepstats, GameObject prefab)

    public static Collectible EnqueueObject(Collectible c)
    {
    	//instance.collectibleQueue.Enqueue(c);
        instance.collectibleQueue.Insert(0, c);
        instance.collectibleDict.Add(c.uTag, c);


        if(instance.collectibleQueue.Count >= instance.queueSize)
        {
            //TODO: eliminate item from gamespace if unowned

            Collectible c1 = instance.collectibleQueue[instance.collectibleQueue.Count - 1];
            instance.collectibleQueue.RemoveAt(instance.collectibleQueue.Count - 1);
            instance.collectibleDict.Remove(c1.uTag);
        }
        return c;
    }

    public static void PurgeQueue()
    {
    	//TODO: eliminate items from gamespace
    	instance.collectibleDict.Clear();
    	instance.collectibleQueue.Clear();

    }

    public Collectible getCollectibleByUTag(ushort uTag)
    {
        foreach(Collectible c in collectibleQueue)
        {
            if(c.uTag == uTag)
            {
                return c;
            }
        }
        return null;
    }


}
