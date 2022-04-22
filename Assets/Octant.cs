using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octant
{
	public enum Sector
	{
		/*UpLeftFront = 0,
		UpRightFront = 1,
		DownLeftFront = 2,
		DownRightFront = 3,
		UpLeftBack = 4,
		UpRightBack = 5,
		DownLeftBack = 6,
		DownRightBack = 7*/
		LeftDownBack = 0,
		LeftDownFront = 1,
		LeftUpBack = 2,
		LeftUpFront = 3,
		RightDownBack = 4,
		RightDownFront = 5,
		RightUpBack = 6,
		RightUpFront = 7
	}

	public static Vector3[] SectorMoves = {
		new Vector3(0f, 0f, 0f),
		new Vector3(0f, 0f, 1f),
		new Vector3(0f, 1f, 0f),
		new Vector3(0f, 1f, 1f),
		new Vector3(1f, 0f, 0f),
		new Vector3(1f, 0f, 1f),
		new Vector3(1f, 1f, 0f),
		new Vector3(1f, 1f, 1f)
	};

	protected Octant[] sectors;

	public Octant parent;
	public int sizeFactor;

	public int indexInParent;

	public Vector3 position;

	public bool occupied;

	public int octId;


	//public List<GamePlanet> planets;

	public List<Octant> getSpaciallyAdjacentOctants()
	{
		return null;
	}

	public Octant(Octant parent, int numPlanets, int indexInParent, bool occupied)
	{
		this.indexInParent = indexInParent;
		this.parent = parent;
		sectors = null;
		this.occupied = occupied;
		this.sizeFactor = parent.sizeFactor / 2;
		position = parent.position;
		position += (SectorMoves[indexInParent] * parent.sizeFactor);
		Octant current = parent;
		while(current != null)
		{
			octId = parent.octId;
			octId <<= 3;
			current = current.parent;
		}
	}

	public Octant(int sizeFactor, Vector3 position)
	{
		this.sizeFactor = sizeFactor;
		this.position = position;
		occupied = false;
		sectors = null;
		parent = null;
		indexInParent = 0;
		octId = 0;
	}

	public Octant[] Subdivide()
	{
		if(occupied)
		{
			return null;
		}
		if(sectors != null)
		{
			return sectors;
		}
		sectors = new Octant[8];
		for(int index0 = 0; index0 < 8; ++index0)
		{
			if(sizeFactor < 8)
			{
				//Debug.Log("sizeFactor under 8");
				sectors[index0] = new Octant(this, 1, index0, true);
			}
			else
			{
				sectors[index0] = new Octant(this, 0, index0, false);
			}
		}
		return sectors;
	}

	public Octant getSector(int sector)
	{
		if(sector >= 0 && sector < 8)
		{
			return sectors[sector];
		}
		else
		{
			return null;
		}
	}




    
}
