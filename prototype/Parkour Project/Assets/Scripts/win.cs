﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Application.Quit();
        Debug.Log("gg");
        Destroy(gameObject);
    }
}
