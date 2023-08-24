using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public List<MenuPanelComponent> panels;
    public GameObject QuadGameObj;

    string serversUIName = "LobbiesUI";
    void Start()
    {
        ShowPanel(null);
    }

    void Update()
    {

    }
    private void Awake()
    {
        DataManager.LoadAll();
        Parameters.controlMap = ControlMap.Radiomaster_TX12;
        //Parameters.forceMultiplier = 5;
    }

    public void PressQuadSettings(string panelName)
    {
        ShowPanel(panelName);
        QuadGameObj.SetActive(true);
        //Debug.Log(QuadGameObj.activeSelf);
        QuadGameObj.GetComponent<MenuQuad>().Respawn();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        ShowPanel(serversUIName);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Disconected: " + cause.ToString());
        ShowPanel(null);
    }
    public void PressConnect(string panelName)
    {
        ShowPanel(panelName);
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting...");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Already Connected");
            ShowPanel(serversUIName);
        }
    }

    public void PressQuit()
    {
        Application.Quit();
    }

    public void PressControls()
    {
        SceneManager.LoadScene(Scenes.ControlMapperScene.ToString());
    }

    public void ShowPanel(string panelName)
    {
        if(QuadGameObj != null) QuadGameObj.SetActive(false);
        foreach (MenuPanelComponent panel in panels)
        {
            if (panel.name == panelName)
                panel.Show();
            else
                panel.Hide();
        }
    }
}
