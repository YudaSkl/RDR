using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Canvas pauseMenu;
    public Canvas playerUI;
    public Text speedValue;
    public Text flyModeValue;
    bool isPaused;

    void Start()
    {
        SetPlayerUI();
    }

    void UpdateData()
    {
        //pauseMenu.transform.Find("Quad Panel").GetComponent<QuadSettings>().LoadData();
    }

    public void SetSpeed(int speed)
    {
        speedValue.text = speed.ToString() + "КМ/Ч";
    }
    public void SetFlyMode(FlyMode fm)
    {
        string str = "";
        switch (fm)
        {
            case FlyMode.Arm: str = "ARM"; flyModeValue.color = Color.red; break;
            case FlyMode.Default: str = "Свободный"; flyModeValue.color = Color.yellow; break;
            case FlyMode.Stab: str = "Стабилизация"; flyModeValue.color = Color.green; break;
            default: break;
        }
        flyModeValue.text = str;
    }
    public void ChangeUI()
    {
        UpdateData();
        if (isPaused)
        {
            SetPlayerUI();
        }
        else
        {
            SetPauseUI();
        }
        isPaused = !isPaused;
    }

    public void SetUI(string UIName)
    {
        switch (UIName)
        {
            case "Pause":
                SetPauseUI();
                break;
            case "Player":
                SetPlayerUI();
                break;
            default:
                SetPlayerUI();
                break;
        }
    }

    private void SetPlayerUI()
    {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
        playerUI.enabled = true;
    }

    private void SetPauseUI()
    {
        Time.timeScale = 0;
        pauseMenu.enabled = true;
        playerUI.enabled = false;
    }

    public void PressRespawn(Quad quadScript)
    {
        ChangeUI();
        quadScript.Respawn();
    }

    public void PressExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
