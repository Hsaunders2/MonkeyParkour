﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset2 : MonoBehaviour
{


    private Scene scene;

    void Start()
    {
    }



    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(1);
    }
}
