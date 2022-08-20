using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseMapUI : MonoBehaviour
{
    public List<Scene> scenes;
    public List<Button> buttons;

    void Start()
    {

    }

    void Update()
    {

    }

    void CreateButton()
    {

    }

    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void PressTestMapButton()
    {
        SceneManager.LoadScene(Scene.TestMap.ToString());
    }
}
