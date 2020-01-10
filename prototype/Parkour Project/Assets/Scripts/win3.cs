using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win3 : MonoBehaviour
{


    private Scene scene;

    void Start()
    {
    }



    void OnTriggerEnter(Collider other)
    {
        Application.Quit();
    }
}
