using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSphere
{

	//List<List<int>> p;

	//List<Vector3> v;

	GameObject g;

	Mesh m;

	MeshRenderer mr;

	MeshFilter mf;

	MeshCollider mc;

	public Material mat;

	//public List<Rigidbody> gravityBodies;

	public ProceduralSphere(Mesh newMesh, Material newMaterial, GameObject gamePlanet)
	{

		this.g = gamePlanet;

		this.m = newMesh;

		this.mat = newMaterial;

		//g = new GameObject("Planet Mesh");

        //mr = g.AddComponent<MeshRenderer>();
        mr = g.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        if(mr)
        {
        	mr.material     = mat;
        }

        
        //mf = g.AddComponent<MeshFilter>();
        mf = g.GetComponent(typeof(MeshFilter)) as MeshFilter;
        if(mf)
        {
        	mf.mesh = m;
        }


        //TODO: mesh cooking

        //mc = g.AddComponent<MeshCollider>();
        mc = g.GetComponent(typeof(MeshCollider)) as MeshCollider;
        if(mc)
        {

        	mc.sharedMesh = m;
        }



	}

	public void reloadSphere(Mesh newMesh, Material newMaterial, GameObject gamePlanet)
	{
		this.g = gamePlanet;

		this.m = newMesh;

		this.mat = newMaterial;

		//g = new GameObject("Planet Mesh");

        //mr = g.AddComponent<MeshRenderer>();
        mr = g.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        if(mr)
        {
        	mr.material     = mat;
        }

        
        //mf = g.AddComponent<MeshFilter>();
        mf = g.GetComponent(typeof(MeshFilter)) as MeshFilter;
        if(mf)
        {
        	mf.mesh = m;
        }


        //TODO: mesh cooking

        //mc = g.AddComponent<MeshCollider>();
        mc = g.GetComponent(typeof(MeshCollider)) as MeshCollider;
        if(mc)
        {

        	mc.sharedMesh = m;
        }
	}



	/*public ProceduralSphere(List<List<int>> p0, List<Vector3> v0, Material mat0)
	{

		this.p = p0;
		this.v = v0;
		this.mat = mat0;
		gravityBodies = new List<Rigidbody>();
		g = new GameObject("Planet Mesh");

        mr = g.AddComponent<MeshRenderer>();
        mr.material     = mat;

         m = new Mesh();

        int vertexCount = p.Count * 3;

        int[] indices = new int[vertexCount];

        Vector3[] vertices = new Vector3[vertexCount];
        Vector3[] normals  = new Vector3[vertexCount];
        Color32[] colors   = new Color32[vertexCount];

        Color32 green = new Color32(20,  255, 30, 255);
        Color32 brown = new Color32(220, 150, 70, 255);

        for (int i = 0; i < p.Count; i++)
        {
            var poly = p[i];

            indices[i * 3 + 0] = i * 3 + 0;
            indices[i * 3 + 1] = i * 3 + 1;
            indices[i * 3 + 2] = i * 3 + 2;

            vertices[i * 3 + 0] = v[poly[0]];
            vertices[i * 3 + 1] = v[poly[1]];
            vertices[i * 3 + 2] = v[poly[2]];

            Color32 polyColor = Color32.Lerp(green, brown, Random.Range(0.0f, 1.0f)); 

            colors[i * 3 + 0] = polyColor;
            colors[i * 3 + 1] = polyColor;
            colors[i * 3 + 2] = polyColor;

            normals[i * 3 + 0] = v[poly[0]];
            normals[i * 3 + 1] = v[poly[1]];
            normals[i * 3 + 2] = v[poly[2]];
        }

        m.vertices = vertices;
        m.normals  = normals;
        m.colors32 = colors;

        m.SetTriangles(indices, 0);

        mf = g.AddComponent<MeshFilter>();
        mf.mesh = m;


        //TODO: mesh cooking

        mc = g.AddComponent<MeshCollider>();
        mc.sharedMesh = m;

        Debug.Log(vertices.Length);
        Debug.Log(indices.Length);

        /*foreach(Vector3 i in vertices)
        {
        	Debug.Log(" " + i[0] + " " + i[1] + " " + i[2]);
        }*/

		/*Debug.Log("here1");
		this.p = p;
		this.v = v;
		this.mat = mat;

		g = new GameObject("mesh");

		mr = g.AddComponent<MeshRenderer>();
		mr.material = mat;

		m = new Mesh();

		int vertexCount = p.Count * 3;

		Debug.Log("pcount " + p.Count);

		int[] indices = new int[vertexCount];

		Vector3[] vertices = new Vector3[vertexCount];
		Vector3[] normals = new Vector3[vertexCount];
		Color32[] colors = new Color32[vertexCount];

		Color32 green = new Color32(20, 255, 30, 255);
		Color32 brown = new Color32(220, 150, 70, 255);

		for(int index0 = 0; index0 < p.Count; ++index0)
		{
			List<int> poly = p[index0];

			indices[index0*3 + 0] = index0 * 3 + 0;
			indices[index0*3 + 1] = index0 * 3 + 1;
			indices[index0*3 + 2] = index0 * 3 + 2;

			vertices[index0 * 3 + 0] = v[poly[0]];
			vertices[index0 * 3 + 1] = v[poly[1]];
			vertices[index0 * 3 + 2] = v[poly[2]];

			Color32 color = Color32.Lerp(green, brown, Random.Range(0.0f, 1.0f));

			colors[index0 * 3 + 0] = color;
			colors[index0 * 3 + 1] = color;
			colors[index0 * 3 + 2] = color;

			normals[index0 * 3 + 0] = v[poly[0]];
			normals[index0 * 3 + 1] = v[poly[1]];
			normals[index0 * 3 + 2] = v[poly[2]];
		}

	m.vertices = vertices;
	m.normals = normals;
	m.colors32 = colors;

	m.SetTriangles(indices, 0);

	mf = g.AddComponent<MeshFilter>();
	mf.mesh = m;*/



	//}
}
