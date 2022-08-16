using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class JoystickSetup : MonoBehaviour
{
    [SerializeField]
    Input_Values conrolValues;
    public GameObject leftStick;
    public GameObject rightStick;
    Vector3 leftStickZeroPos;
    Vector3 rightStickZeroPos;
    public int moveDistance;
    PlayerInput playerInput;

    void Start()
    {
        leftStickZeroPos = leftStick.transform.position;
        rightStickZeroPos = rightStick.transform.position;

        playerInput = GetComponent<PlayerInput>();
        string str = "";
        IReadOnlyList<InputDevice> devices = playerInput.devices;
        foreach (InputDevice d in devices)
        {
            str += d.displayName + "   ";
        }
        print(str);
    }
    
    void Update()
    {
        float leftXPos = leftStickZeroPos.x + conrolValues.yaw * moveDistance;
        float leftYPos = leftStickZeroPos.y + conrolValues.throttle * moveDistance;
        float rightXPos = rightStickZeroPos.x + conrolValues.roll * moveDistance;
        float rightYPos = rightStickZeroPos.y + conrolValues.pitch * moveDistance;
        leftStick.transform.position = new Vector3(leftXPos, leftYPos, 0);
        rightStick.transform.position = new Vector3(rightXPos, rightYPos, 0);
    }
}
