﻿using UnityEngine;
using System.Collections;

public class ParticleFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerName = "Foreground";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
