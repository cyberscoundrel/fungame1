using System;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GLObject
{
    public Vector3 position;

    public Vector3 adjustment;

    public Vector3 gridOffset;

    public float radius;

    public List<GLObject> connections;

    public int seed;

    public bool visitable;

    public bool collapsed;

    public GLObject(Vector3 newPosition)
    {
        position = newPosition;
    }

    public Vector3 getAdjusted()
    {
        return position + adjustment;
    }


}

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

    public Vector3 globalPlanetOffset;

    public Vector3 globalGridOffset;

    public int seed;

    public float mDist = 1f;

    //public static int prime = 12289;

    //public List<List<List<List<Vector3>>>> generationGrid;

    //public Vector3[,,,] generationGrid;

    public GLObject[,,,] gGrid;

    //public Vector3[,,,] adjustmentGrid;

    //public Vector3[,,,] 

    public Vector3 generationGridOffset;

    public SHA256 hasher;
    // Start is called before the first frame update
    //void Start()
    void Awake()
    {
        seed = 69;
        hasher = SHA256.Create();
        generationGridOffset = new Vector3(0f,0f,0f);
        parentOctant = new Octant(16, new Vector3(0f, 0f, 0f));
    	//planetPool = new List<GamePlanet>();
    	Debug.Log("GalaxyManager");
        UnityEngine.Random.seed = 69;
        planetPool.Add(new GamePlanet(0, tsg));
        planetPool[0].gameObject.transform.Translate(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-4f, 4f));
        planetPool[0].gameObject.transform.localScale += new Vector3(planetFixedScale, planetFixedScale, planetFixedScale);
        planetPool[0].gameObject.tag = "planet_object";
        planetPool[0].gameObject.layer = LayerMask.NameToLayer("planet_object");

        initGalaxyTest(planetPool[0].gameObject.transform.position);
        initGalaxyTestGrid();
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
                    if(UnityEngine.Random.Range(0,8) == 0)
                    {
                        planetPool.Add(new GamePlanet(0, tsg));
                        planetPool[planetPool.Count - 1].gameObject.transform.Translate(thisOctant.position + new Vector3(thisOctant.sizeFactor/2, thisOctant.sizeFactor/2, thisOctant.sizeFactor/2));
                        planetPool[planetPool.Count - 1].gameObject.transform.Translate(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                        planetPool[planetPool.Count - 1].gameObject.transform.localScale += new Vector3(planetFixedScale, planetFixedScale, planetFixedScale);
                        planetPool[planetPool.Count - 1].gameObject.tag = "planet_object";
                        planetPool[planetPool.Count - 1].gameObject.layer = LayerMask.NameToLayer("planet_object");
                    }
                    

                }
            }

        }
    }

    public void initGalaxyTestGrid()
    {

        //generationGrid = new Vector3[8,8,8,2];
        gGrid = new GLObject[8,8,8,2];

        //phase 0
        for(int index0 = 0; index0 < 8; ++index0)
        {
            //generationGrid[index0].Add(new List<List<List<Vector3>>>());
            //generationGrid[index0] = new Vector3[8][8][2];
            for(int index1 = 0; index1 < 8; ++index1)
            {
                //generationGrid[index0][index1].Add(new List<List<Vector3>>());
                //generationGrid[index0][index1] = new Vector3[8][2];
                for(int index2 = 0; index2 < 8; ++index2)
                {

                    //generationGrid[index0][index1][index2] = new Vector3[2];
                    //generationGrid[index0,index1,index2,0] = new Vector3(Random.Range(0f, 2f) + (index0 * 2), Random.Range(0f, 2f) + (index1 * 2), Random.Range(0f, 2f) + (index2 * 2));
                    //generationGrid[index0,index1,index2,1] = new Vector3(Random.Range(0f, 2f) + (index0 * 2), Random.Range(0f, 2f) + (index1 * 2), Random.Range(0f, 2f) + (index2 * 2));
                    //float pseudoRandX = (((index0 + globalGridOffset.x + seed) * prime) % prime)
                    float pseudoRandX0 = BitConverter.ToInt32(hasher.ComputeHash(BitConverter.GetBytes((((index0 + globalGridOffset.x) * 100) + ((index1 + globalGridOffset.y) * 10) + (index2 + globalGridOffset.z)) + index0 + seed + 0)), 0) / 4294967296f;
                    float pseudoRandY0 = BitConverter.ToInt32(hasher.ComputeHash(BitConverter.GetBytes((((index0 + globalGridOffset.x) * 100) + ((index1 + globalGridOffset.y) * 10) + (index2 + globalGridOffset.z)) + index1 + seed + 0)), 0) / 4294967296f;
                    float pseudoRandZ0 = BitConverter.ToInt32(hasher.ComputeHash(BitConverter.GetBytes((((index0 + globalGridOffset.x) * 100) + ((index1 + globalGridOffset.y) * 10) + (index2 + globalGridOffset.z)) + index2 + seed + 0)), 0) / 4294967296f;
                    float pseudoRandX1 = BitConverter.ToInt32(hasher.ComputeHash(BitConverter.GetBytes((((index0 + globalGridOffset.x) * 100) + ((index1 + globalGridOffset.y) * 10) + (index2 + globalGridOffset.z)) + index0 + seed + 1)), 0) / 4294967296f;
                    float pseudoRandY1 = BitConverter.ToInt32(hasher.ComputeHash(BitConverter.GetBytes((((index0 + globalGridOffset.x) * 100) + ((index1 + globalGridOffset.y) * 10) + (index2 + globalGridOffset.z)) + index1 + seed + 1)), 0) / 4294967296f;
                    float pseudoRandZ1 = BitConverter.ToInt32(hasher.ComputeHash(BitConverter.GetBytes((((index0 + globalGridOffset.x) * 100) + ((index1 + globalGridOffset.y) * 10) + (index2 + globalGridOffset.z)) + index2 + seed + 1)), 0) / 4294967296f;

                    //gGrid[index0,index1,index2,0] = new GLObject(new Vector3(Random.Range(0f, 2f) + (index0 * 2), Random.Range(0f, 2f) + (index1 * 2), Random.Range(0f, 2f) + (index2 * 2)));
                    //gGrid[index0,index1,index2,1] = new GLObject(new Vector3(Random.Range(0f, 2f) + (index0 * 2), Random.Range(0f, 2f) + (index1 * 2), Random.Range(0f, 2f) + (index2 * 2)));
                    //gGrid[index0,index1,index2,0] = new GLObject(new Vector3((pseudoRandX0 * 2f) + ((index0 + globalGridOffset.x) * 2), (pseudoRandY0 * 2f) + ((index1 + globalGridOffset.y) * 2), (pseudoRandZ0 * 2f) + ((index2 + globalGridOffset.z) * 2)));
                    //gGrid[index0,index1,index2,1] = new GLObject(new Vector3((pseudoRandX1 * 2f) + ((index0 + globalGridOffset.x) * 2), (pseudoRandY1 * 2f) + ((index1 + globalGridOffset.y) * 2), (pseudoRandZ1 * 2f) + ((index2 + globalGridOffset.z) * 2)));
                    //gGrid[index0,index1,index2,0].gridOffset = new Vector3(index0, index1, index2);
                    gGrid[index0,index1,index2,0] = new GLObject(new Vector3((pseudoRandX0 * 2f), (pseudoRandY0 * 2f), (pseudoRandZ0 * 2f)));
                    gGrid[index0,index1,index2,1] = new GLObject(new Vector3((pseudoRandX1 * 2f), (pseudoRandY1 * 2f), (pseudoRandZ1 * 2f)));
                    gGrid[index0,index1,index2,0].gridOffset = new Vector3(index0 * 2, index1 * 2, index2 * 2);
                    gGrid[index0,index1,index2,1].gridOffset = new Vector3(index0 * 2, index1 * 2, index2 * 2);
                    //generationGrid[index0][index1][index2].Add(new List<Vector3>());
                    //List<Vector3
                }
            }
        }
        //phase 1 space adjustment
        for(int index0 = 1; index0 < 7; ++index0)
        {
            //generationGrid[index0].Add(new List<List<List<Vector3>>>());
            //generationGrid[index0] = new Vector3[][][8];
            for(int index1 = 1; index1 < 7; ++index1)
            {
                //generationGrid[index0][index1].Add(new List<List<Vector3>>());
                //generationGrid[index0][index1] = new Vector3[][8];
                for(int index2 = 1; index2 < 7; ++index2)
                {

                    for(int index3 = -1; index3 < 2; ++index3)
                    {

                        for(int index4 = -1; index4 < 2; ++index4)
                        {

                            for(int index5 = -1; index5 < 2; ++index5)
                            {
                                //gGrid[index0,index1,index2,0].adjustment += 

                            }
                        }
                    }
                    


                    //generationGrid[index0][index1][index2] = new Vector3[2];
                    //generationGrid[index0][index1][index2][0] = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
                    //generationGrid[index0][index1][index2][1] = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
                    //generationGrid[index0][index1][index2].Add(new List<Vector3>());
                    //List<Vector3
                }
            }
        }
        //phase 2 radius calculation
        for(int index0 = 2; index0 < 6; ++index0)
        {

            for(int index1 = 2; index1 < 6; ++ index1)
            {

                for(int index2 = 2; index2 < 6; ++index2)
                {

                    for(int index3 = -1; index3 < 2; ++index3)
                    {

                        for(int index4 = -1; index4 < 2; ++index4)
                        {

                            for(int index5 = -1; index5 < 2; ++index5)
                            {
                                //gGrid[index0,index1,index2,0].adjustment += (gGrid[index0,index1,index2,1])
                                //gGrid[index0,index1,index2,0].adjustment += 

                            }
                        }
                    }
                }
            }
        }
        //phase 3 planet instantiation
        for(int index0 = 2; index0 < 6; ++index0)
        {

            for(int index1 = 2; index1 < 6; ++ index1)
            {

                for(int index2 = 2; index2 < 6; ++index2)
                {

                    for(int index3 = -1; index3 < 2; ++index3)
                    {

                        for(int index4 = -1; index4 < 2; ++index4)
                        {

                            for(int index5 = -1; index5 < 2; ++index5)
                            {
                                //gGrid[index0,index1,index2,0].adjustment += 

                            }
                        }
                    }
                }
            }
        }


    }

    /*public void initGalaxyTestGrid(Vector3 gOffset)
    {

    }*/

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;


        for(int index0 = 0; index0 < 8; ++index0)
        {
            //generationGrid[index0].Add(new List<List<List<Vector3>>>());
            //generationGrid[index0] = new Vector3[][][8];
            for(int index1 = 0; index1 < 8; ++index1)
            {
                //generationGrid[index0][index1].Add(new List<List<Vector3>>());
                //generationGrid[index0][index1] = new Vector3[][8];
                for(int index2 = 0; index2 < 8; ++index2)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(new Vector3(index0 * 2, index1 * 2, index2 * 2), new Vector3(2, 2, 2));
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(gGrid[index0,index1,index2,0].position + gGrid[index0,index1,index2,0].gridOffset, 0.1f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(gGrid[index0,index1,index2,1].position + gGrid[index0,index1,index2,1].gridOffset, 0.1f);




                    //generationGrid[index0][index1][index2] = new Vector3[2];
                    //generationGrid[index0][index1][index2][0] = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
                    //generationGrid[index0][index1][index2][1] = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
                    //generationGrid[index0][index1][index2].Add(new List<Vector3>());
                    //List<Vector3
                }
            }
        }
        //draw gizmos for grid galaxy generation
    }


    public void initGalaxyTest(Vector3 initPosition)
    {
        globalPlanetOffset = new Vector3(parentOctant.sizeFactor / 2 + 1, parentOctant.sizeFactor / 2 + 1, parentOctant.sizeFactor / 2 + 1);
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
                    if(!(index0 == 2 && index1 == 0 && index2 == 0))
                    {
                        if(UnityEngine.Random.Range(0,8) == 0)
                        {

                            planetPool.Add(new GamePlanet(0, tsg));
                            planetPool[planetPool.Count - 1].gameObject.transform.Translate(thisOctant.position + new Vector3(thisOctant.sizeFactor/2, thisOctant.sizeFactor/2, thisOctant.sizeFactor/2));
                            planetPool[planetPool.Count - 1].gameObject.transform.Translate(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                            planetPool[planetPool.Count - 1].gameObject.transform.Translate(-globalPlanetOffset);
                            float planetRandomScale = UnityEngine.Random.Range(1f, 50f) * planetFixedScale;
                            planetPool[planetPool.Count - 1].gameObject.transform.localScale += new Vector3(planetRandomScale, planetRandomScale, planetRandomScale);
                            planetPool[planetPool.Count - 1].gameObject.tag = "planet_object";
                            planetPool[planetPool.Count - 1].gameObject.layer = LayerMask.NameToLayer("planet_object");
                        }
                    }
                    

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
        	setGravityCenter(UnityEngine.Random.Range(0, planetPool.Count));
        	//setGravityCenter(1);
        }
    }

    void FixedUpdate()
    {
    	processGravity();
    }
}
