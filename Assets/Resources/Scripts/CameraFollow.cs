﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	private GameObject santa;

	void Start ()
	{
		santa = GameObject.Find ("Santa");
	}

	void Update ()
	{
		Vector3 camPos = Camera.main.transform.position;
		camPos.y = santa.transform.position.y;
		camPos.x = santa.transform.position.x;
		Camera.main.transform.position = camPos;
	}
}
