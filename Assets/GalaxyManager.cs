using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyManager : MonoBehaviour
{

	public Rigidbody rb;
	[SerializeField] public List<Rigidbody> rbs;

	public List<GamePlanet> planetPool;

	public uint numPlanets;

	public float planetFixedScale = 0.01f;

	public static GamePlanet gravityCenter;

	public TestSphereGenerator tsg;

    public float g;

    public static GalaxyManager instance;

    public Octant parentOctant;
    // Start is called before the first frame update
    //void Start()
    void Awake()
    {
        parentOctant = new Octant(16, new Vector3(0f, 0f, 0f));
    	//planetPool = new List<GamePlanet>();
    	Debug.Log("GalaxyManager");
        Random.seed = 69;
        planetPool.Add(new GamePlanet(0, tsg));
        planetPool[0].gameObject.transform.Translate(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f));
        planetPool[0].gameObject.transform.localScale += new Vector3(planetFixedScale, planetFixedScale, planetFixedScale);
        planetPool[0].gameObject.tag = "planet_object";
        planetPool[0].gameObject.layer = LayerMask.NameToLayer("planet_object");

        initGalaxyTest();
    	//planetPool.Add(new GamePlanet(0));
    	//planetPool.Add(new GamePlanet(0));
    	//planetPool[1].gameObject.transform.Translate(2,2,2);
     	/*for(int index0 = 0; index0 < numPlanets; ++index0)
    	{
    		planetPool.Add(new GamePlanet(0, tsg));
    		planetPool[index0].gameObject.transform.Translate(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f));
    		planetPool[index0].gameObject.transform.localScale += new Vector3(planetFixedScale, planetFixedScale, planetFixedScale);
            planetPool[index0].gameObject.tag = "planet_object";
            planetPool[index0].gameObject.layer = LayerMask.NameToLayer("planet_object");
    	}*/
    	setGravityCenter(0);

        instance = this;
        
    }

    public GalaxyManager()
    {

    	planetPool = new List<GamePlanet>();

    }

    public void setGravityCenter(int id)
    {
        if(gravityCenter != null)
        {
            gravityCenter.gameObject.name = "planet0";

        }
    	if(rb)
    	{
    		Debug.Log("Rigidbody");
    		rb.useGravity = false;
    	}
    	for(int index0 = 0; index0 < rbs.Count; ++index0)
    	{
    		if(rbs[index0].useGravity)
    		{
    			rbs[index0].useGravity = false;
    		}

    	}
    	gravityCenter = planetPool[id];
        gravityCenter.gameObject.name = "gravityCenter";



    }

    public void processGravity()
    {

    	/*Vector3 d = rb.transform.position - gravityCenter.gameObject.transform.position;
    	rb.AddForce(d.normalized * (-9.81f)/2 * (rb.mass));*/
    	for(int index0 = 0; index0 < rbs.Count; ++index0)
    	{
    		//Debug.Log("rbs count" + rbs.Count);
    		Vector3 d = rbs[index0].transform.position - gravityCenter.gameObject.transform.position;
    		rbs[index0].AddForce(d.normalized * (-g)/2 * (rbs[index0].mass));
    	}

    }

    public static Vector3 getGravityVector(Rigidbody r)
    {
    	return r.transform.position - gravityCenter.gameObject.transform.position;
    }
    public static Vector3 getGravityVector(Transform pos)
    {
        return pos.transform.position - gravityCenter.gameObject.transform.position;
    }
    public static Vector3 getGravityVector(Vector3 vec)
    {
        return vec - gravityCenter.gameObject.transform.position;
    }

    public static void AddRb(Rigidbody r)
    {
        instance.rbs.Add(r);

    }

    public static void RemoveRb(Rigidbody r)
    {
        instance.rbs.Remove(r);
    }

    public void initGalaxyTest()
    {
        parentOctant.Subdivide();
        for(int index0 = 0; index0 < 8; ++index0)
        {
            parentOctant.getSector(index0).Subdivide();
            for(int index1 = 0; index1 < 8; ++index1)
            {
                parentOctant.getSector(index0).getSector(index1).Subdivide();
                for(int index2 = 0; index2 < 8; ++index2)
                {
                    Octant thisOctant = parentOctant.getSector(index0).getSector(index1).getSector(index2);
                    planetPool.Add(new GamePlanet(0, tsg));
                    planetPool[planetPool.Count - 1].gameObject.transform.Translate(thisOctant.position + new Vector3(thisOctant.sizeFactor/2, thisOctant.sizeFactor/2, thisOctant.sizeFactor/2));
                    planetPool[planetPool.Count - 1].gameObject.transform.Translate(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    planetPool[planetPool.Count - 1].gameObject.transform.localScale += new Vector3(planetFixedScale, planetFixedScale, planetFixedScale);
                    planetPool[planetPool.Count - 1].gameObject.tag = "planet_object";
                    planetPool[planetPool.Count - 1].gameObject.layer = LayerMask.NameToLayer("planet_object");
                    

                }
            }

        }
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
        	//Debug.Log("gravityCenter");
        	setGravityCenter(Random.Range(0, planetPool.Count));
        	//setGravityCenter(1);
        }
    }

    void FixedUpdate()
    {
    	processGravity();
    }
}
