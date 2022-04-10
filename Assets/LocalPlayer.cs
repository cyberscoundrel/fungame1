using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player
{

	public Ticking playerTick;

	public object healthLocker = new object();

	public object levelLocker = new object();

	public object collectiblesLocker = new object();

	public object weaponsLocker = new object();

    public LocalPlayer(int maxWeaponCount, int maxItemCount, GameObject prefab) : base(maxWeaponCount, maxItemCount, prefab)
    {
    	playerTick = new Ticking();
    	playerTick.initTick(tickProc);

    }

    public virtual void tickProc()
    {
    	Debug.Log("local player tick 2");
    	Thread.Sleep(1000);//set tick rate of 1 second
    }

    override public void AddCollectible(Collectible c)
    {
    	lock(collectiblesLocker)
    	{
    		//base.AddCollectible(c);
    		collectibles.Add(c);
			c.PickUp(this);
    		if(c.typeFlag == 0x02)
			{
				gameObject.GetComponent<PlayerController>().selectedWeaponNum = this.AddWeapon(c as Weapon);
			}


    	}
    }

    override public void RemoveCollectible(Collectible c)
    {
    	lock(collectiblesLocker)
    	{
    		base.RemoveCollectible(c);

    	}
    }

    override public void RemoveCollectible(int index)
    {
    	lock(collectiblesLocker)
    	{
    		base.RemoveCollectible(index);
    	}
    }

    override public void setLevel(int level)
    {
    	lock(levelLocker)
    	{
    		this.level = level;
    	}
    }

    override public void setHealth(int newHealth)
    {
    	lock(healthLocker)
    	{
    		health = newHealth;
    	}
    }

    override public int AddWeapon(Weapon w)
    {
    	int ret;
    	lock(weaponsLocker)
    	{

    		ret = base.AddWeapon(w);
    		gameObject.GetComponent<LocalPlayerController>().selectWeapon(ret);
    	}
    	return ret;
    }

    void OnDisable()
    {
    	Debug.Log("destroy LocalPlayer");
    	playerTick.ceaseTick();
    }

    void OnApplicationQuit()
    {
    	Debug.Log("quit LocalPlayer");
    	playerTick.ceaseTick();
    }

    //TODO: override player methods to be thread safe, local players will have ticks as well as other threads affecting their live stats, whereas online players have this information streamed serially, you are getting one stream of data from an individual player about their live stats at a particular instant in time
}
