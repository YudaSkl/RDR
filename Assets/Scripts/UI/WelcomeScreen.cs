using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WelcomeScreen : MonoBehaviour
{
    void Start()
    {
        DataManager.LoadAll();
        //DataManager.TestLoad();
        Parameters.controlMap = ControlMap.Keyboard;
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
        SceneManager.LoadScene(Scene.MainMenuScene.ToString());
    }
}
