using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PIDManager))]

public class Quad : MonoBehaviour
{
    #region Fields
    [SerializeField]
    public Cameras cam;

    [SerializeField]
    Propellers propellers;

    Rigidbody body;
    InputManager inputManager;
    UIManager uiManager;
    PhotonView photonView;

    [HideInInspector]
    public PIDManager pidManager;

    public QuadGhost ghost;
    private AudioSource audioSourceQuad;
    private float maxEffectVolume;

    public FlyMode flyMode;

    public float throttle_K;
    #endregion

    private void Awake()
    {
        DataManager.TestLoad();
        photonView = GetComponentInParent<PhotonView>();

        inputManager = GetComponent<InputManager>();
        uiManager = GetComponent<UIManager>();
        pidManager = GetComponent<PIDManager>();
        audioSourceQuad = GetComponent<AudioSource>();
        //body = transform.Find("Body").GetComponent<Rigidbody>();
        body = GetComponent<Rigidbody>();
        if (!photonView.IsMine)
        {
            inputManager.enabled = false;
            if (uiManager != null) { uiManager.enabled = false; }
            pidManager.enabled = false;
            audioSourceQuad.enabled = false;
            ghost.gameObject.SetActive(false);
            cam.firstPersonCamera.gameObject.SetActive(false);
            cam.thirdPersonCamera.gameObject.SetActive(false);
            this.enabled = false;
            return;
        }

        SetUp();
    }

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            //Start();
        }
        else if (collision.collider.tag == "Wall")
        {
            //Start();
        }
        else if (collision.collider.tag == "CheckPoint")
        {
            //Start()
        }
    }
    #region Setup
    void RigitbodySetup()
    {
        body.mass = QuadCharacteristics.mass;
    }
    void CameraSetup()
    {
        cam.firstPersonCamera.transform.eulerAngles = new Vector3(-CameraProperties.firstPersonCameraAngle, 0, 0);
        cam.firstPersonCamera.fieldOfView = CameraProperties.FOV;
        cam.thirdPersonCamera.fieldOfView = CameraProperties.FOV;
        ChangeCam(CameraProperties.isFirstPersonCamOn);
    }
    void GhostSetup()
    {
        ghost.SetUp(body.rotation);
    }
    void AudioSetup()
    {
        maxEffectVolume = Parameters.effectVolume;
    }
    #endregion

    public void SetUp()
    {
        RigitbodySetup();
        GhostSetup();
        CameraSetup();
        AudioSetup();
    }
    void ChangeCam(bool isFirst)
    {
        if (isFirst)
        {
            cam.thirdPersonCamera.enabled = false;
            cam.thirdPersonCamera.GetComponent<AudioListener>().enabled = false;

            cam.firstPersonCamera.enabled = true;
            cam.firstPersonCamera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            cam.firstPersonCamera.enabled = false;
            cam.firstPersonCamera.GetComponent<AudioListener>().enabled = false;

            cam.thirdPersonCamera.enabled = true;
            cam.thirdPersonCamera.GetComponent<AudioListener>().enabled = true;
        }
    }

    public void Respawn()
    {
        SetUp();
    }

    void FixedUpdate()
    {
        SetAudio();
        AddThrottlePower();
        AddStabilizePower(ghost.GetRotation());
        if (uiManager!=null && uiManager.isActiveAndEnabled)
        {
            ShowInfo();
        }
        
        //Debug.Log("Pitch_PID: (" + PID_Properties.pitch_P + ", " + PID_Properties.pitch_I + ", " + PID_Properties.pitch_D + ")");
        //Debug.Log("Control System: " + Parameters.controlMap.ToString());
    }

    void SetAudio()
    {
        audioSourceQuad.volume = (maxEffectVolume / 100) * (inputManager.convertedValues.throttle);
    }

    void AddThrottlePower()
    {
        propellers.propellerBL.SetPower(inputManager.convertedValues.throttle * throttle_K / 4);
        propellers.propellerBR.SetPower(inputManager.convertedValues.throttle * throttle_K / 4);
        propellers.propellerFL.SetPower(inputManager.convertedValues.throttle * throttle_K / 4);
        propellers.propellerFR.SetPower(inputManager.convertedValues.throttle * throttle_K / 4);
        //Debug.Log("Input: " + inputManager.inputValues.throttle + " Converted: " + inputManager.convertedValues.throttle + " Result: " + inputManager.convertedValues.throttle * throttle_K / 4);
    }

    private float GetPitchError(Quaternion targetRotation)
    {
        float targetX = WrapAngle(NormalizeAngle(targetRotation.eulerAngles.x));
        float currX = WrapAngle(NormalizeAngle(body.transform.rotation.eulerAngles.x));
        float errorAngle = targetX - currX;
        errorAngle = WrapAngle(NormalizeAngle(errorAngle));
        //Debug.Log("target X: " + targetX + " current X: " + currX + " error X: " + errorAngle);
        return errorAngle;
    }
    private float GetRollError(Quaternion targetRotation)
    {
        float targetZ = WrapAngle(NormalizeAngle(targetRotation.eulerAngles.z));
        float currZ = WrapAngle(NormalizeAngle(body.transform.rotation.eulerAngles.z));
        float errorAngle = targetZ - currZ;
        errorAngle = WrapAngle(NormalizeAngle(errorAngle));
        //Debug.Log("target Z: " + targetZ + " current Z: " + currZ + " error Z: " + errorAngle);

        return errorAngle;
    }
    private float GetYawError(Quaternion targetRotation)
    {
        float targetY = WrapAngle(NormalizeAngle(targetRotation.eulerAngles.y));
        float currY = WrapAngle(NormalizeAngle(body.transform.rotation.eulerAngles.y));
        float errorAngle = targetY - currY;
        errorAngle = WrapAngle(NormalizeAngle(errorAngle));
        //Debug.Log("target Y: " + targetY + " current Y: " + currY + " error Y: " + errorAngle);

        return errorAngle;
    }
    float NormalizeAngle(float inputAngle)
    {
        return ((inputAngle % 360f) + 360f) % 360f;
    }
    float WrapAngle(float inputAngle)
    {
        if (inputAngle > 180f && inputAngle < 360f)
        {
            inputAngle = 360f - inputAngle;
        }
        else
        {
            inputAngle *= -1f;
        }

        return inputAngle;
    }

    public void AddStabilizePower(Quaternion targetRotation)
    {
        //print("Ghost Rotation: (X: " + targetRotation.eulerAngles.x + " Y: " + targetRotation.eulerAngles.y + " Z: " + targetRotation.eulerAngles.z + ")");
        float pitchAngleError = GetPitchError(targetRotation);
        pidManager.pidCorrection.pitch = pidManager.pitchPID.Calculate(pitchAngleError);
        PitchCorrection(-pidManager.pidCorrection.pitch); //���� ����� �� ���� ������ ����� ������������� �������� ����

        float rollAngleError = GetRollError(targetRotation);
        pidManager.pidCorrection.roll = pidManager.rollPID.Calculate(rollAngleError);
        RollCorrection(pidManager.pidCorrection.roll);  //��� ������ �� ��� ������ ����� ������������� �������� ����

        float yawAngleError = GetYawError(targetRotation);
        pidManager.pidCorrection.yaw = pidManager.yawPID.Calculate(yawAngleError);
        YawCorrection(pidManager.pidCorrection.yaw);
    }

    void PitchCorrection(float value)
    {
        //print("PitchCorrection value: " + value);
        propellers.propellerBL.AddPower(-value / 2);
        propellers.propellerBR.AddPower(-value / 2);
        propellers.propellerFL.AddPower(value / 2);
        propellers.propellerFR.AddPower(value / 2);
    }

    void RollCorrection(float value)
    {
        propellers.propellerBL.AddPower(-value / 2);
        propellers.propellerBR.AddPower(value / 2);
        propellers.propellerFL.AddPower(-value / 2);
        propellers.propellerFR.AddPower(value / 2);
    }
    void YawCorrection(float value)
    {
        body.AddRelativeTorque(new Vector3(0, value, 0));
    }
    void ShowInfo()
    {
        int speed = Mathf.RoundToInt(Mathf.Max(body.velocity.x, -body.velocity.y, body.velocity.z));
        if (speed < 0)
            speed = 0;
        uiManager.SetSpeed(speed);
        uiManager.SetFlyMode(flyMode);
    }
}
