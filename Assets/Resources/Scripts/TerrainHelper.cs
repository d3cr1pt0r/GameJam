using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainHelper : MonoBehaviour
{
	public GameObject TilePrefab;
	public GameObject SnowmanPrefab;
	
	public List<Floor> floors;
	public List<Floor> enemy;
	
	public Vector3 previous;
	void Start ()
	{
		floors = new List<Floor> ();
		previous = TilePrefab.transform.position;
		generateTiles (10);
		spawnSnowman ();
		//generateTile ();
		//generateTile ();
		
	}
	
	bool inRange(Vector3 new_pos, Vector3 prev_pos)
	{
		if (Vector3.Distance (new_pos, prev_pos) < 5f)
		{
			return true;
			
		}
		return false;
	}
	
	void spawnSnowman()
	{
		Random.seed = (int)System.DateTime.Now.Ticks;
		
		for (int i = 0; i< floors.Count; i++) {
			float propability = Random.Range (0,floors.Count);
			
			if(propability%3 == 0)
			{
				Vector3[] pos = floors[i].GetPoints();
				
				pos[1].y +=0.75f;
				
				floors.Add (CreateEnemy(new Vector3(pos[1].x,pos[1].y, 5)));
			}
		}
	}
	
	void generateTiles(int num)
	{
		previous = TilePrefab.transform.position;
		Vector3 new_pos;
		
		Random.seed = (int)System.DateTime.Now.Ticks;
		float max_up = 2.5f;
		float max_side = 1.5f;
		
		for (int i =0; i<num; i++)
		{
			Vector3 borderPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height/2.0f,5));
			
			float max_x= Random.Range (0-borderPos.x-max_side,borderPos.x+max_side);
			float max_y= Random.Range (max_x, previous.y+max_side);
			
			int length = (int)Random.Range (9f, 17f);
			
			new_pos = new Vector3(max_x, previous.y+max_up, 5);
			
			float dist = Vector3.Distance(previous, new_pos);
			Debug.Log (dist);
			
			while(!inRange(new_pos,previous))
			{
				if(new_pos.x < previous.x)
				{
					new_pos.x+= 2.5f;
				}
				else
				{
					new_pos.x-= 2.5f;
					
				}
			}
			floors.Add (CreateFloor(new_pos, length));
			
			previous.y += max_up;
			
			
		}
	}
	
	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			//Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//floors.Add (CreateFloor(new Vector3(pos.x, pos.y, 5), 8));
		}
		
	}
	
	public Floor CreateFloor(Vector3 pos, int length)
	{
		return new Floor (TilePrefab, pos, length);
	}
	
	public Floor CreateEnemy(Vector3 pos)
	{
		return new Floor (SnowmanPrefab, pos);
	}
}
