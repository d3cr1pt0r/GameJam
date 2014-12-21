using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour
{
	public float speed = 0.02f;

	private bool flipped;

	void Start ()
	{
		flipped = false;
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.A))
		{
			moveLeft (speed);
		}
		if (Input.GetKey (KeyCode.D))
		{
			moveRight(speed);
		}
	}

	void moveLeft(float speed)
	{
		transform.position += new Vector3(-speed, 0, 0);
		if (flipped)
		{
			flipped = false;
			Vector3 curScale = transform.localScale;
			curScale.x *= -1.0f;
			transform.localScale = curScale;
		}
	}

	void moveRight(float speed)
	{
		transform.position += new Vector3(speed, 0, 0);
		if (!flipped)
		{
			flipped = true;
			Vector3 curScale = transform.localScale;
			curScale.x *= -1.0f;
			transform.localScale = curScale;
		}
	}
}
