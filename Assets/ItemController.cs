using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    public Item itemObject;

    public int typeFlag;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract float behavior(Player p);

    //public abstract void affect()
}
