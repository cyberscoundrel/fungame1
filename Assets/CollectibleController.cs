using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{


	Collectible collectibleObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected CollectibleController()
    {

    }

    public virtual void behavior()
    {

    }

    public Collectible getManager()
    {
    	return collectibleObject;

    }

    public void setManager(Collectible c)
    {
    	collectibleObject = c;
    }
}
