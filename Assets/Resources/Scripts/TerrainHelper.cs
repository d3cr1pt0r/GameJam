using UnityEngine;
using System.Collections;

public class TerrainHelper : MonoBehaviour
{
	public GameObject TilePrefab;

	void Start ()
	{
		
	}

	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			CreateFloor(new Vector2(pos.x, pos.y), 5);
		}
	}

	private void CreateTile(Vector2 pos)
	{
		Instantiate (TilePrefab, new Vector3 (pos.x, pos.y, 5), Quaternion.identity);
	}

	public void CreateFloor(Vector2 pos, int length)
	{
		for (float i=pos.x; i<=pos.x+(length*TilePrefab.transform.localScale.x); i+= TilePrefab.transform.localScale.x)
		{
			CreateTile(new Vector2(i, pos.y));
		}
	}

}
