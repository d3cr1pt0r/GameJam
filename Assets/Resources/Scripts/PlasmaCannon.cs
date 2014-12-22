using UnityEngine;
using System.Collections;

public class PlasmaCannon : MonoBehaviour
{
	public GameObject ProjectilePrefab;
	private int amount;

	void Start()
	{
		amount = 5;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Vector3 pos = new Vector3(transform.position.x, transform.position.y, 2.9f);
			GameObject projectile = Instantiate(ProjectilePrefab, pos, Quaternion.identity) as GameObject;
			projectile.GetComponent<Projectile>().SetTarget(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5)));

			amount--;

			if(amount == 0)
			{
				Destroy (this);
			}
		}
	}
}
