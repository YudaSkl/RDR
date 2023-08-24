using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class WelcomeScreen : MonoBehaviour
{

    [SerializeField] private int playerID = 0;
    [SerializeField] private Player controls;
    void Start()
    {
        DataManager.LoadAll();
        controls = ReInput.players.GetPlayer(playerID);
    }

    void Update()
    {
        if (controls.GetAnyButtonDown())
        {
            SceneManager.LoadScene(Scenes.MainMenuScene.ToString());
        }
    }
}
