using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySettings : MonoBehaviour
{
    SliderSync musicVolume_slider, effectVolume_slider;
    Dropdown controlSelector;

    private float volume_K = 0.01f;

    void Awake()
    {
        GetSliders();
    }

    private void Start()
    {
        SendOldDataToUI();
    }

    public void GetSliders()
    {
        musicVolume_slider = transform.Find("MusicVolume").GetComponent<SliderSync>();
        effectVolume_slider = transform.Find("EffectVolume").GetComponent<SliderSync>();
        controlSelector = transform.Find("Controls").GetComponent<Dropdown>();
    }

    public void ControlChanged()
    {
        switch (controlSelector.value)
        {
            case 0: Parameters.controlMap = ControlMap.FrSkyTaranisX7; break;
            case 1: Parameters.controlMap = ControlMap.FrSkyTaranis; break;
            case 2: Parameters.controlMap = ControlMap.Xbox; break;
            default: Parameters.controlMap = ControlMap.FrSkyTaranisX7; break;
        }
    }

    void Update()
    {
        Parameters.musicVolume = musicVolume_slider.GetValue() * volume_K;
        Parameters.effectVolume = effectVolume_slider.GetValue() * volume_K;
    }
    private void SendOldDataToUI()
    {
        musicVolume_slider.SetValue(Parameters.musicVolume / volume_K);
        effectVolume_slider.SetValue(Parameters.effectVolume / volume_K);
    }
    public void Apply()
    {
        DataManager.SaveAll();
    }
}
