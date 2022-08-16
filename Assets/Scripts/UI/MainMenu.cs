using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MainMenu : MonoBehaviourPunCallbacks
{
    public GameObject ChooseMapUI;
    public GameObject ConnectionUI;
    public GameObject QuadSettingsUI;
    public GameObject GameplaySettingsUI;

    public GameObject Quad;
    private GameObject currPanel;

    void Start()
    {
        currPanel = null;
        ShowPanel(null);
    }

    void Update()
    {

    }
    private void Awake()
    {
        GameplaySettingsUI.SetActive(false);
        ChooseMapUI.SetActive(false);
        QuadSettingsUI.SetActive(false);
        ConnectionUI.SetActive(false);
        //Quad.SetActive(false);
    }
    public void PressTesting() { ShowPanel(ChooseMapUI); }

    public void PressRace() { 
        ShowPanel(ConnectionUI);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //Debug.Log("OnConnectedToMaster");
        SceneManager.LoadScene("SearchRaceScene");
        //PhotonNetwork.JoinRandomRoom();
    }

    public void PressQuadSettings() 
    { 
        ShowPanel(QuadSettingsUI); 
        //Quad.SetActive(true); 
        //Quad.GetComponent<Quad>().Respawn(); 
    }

    public void PressGameplaySettings() { ShowPanel(GameplaySettingsUI); }

    public void PressQuit()
    {
        Application.Quit();
    }

    public void PressApply() { ShowPanel(null); }

    void ShowPanel(GameObject panel)
    {
        if (currPanel) currPanel.SetActive(false);
        //Quad.SetActive(false);
        currPanel = panel;
        if (currPanel) currPanel.SetActive(true);
    }
}
