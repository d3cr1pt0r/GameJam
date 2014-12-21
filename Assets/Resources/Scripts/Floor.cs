using UnityEngine;
using System.Collections;

public class Floor
{
	private GameObject tile;
	private int length;

	public Floor(GameObject TilePrefab, Vector3 pos, int length)
	{
		this.length = length;
		tile = GameObject.Instantiate (TilePrefab, pos, Quaternion.identity) as GameObject;
		tile.renderer.materials [0].mainTextureScale = new Vector2 (length, 1);

		Vector3 curScale = tile.transform.localScale;
		curScale.x = 0.3f * length;
		tile.transform.localScale = curScale;
	}

	public Vector3[] GetPoints()
	{
		Vector3 p1 = new Vector3 (tile.transform.position.x - tile.transform.localScale.x / 2.0f, tile.transform.position.y, tile.transform.position.z);
		Vector3 p2 = tile.transform.position;
		Vector3 p3 = new Vector3 (tile.transform.position.x + tile.transform.localScale.x / 2.0f, tile.transform.position.y, tile.transform.position.z);

		return new Vector3[] {p1, p2, p3};
	}

	public void AddEnemy()
	{
		Debug.Log ("Add enemy");
	}

	public int GetLenght()
	{
		return length;
	}

	public Vector3 GetPosition()
	{
		return tile.transform.position;
	}

}
