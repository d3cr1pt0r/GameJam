using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainHelper : MonoBehaviour
{
	public GameObject TilePrefab;
	
	public List<Floor> floors;
	public Vector3 previous;
	void Start ()
	{
		floors = new List<Floor> ();
		previous = TilePrefab.transform.position;
		generateTiles (30);
		//generateTile ();
		//generateTile ();
		
	}
	void generateTile()
	{
		Random.seed = (int)System.DateTime.Now.Ticks;
		
		Vector3 borderPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height/2.0f,5));
		
		float max_side = 0.3f;
		float max_up = 0.05f;
		float max_x= Random.Range (-(previous.x-max_side),previous.x+max_side);
		int length = (int)Random.Range (2f, 8f);
		
		floors.Add (CreateFloor(new Vector3(max_x, previous.y+max_up, 5), length));
		previous = new Vector3 (max_x, previous.y + max_up, 5);
	}
	
	bool isLegitPosition(Vector3 new_pos, Vector3 prev_pos)
	{
		return true;
	}
	void generateTiles(int num)
	{
		previous = TilePrefab.transform.position;
		
		Random.seed = (int)System.DateTime.Now.Ticks;
		float max_up = 1.5f;
		for (int i =0; i<num; i++)
		{
			Vector3 borderPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height/2.0f,5));
			
			float max_x= Random.Range (0,borderPos.x);
			float max_y= Random.Range (max_x, previous.y+1.5f);
			int length = (int)Random.Range (6f, 10f);
			floors.Add (CreateFloor(new Vector3(max_x, previous.y+max_up, 5), length));
			previous.y += max_up;
			
			
		}
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
