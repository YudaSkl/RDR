using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject quad;
    public List<GameObject> spawnPoints;
    public GameObject soloModeUI;
    Controlls control;

    void Awake()
    {
        //control = new Controlls();
        //control.FrSky.Escape.performed += ctx => ChangeUI();
    }

    void Start()
    {
        Transform newQuad = Instantiate(quad.transform, spawnPoints[0].transform.position, Quaternion.identity);
        newQuad.name = "YudaSkl";
        newQuad.GetComponent<Quad>().spawnPoint = spawnPoints[0];
        newQuad.GetComponent<Quad>().SetUp();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeUI();
        }
    }

    void ChangeUI()
    {
        soloModeUI.GetComponent<UIManager>().ChangeUI();
    }
}
