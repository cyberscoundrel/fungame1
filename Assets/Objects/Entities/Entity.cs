using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
	//will be base class for player and enemy
    // Start is called before the first frame update
    public GameObject gameObject;

    public int uTag = -1;

    protected List<Collectible> collectibles;

    public float baseSpd;

    public float baseDmg;

    public float baseTick;

    public float baseRegen;

    protected int level;

    public int baseHealth;

    protected int health;



    public virtual int getUTag()
    {
    	return uTag;
    }

    public virtual void setUTag(int uTag)
    {
    	this.uTag = uTag;
    }

    public virtual void AddCollectible(Collectible c)
    {
        collectibles.Add(c);
        c.PickUp(this);
        //TODO:check item tFlag to see if it must be added from weapons
        /*if(c.typeFlag == 0x02)
        {
            gameObject.GetComponent<PlayerController>().selectedWeaponNum = AddWeapon(c as Weapon);
        }*/
    }

    public virtual void RemoveCollectible(Collectible c)
    {
        collectibles.Remove(c);
        c.Discard();
        //TODO:check item tFlag to see if it must be removed from weapons
        /*if(c.typeFlag == 0x02)
        {
            //int count = 0;
            for(int index0 = 0; index0 < weapons.Length; index0++)
            {
                if(weapons[index0] == c)
                {
                    weapons[index0] = null;
                    weaponCount--;
                }
                //coount++;
            }
        }*/
    }

    public virtual void RemoveCollectible(int index)
    {
        collectibles.RemoveAt(index);
        //TODO:check item tFlag to see if it must be removed from weapons
    }

    public virtual void setLevel(int level)
    {
        this.level = level;
    }

    public virtual int getLevel()
    {
        return level;
    }

    public virtual void setHealth(int newHealth)
    {
        health = newHealth;
    }

    public virtual int getHealth()
    {
        return health;
    }
}
