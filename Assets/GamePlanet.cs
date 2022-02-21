using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlanet
{
    // Start is called before the first frame update
    public Rigidbody testGravityObject;

    public List<Rigidbody> gravityObjects;

    public ProceduralSphere proceduralSphere;

    public long planetSeed;

    public GameObject gameObject;
    
    public GamePlanet(long newSeed)
    {
    	this.planetSeed = newSeed;
    	gravityObjects = new List<Rigidbody>();
    	this.gameObject = new GameObject("Planet" + 0000);
    	gameObject.AddComponent<MeshRenderer>();
    	gameObject.AddComponent<MeshFilter>();
    	gameObject.AddComponent<MeshCollider>();
    	proceduralSphere = TestSphereGenerator.generateSphere(3, this.gameObject);


    	





    }

    public void reseed(long newSeed)
    {
    	this.planetSeed = newSeed;
    	gravityObjects = new List<Rigidbody>();
    	proceduralSphere = TestSphereGenerator.generateSphere(3, this.gameObject);

    }
}
