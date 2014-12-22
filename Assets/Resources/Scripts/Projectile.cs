using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private Vector3 targetDir;
	private float time;

	// Use this for initialization
	void Start () {
		time = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (targetDir != null)
		{
			Vector3 t = new Vector3(targetDir.x, targetDir.y, 0);
			transform.position += t * 0.1f;

			time -= Time.deltaTime;
			if(time < 0)
			{
				Destroy(gameObject);
			}
		}
	}

	public void SetTarget(Vector3 target)
	{
		targetDir = Vector3.Normalize(target - transform.position);
		gameObject.GetComponent<Light> ().enabled = true;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90.0f - (Mathf.Atan2 (targetDir.x, targetDir.y) * 180.0f / Mathf.PI) + 180.0f));
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("Enemy"))
		{
			other.gameObject.GetComponent<Snowman>().Kill();
		}
	}
}
