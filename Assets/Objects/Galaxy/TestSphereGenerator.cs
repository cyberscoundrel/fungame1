using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphereGenerator : MonoBehaviour
{
    // Start is called before the first frame update
	/*public static List<Vector3> v;
	public static List<List<int>> p;

	public static List<ProceduralSphere> l = new List<ProceduralSphere>();
	public static Material m;
	public static List<Mesh> prebuiltMeshes = new List<Mesh>();
	public static List<MeshCollider> cookedMeshColliders = new List<MeshCollider>();
	public static List<Material> materialPool = new List<Material>();
	public static List<MeshRenderer> mrPool = new List<MeshRenderer>();
	public static List<MeshFilter> mfPool = new List<MeshFilter>();*/
	public List<Vector3> v;
	public List<List<int>> p;

	public List<ProceduralSphere> l = new List<ProceduralSphere>();
	public Material m;
	public List<Mesh> prebuiltMeshes = new List<Mesh>();
	public List<MeshCollider> cookedMeshColliders = new List<MeshCollider>();
	public List<Material> materialPool = new List<Material>();
	public List<MeshRenderer> mrPool = new List<MeshRenderer>();
	public List<MeshFilter> mfPool = new List<MeshFilter>();

	public List<GameObject> environmentPrefabs;

	public TestSphereGenerator instance;

	public void Start()
	{
		instance = this;
		//Debug.Log("TestSphereGenerator start");
		//v = new List<Vector3>();
		//p = new List<List<int>>();
		//generateIcosahedraMeshes(5);
		//generateIcosahedron();

		//ProceduralSphere s = new ProceduralSphere(p, v, m);
		//l.Add(s);
		//generateSphere(3);
	}

	//public TestSphereGenerator()
	public void Awake()
	{
		Debug.Log("here");
		v = new List<Vector3>();
		p = new List<List<int>>();
		generateIcosahedraMeshes(5);
		//l = new List<ProceduralSphere>();
		/*v = new List<Vector3>();
		p = new List<List<int>>();*/
		//generateIcosahedron();



	}

	public void generateIcosahedraMeshes(int n)
	{
		if(prebuiltMeshes.Count == 0)
		{
			generateIcosahedron();

		}
		//generateIcosahedron();
		var generatedMidPoints = new Dictionary<int, int>();
		for (int index0 = prebuiltMeshes.Count; index0 < n; ++index0)
		{
			generateMeshFromPolys();


			var newPolys = new List<List<int>>();
			foreach(var i in p)
			{
				int a = i[0];
				int b = i[1];
				int c = i[2];

				int ab = getMidPointIndex(generatedMidPoints, a, b);
				int bc = getMidPointIndex(generatedMidPoints, b, c);
				int ca = getMidPointIndex(generatedMidPoints, c, a);

				newPolys.Add(new List<int> {a, ab, ca});
				newPolys.Add(new List<int> {b, bc, ab});
				newPolys.Add(new List<int> {c, ca, bc});
				newPolys.Add(new List<int> {ab, bc, ca});

				p = newPolys;


			}
		}


	}

	public Mesh generateMeshFromPolys()
	{

        prebuiltMeshes.Add(new Mesh());

        //int vertexCount = p.Count;

        int[] indices = new int[p.Count * 3];

        //Vector3[] vertices = new Vector3[v.Count];
        //Vector3[] normals  = new Vector3[vertexCount];
        //Color32[] colors   = new Color32[vertexCount];

        //Color32 green = new Color32(20,  255, 30, 255);
        //Color32 brown = new Color32(220, 150, 70, 255);


        //icosahedra polygon divider, splits triangle faces into 3 triangles
        for (int i = 0; i < p.Count; i++)
        {
            var poly = p[i];

            indices[i * 3 + 0] = poly[0];
            indices[i * 3 + 1] = poly[1];
            indices[i * 3 + 2] = poly[2];



            //vertices[i] = v[i];

            /*vertices[i * 3 + 0] = v[poly[0]];
            vertices[i * 3 + 1] = v[poly[1]];
            vertices[i * 3 + 2] = v[poly[2]];*/

            //Color32 polyColor = Color32.Lerp(green, brown, Random.Range(0.0f, 1.0f)); 

            //colors[i * 3 + 0] = polyColor;
            //colors[i * 3 + 1] = polyColor;
            //colors[i * 3 + 2] = polyColor;

            /*normals[i * 3 + 0] = v[poly[0]];
            normals[i * 3 + 1] = v[poly[1]];
            normals[i * 3 + 2] = v[poly[2]];*/
        }

        meshNoise(v);





        prebuiltMeshes[prebuiltMeshes.Count - 1].vertices = v.ToArray();
        prebuiltMeshes[prebuiltMeshes.Count - 1].normals  = v.ToArray();
        //prebuiltMeshes[prebuiltMeshes.Count - 1].colors32 = colors;

        prebuiltMeshes[prebuiltMeshes.Count - 1].SetTriangles(indices, 0);

        Physics.BakeMesh(prebuiltMeshes[prebuiltMeshes.Count - 1].GetInstanceID(), false);

        return prebuiltMeshes[prebuiltMeshes.Count - 1];

        //TODO: mesh cooking
	}


	//TODO: eliminate this function replace with prebuilt icosahedron mesh for cleaner code
	public void generateIcosahedron()
	{
		float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

		//float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        v.Add(new Vector3(-1,  t,  0).normalized);
        v.Add(new Vector3( 1,  t,  0).normalized);
        v.Add(new Vector3(-1, -t,  0).normalized);
        v.Add(new Vector3( 1, -t,  0).normalized);
        v.Add(new Vector3( 0, -1,  t).normalized);
        v.Add(new Vector3( 0,  1,  t).normalized);
        v.Add(new Vector3( 0, -1, -t).normalized);
        v.Add(new Vector3( 0,  1, -t).normalized);
        v.Add(new Vector3( t,  0, -1).normalized);
        v.Add(new Vector3( t,  0,  1).normalized);
        v.Add(new Vector3(-t,  0, -1).normalized);
        v.Add(new Vector3(-t,  0,  1).normalized);

        // And here's the formula for the 20 sides,
        // referencing the 12 vertices we just created.

        p.Add(new List<int> { 0, 11,  5});
        p.Add(new List<int> { 0,  5,  1});
        p.Add(new List<int> { 0,  1,  7});
        p.Add(new List<int> { 0,  7, 10});
        p.Add(new List<int> { 0, 10, 11});
        p.Add(new List<int> { 1,  5,  9});
        p.Add(new List<int> { 5, 11,  4});
        p.Add(new List<int> { 11, 10,  2});
        p.Add(new List<int> { 10,  7,  6});
        p.Add(new List<int> {  7,  1,  8});
        p.Add(new List<int> {  3,  9,  4});
        p.Add(new List<int> {  3,  4,  2});
        p.Add(new List<int> {  3,  2,  6});
        p.Add(new List<int> {  3,  6,  8});
        p.Add(new List<int> {  3,  8,  9});
        p.Add(new List<int> {  4,  9,  5});
        p.Add(new List<int> {  2,  4, 11});
        p.Add(new List<int> {  6,  2, 10});
        p.Add(new List<int> {  8,  6,  7});
        p.Add(new List<int> {  9,  8,  1});



	}

	public int getMidPointIndex(Dictionary<int, int> cache, int indexA, int indexB)
	{
		int smallerIndex = Mathf.Min(indexA, indexB);
		int greaterIndex = Mathf.Max(indexA, indexB);

		int key = (smallerIndex << 16) + greaterIndex;

		int ret;

		if(cache.TryGetValue(key, out ret))
		{
			return ret;
		}

		Vector3 p1 = v[indexA];
		Vector3 p2 = v[indexB];
		Vector3 middle = Vector3.Lerp(p1, p2, 0.5f).normalized;

		ret = v.Count;
		v.Add(middle);

		cache.Add(key, ret);

		return ret;
	}

	public void meshNoise(List<Vector3> mv)
	{
		for(int index0 = 0; index0 < mv.Count; ++index0)
		{
			mv[index0] *= Random.Range(1f, 1.05f);
		}
		//return mv;

	}

	public ProceduralSphere generateSphere(int interations, GameObject newGameObject)
	{

		generateIcosahedraMeshes(interations + 1);
		//TODO: impliment procedural mesh noise into icosahedra meshes on fetch


		//var midPointCache = new Dictionary<int, int>();

		/*for(int index0 = 0; index0 < interations; ++index0)
		{
			var newPolys = new List<List<int>>();
			foreach(var i in p)
			{
				int a = i[0];
				int b = i[1];
				int c = i[2];

				int ab = getMidPointIndex(midPointCache, a, b);
				int bc = getMidPointIndex(midPointCache, b, c);
				int ca = getMidPointIndex(midPointCache, c, a);

				newPolys.Add(new List<int> {a, ab, ca});
				newPolys.Add(new List<int> {b, bc, ab});
				newPolys.Add(new List<int> {c, ca, bc});
				newPolys.Add(new List<int> {ab, bc, ca});

				p = newPolys;


			}
		}*/

		//ProceduralSphere s = new ProceduralSphere(p, v, m);
		ProceduralSphere s = new ProceduralSphere(prebuiltMeshes[interations], m, newGameObject);
		//GamePlanet g = new GamePlanet(0x6969696969696969);

		//l.Add(s);
		return s;
		//return s;





	}






    
}
