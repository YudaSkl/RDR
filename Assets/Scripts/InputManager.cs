using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputManager : MonoBehaviour
{
    Controlls control;
    public ControlMap controlMap;

    [SerializeField]
    public Input_Values inputValues;

    [SerializeField]
    public Converted_Values convertedValues;

    Quad quadScript;

    public void SetControlMap(ControlMap cm)
    {
        controlMap = cm;
        switch (cm)
        {
            case ControlMap.FrSkyTaranis: SetFrSkyTaranis(); break;
            case ControlMap.FrSkyTaranisX7: SetFrSkyTaranisX7(); break;
            case ControlMap.FSI6SEmulator: SetFSI6SEmulator(); break;
            case ControlMap.Xbox: SetXbox(); break;
            case ControlMap.Keyboard: SetKeyboard(); break;
            default: SetKeyboard(); break;
        }
    }
    private void SetKeyboard()
    {
        control.Keyboard.Throttle.performed += ctx => inputValues.throttle = ctx.ReadValue<float>();
        control.Keyboard.Throttle.canceled += ctx => inputValues.throttle = 0;

        control.Keyboard.Yaw.performed += ctx => inputValues.yaw = ctx.ReadValue<float>();
        control.Keyboard.Yaw.canceled += ctx => DropYawStick();

        control.Keyboard.Roll.performed += ctx => inputValues.roll = ctx.ReadValue<float>();
        control.Keyboard.Roll.canceled += ctx => DropRollStick();

        control.Keyboard.Pitch.performed += ctx => inputValues.pitch = ctx.ReadValue<float>();
        control.Keyboard.Pitch.canceled += ctx => DropPitchStick();

        control.Keyboard.StabMode.performed += ctx => FlyModeChange(ctx.ReadValue<int>());
    }
    private void SetFrSkyTaranis()
    {
        control.FrSkyTaranis.Throttle.performed += ctx => inputValues.throttle = ctx.ReadValue<float>();
        control.FrSkyTaranis.Throttle.canceled += ctx => inputValues.throttle = 0;

        control.FrSkyTaranis.Yaw.performed += ctx => inputValues.yaw = ctx.ReadValue<float>();
        control.FrSkyTaranis.Yaw.canceled += ctx => DropYawStick();

        control.FrSkyTaranis.Roll.performed += ctx => inputValues.roll = ctx.ReadValue<float>();
        control.FrSkyTaranis.Roll.canceled += ctx => DropRollStick();

        control.FrSkyTaranis.Pitch.performed += ctx => inputValues.pitch = ctx.ReadValue<float>();
        control.FrSkyTaranis.Pitch.canceled += ctx => DropPitchStick();

        control.FrSkyTaranis.StabMode.performed += ctx => FlyModeChange((FlyMode)ctx.ReadValue<float>());
        control.FrSkyTaranis.StabMode.canceled += ctx => FlyModeChange((FlyMode)0);
    }
    private void SetFrSkyTaranisX7()
    {
        control.FrSkyTaranisX7.Throttle.performed += ctx => inputValues.throttle = ctx.ReadValue<float>();
        control.FrSkyTaranisX7.Throttle.canceled += ctx => inputValues.throttle = 0;

        control.FrSkyTaranisX7.Yaw.performed += ctx => inputValues.yaw = ctx.ReadValue<float>();
        control.FrSkyTaranisX7.Yaw.canceled += ctx => DropYawStick();

        control.FrSkyTaranisX7.Roll.performed += ctx => inputValues.roll = ctx.ReadValue<float>();
        control.FrSkyTaranisX7.Roll.canceled += ctx => DropRollStick();

        control.FrSkyTaranisX7.Pitch.performed += ctx => inputValues.pitch = ctx.ReadValue<float>();
        control.FrSkyTaranisX7.Pitch.canceled += ctx => DropPitchStick();

        control.FrSkyTaranisX7.StabMode.performed += ctx => FlyModeChange((FlyMode)ctx.ReadValue<float>());
        control.FrSkyTaranisX7.StabMode.canceled += ctx => FlyModeChange(0);
    }
    private void SetXbox()
    {
        control.Xbox.Throttle.performed += ctx => inputValues.throttle = ctx.ReadValue<float>();
        control.Xbox.Throttle.canceled += ctx => inputValues.throttle = 0;

        control.Xbox.Yaw.performed += ctx => inputValues.yaw = ctx.ReadValue<float>();
        control.Xbox.Yaw.canceled += ctx => DropYawStick();

        control.Xbox.Roll.performed += ctx => inputValues.roll = ctx.ReadValue<float>();
        control.Xbox.Roll.canceled += ctx => DropRollStick();

        control.Xbox.Pitch.performed += ctx => inputValues.pitch = ctx.ReadValue<float>();
        control.Xbox.Pitch.canceled += ctx => DropPitchStick();

        control.Xbox.StabMode.performed += ctx => FlyModeChange(ctx.ReadValue<int>());
    }
    private void SetFSI6SEmulator()
    {
        control.FSI6SEmulator.Throttle.performed += ctx => inputValues.throttle = ctx.ReadValue<float>();
        control.FSI6SEmulator.Throttle.canceled += ctx => inputValues.throttle = 0;

        control.FSI6SEmulator.Yaw.performed += ctx => inputValues.yaw = ctx.ReadValue<float>();
        control.FSI6SEmulator.Yaw.canceled += ctx => DropYawStick();

        control.FSI6SEmulator.Roll.performed += ctx => inputValues.roll = ctx.ReadValue<float>();
        control.FSI6SEmulator.Roll.canceled += ctx => DropRollStick();

        control.FSI6SEmulator.Pitch.performed += ctx => inputValues.pitch = ctx.ReadValue<float>();
        control.FSI6SEmulator.Pitch.canceled += ctx => DropPitchStick();
    }

    void Awake()
    {
        quadScript = GetComponent<Quad>();
        control = new Controlls();
        SetControlMap(Parameters.controlMap);
    }

    private void OnEnable()
    {
        control.Enable();
    }
    private void OnDisable()
    {
        control.Disable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (quadScript.GetComponent<UIManager>() != null) ChangeUI();
        }
    }

    private void FixedUpdate()
    {
        ControllsConvert();
    }

    void ControllsConvert()
    {
        switch (controlMap)
        {
            case ControlMap.Keyboard:
                convertedValues.throttle = inputValues.throttle * convertedValues.convertion_K;
                convertedValues.roll = inputValues.roll * convertedValues.convertion_K;
                convertedValues.yaw = inputValues.yaw * convertedValues.convertion_K;
                convertedValues.pitch = inputValues.pitch * convertedValues.convertion_K;
                break;
            case ControlMap.FrSkyTaranis:
                convertedValues.throttle = (inputValues.throttle + 1) * convertedValues.convertion_K;
                convertedValues.roll = inputValues.roll * convertedValues.convertion_K;
                convertedValues.yaw = inputValues.yaw * convertedValues.convertion_K;
                convertedValues.pitch = inputValues.pitch * convertedValues.convertion_K; 
                break;
            case ControlMap.FrSkyTaranisX7:
                convertedValues.throttle = (inputValues.throttle + 1) * convertedValues.convertion_K;
                convertedValues.roll = inputValues.roll * convertedValues.convertion_K;
                convertedValues.yaw = inputValues.yaw * convertedValues.convertion_K;
                convertedValues.pitch = inputValues.pitch * convertedValues.convertion_K; 
                break;
            case ControlMap.FSI6SEmulator:
                convertedValues.throttle = (inputValues.throttle + 1) * convertedValues.convertion_K;
                convertedValues.roll = inputValues.roll * convertedValues.convertion_K;
                convertedValues.yaw = inputValues.yaw * convertedValues.convertion_K;
                convertedValues.pitch = inputValues.pitch * convertedValues.convertion_K; 
                break;
            case ControlMap.Xbox:
                if (convertedValues.throttle < 0) { convertedValues.throttle = 0; }
                convertedValues.throttle = inputValues.throttle * convertedValues.convertion_K;
                convertedValues.roll = -inputValues.roll * convertedValues.convertion_K;
                convertedValues.yaw = inputValues.yaw * convertedValues.convertion_K;
                convertedValues.pitch = -inputValues.pitch * convertedValues.convertion_K; 
                break;
            default:
                convertedValues.throttle = inputValues.throttle * convertedValues.convertion_K;
                convertedValues.roll = inputValues.roll * convertedValues.convertion_K;
                convertedValues.yaw = inputValues.yaw * convertedValues.convertion_K;
                convertedValues.pitch = inputValues.pitch * convertedValues.convertion_K;
                break;
        }
        Debug.Log("Input Manager Convert: " + convertedValues.throttle);
    }

    void FlyModeChange(FlyMode flyModeValue)
    {
        switch (flyModeValue)
        {
            case FlyMode.Default: break;
            case FlyMode.Stab: break;
            case FlyMode.Arm: break;
            default:break;
        }
        quadScript.flyMode = flyModeValue;
    }

    void FlyModeChange(int value)
    {
        if (value == 1)
        {
            if (quadScript.flyMode == FlyMode.Default)
                quadScript.flyMode = FlyMode.Stab;
            else
                quadScript.flyMode = FlyMode.Default;
        }
    }

    void DropYawStick()
    {
        inputValues.yaw = 0;
        quadScript.pidManager.yawPID.Drop();
        quadScript.pidManager.pidCorrection.yaw = 0;
    }

    void DropRollStick()
    {
        inputValues.roll = 0;
        quadScript.pidManager.rollPID.Drop();
        quadScript.pidManager.pidCorrection.roll = 0;
    }

    void DropPitchStick()
    {
        inputValues.pitch = 0;
        quadScript.pidManager.pitchPID.Drop();
        quadScript.pidManager.pidCorrection.pitch = 0;
    }

    void ChangeUI()
    {
        quadScript.GetComponent<UIManager>().ChangeUI();
    }
}
