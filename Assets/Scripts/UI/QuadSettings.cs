using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuadSettings : MonoBehaviour
{
    //public GameObject QuadCharacteristicsObj;

    public GameObject PID_Settings;

    //public GameObject Cam_Settings;

    //public GameObject Battery_Settings;

    public MenuQuad menuQuad;

    SliderSync pitch_P_slider, pitch_I_slider, pitch_D_slider,
                yaw_P_slider, yaw_I_slider, yaw_D_slider,
                roll_P_slider, roll_I_slider, roll_D_slider;
                

    private void Awake()
    {
        GetSlyders();
    }
    private void Start()
    {
        SendOldDataToUI();
    }
    private void Update()
    {
 
    }

    private void GetSlyders()
    {
        pitch_P_slider = PID_Settings.transform.Find("Pitch").Find("P").GetComponent<SliderSync>();
        pitch_I_slider = PID_Settings.transform.Find("Pitch").Find("I").GetComponent<SliderSync>();
        pitch_D_slider = PID_Settings.transform.Find("Pitch").Find("D").GetComponent<SliderSync>();

        yaw_P_slider = PID_Settings.transform.Find("Yaw").Find("P").GetComponent<SliderSync>();
        yaw_I_slider = PID_Settings.transform.Find("Yaw").Find("I").GetComponent<SliderSync>();
        yaw_D_slider = PID_Settings.transform.Find("Yaw").Find("D").GetComponent<SliderSync>();

        roll_P_slider = PID_Settings.transform.Find("Roll").Find("P").GetComponent<SliderSync>();
        roll_I_slider = PID_Settings.transform.Find("Roll").Find("I").GetComponent<SliderSync>();
        roll_D_slider = PID_Settings.transform.Find("Roll").Find("D").GetComponent<SliderSync>();

       /* cam_angle_slider = Cam_Settings.transform.Find("Angle").GetComponent<SliderSync>();
        cam_fov_slider = Cam_Settings.transform.Find("FOV").GetComponent<SliderSync>();
        cam_mass_slider = Cam_Settings.transform.Find("Mass").GetComponent<SliderSync>();
        cam_fp_ckeck = Cam_Settings.transform.Find("FP").GetComponent<Toggle>();

        battery_mass_slider = Battery_Settings.transform.Find("Mass").GetComponent<SliderSync>();
        battery_max_charge_slider = Battery_Settings.transform.Find("MaxCharge").GetComponent<SliderSync>();

        quad_mass = QuadCharacteristicsObj.transform.Find("Mass").GetComponent<SliderSync>();*/
    }

    public void ApplyButton()
    {
        ApplyNewData();
        DataManager.SaveAll();
        menuQuad.Respawn();
    }

    private void SendOldDataToUI()
    {
        pitch_P_slider.SetValue(PID_Properties.pitch_P);
        pitch_I_slider.SetValue(PID_Properties.pitch_I);
        pitch_D_slider.SetValue(PID_Properties.pitch_D);

        yaw_P_slider.SetValue(PID_Properties.yaw_P);
        yaw_I_slider.SetValue(PID_Properties.yaw_I);
        yaw_D_slider.SetValue(PID_Properties.yaw_D);

        roll_P_slider.SetValue(PID_Properties.roll_P);
        roll_I_slider.SetValue(PID_Properties.roll_I);
        roll_D_slider.SetValue(PID_Properties.roll_D);
        /*
        quad_mass.SetValue(QuadCharacteristics.mass);

        cam_angle_slider.SetValue(CameraProperties.firstPersonCameraAngle);
        cam_fov_slider.SetValue(CameraProperties.FOV);
        cam_mass_slider.SetValue(CameraProperties.mass);
        cam_fp_ckeck.isOn = CameraProperties.isFirstPersonCamOn;

        battery_mass_slider.SetValue(Baterry.mass);
        battery_max_charge_slider.SetValue(Baterry.maxCharge);*/
    }

    private void ApplyNewData()
    {
        PID_Properties.pitch_P = pitch_P_slider.GetValue();
        PID_Properties.pitch_I = pitch_I_slider.GetValue();
        PID_Properties.pitch_D = pitch_D_slider.GetValue();

        PID_Properties.yaw_P = yaw_P_slider.GetValue();
        PID_Properties.yaw_I = yaw_I_slider.GetValue();
        PID_Properties.yaw_D = yaw_D_slider.GetValue();

        PID_Properties.roll_P = roll_P_slider.GetValue();
        PID_Properties.roll_I = roll_I_slider.GetValue();
        PID_Properties.roll_D = roll_D_slider.GetValue();
        /*
        QuadCharacteristics.mass = quad_mass.GetValue();
        CameraProperties.firstPersonCameraAngle = cam_angle_slider.GetValue();
        CameraProperties.FOV = cam_fov_slider.GetValue();
        CameraProperties.mass = cam_mass_slider.GetValue();
        CameraProperties.isFirstPersonCamOn = cam_fp_ckeck.isOn;

        Baterry.mass = battery_mass_slider.GetValue();
        Baterry.maxCharge = battery_max_charge_slider.GetValue();*/
    }
}
