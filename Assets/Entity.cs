using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
	//will be base class for player and enemy
    // Start is called before the first frame update
    public int uTag = -1;

    public virtual int getUTag()
    {
    	return uTag;
    }

    public virtual void setUTag(int uTag)
    {
    	this.uTag = uTag;
    }
}
