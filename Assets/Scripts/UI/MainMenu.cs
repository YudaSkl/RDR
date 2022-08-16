using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject ChooseMapPanel;
    private GameObject currPanel;

    void Start()
    {
        currPanel = null;
        ShowPanel(null);
    }

    void Update()
    {

    }

    public void PressTestStart() { ShowPanel(ChooseMapPanel); }

    public void PressQuadSetUp() {
        SceneManager.LoadScene("CallibrationScene");
    }

    public void PressSettings() { ShowPanel(SettingsPanel); }

    public void PressQuit()
    {
        Application.Quit();
    }

    public void PressApply() { ShowPanel(null); }

    void ShowPanel(GameObject panel)
    {
        SettingsPanel.SetActive(false);
        ChooseMapPanel.SetActive(false);
        currPanel = panel;
        if (currPanel) currPanel.SetActive(true);
    }
}
