using UnityEngine;

[System.Serializable]
public struct Input_Values
{
    [SerializeField]
    [Range(-1, 1)]
    public float throttle, pitch, roll, yaw;
}
[System.Serializable]
public struct Converted_Values
{
    [SerializeField]
    [Range(0, 100)]
    public float throttle;

    [SerializeField]
    [Range(-180, 180)]
    public float roll, yaw, pitch;

    public float convertion_K;
}
[System.Serializable]
public struct PID_Correction {[Range(-100, 100)] public float pitch, roll, yaw; }


[System.Serializable]
public struct Propellers { public Propeller propellerBR, propellerBL, propellerFR, propellerFL; }
[System.Serializable]
public struct Cameras { public Camera firstPersonCamera, thirdPersonCamera; }

public enum FlyMode{Stab = 1, Default = 0, Arm = -1,}
public enum BladeType { Normal, Bullnose, Hybrid }
public enum BladeMaterial { Plastic, Carbon, Composit }
public enum ControlMap { FrSkyTaranis, FrSkyTaranisX7, Xbox, FSI6SEmulator, Keyboard }

public enum Scene { MainMenuScene , SearchRaceScene, RaceScene, TestMap, WelcomeScene }