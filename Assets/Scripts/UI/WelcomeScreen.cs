using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WelcomeScreen : MonoBehaviour
{
    Controlls control;

    void Start()
    {
        DataManager.TestLoad();
        Parameters.controlMap = ControlMap.FrSkyTaranisX7;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pressed();
        }
    }

    void Pressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
