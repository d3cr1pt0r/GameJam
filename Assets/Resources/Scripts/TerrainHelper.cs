using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainHelper : MonoBehaviour
{
	public GameObject TilePrefab;
	public List<Floor> floors;

	void Start ()
	{
		floors = new List<Floor> ();
	}

	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			floors.Add (CreateFloor(new Vector3(pos.x, pos.y, 5), 8));
		}

	}

	public Floor CreateFloor(Vector3 pos, int length)
	{
		return new Floor (TilePrefab, pos, length);
	}

}
