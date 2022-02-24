using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuiTest : MonoBehaviour
{

	public GalaxyManager gMan;

	public GameObject player;

	[SerializeField] public List<Material> pMat;

	public int pMatIndex = 0;
	

	public Vector3 p;

	public Camera c;

	public Light l;

	
    // Start is called before the first frame update
    void Start()
    {
    	//pMat = new List<Material>();
        
    }

    // Update is called once per frame
    void Update()
    {

    	if(Input.GetMouseButton(0))
    	{
    		Ray ray = c.ScreenPointToRay(Input.mousePosition);
    		RaycastHit hit;
    		Debug.DrawRay(ray.origin, ray.origin + new Vector3(130, 110, 120), Color.magenta);
    		if(Physics.Raycast(ray, out hit, 1000f))
    		{
    			Debug.Log("ray");
    			if(hit.transform != null)
    			{

    				if(hit.transform.gameObject.tag == "testcollisioncube")
    				{
    					Plane plane = new Plane(Vector3.up, hit.transform.position);
						float f = 0f;
						plane.Raycast(ray, out f);
						p = ray.GetPoint(f);
						Debug.Log(hit.transform.gameObject.name);
    					hit.transform.position = Vector3.MoveTowards(hit.transform.position, p, 10f * Time.deltaTime);

    				}
    			}
    		}

    	}
        
    }

    void OnGUI()
    {
    	if(GUI.Button(new Rect(10, 10, 100, 40), "Scene Swap"))
    	{
    		Debug.Log("swap");
    		if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SampleScene"))
    		{

    			SceneManager.LoadScene("scene2");

    		}
    		else
    		{
    			SceneManager.LoadScene("SampleScene");
    		}
    	}
    	if(GUI.Button(new Rect(110, 10, 100, 40), "Material Swap"))
    	{
    		Debug.Log("mat swap");
    		pMatIndex += 1;
    		pMatIndex = pMatIndex % pMat.Count;
    		for(int index0 = 0; index0 < gMan.planetPool.Count; ++index0)
    		{
    			gMan.planetPool[index0].gameObject.GetComponent<MeshRenderer>().material = pMat[pMatIndex];
    		}
    	}
    	if(GUI.Button(new Rect(220, 10, 200, 40), "Do a Sick Jump"))
    	{
    		Debug.Log("jump");
    		player.GetComponent<Rigidbody>().AddForce(player.transform.up * 200f);
    		player.GetComponent<Rigidbody>().AddTorque(player.transform.forward * 2000f * 2000f);
    	}
    	if(GUI.Button(new Rect(430, 10, 200, 40), "Sleepy Mode"))
    	{
    		Debug.Log("sleepy mode");
    		if(l.intensity < 1)
    		{
    			l.intensity = 1;
    		}
    		else
    		{
    			l.intensity = 0.01f;
    		}

    	}
    }
}
