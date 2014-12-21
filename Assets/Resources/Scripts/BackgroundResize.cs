using UnityEngine;
using System.Collections;

public class BackgroundResize : MonoBehaviour
{
	
	void Start ()
	{
	
	}

	void Update ()
	{
		Vector3 leftPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 5));
		Vector3 rightPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 5));

		float screenWidthWorld = rightPos.x - leftPos.x;
		transform.localScale = new Vector3 (screenWidthWorld, screenWidthWorld, 1);

		//transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 6);
	}
}
