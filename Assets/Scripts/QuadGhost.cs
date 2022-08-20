using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadGhost : MonoBehaviour
{
    public Quad quadBody;
    InputManager inputManager;
    public float maxAngle;
    public float yawSpeed;
    float yRot = 0;
    private void Start()
    {
        inputManager = quadBody.GetComponent<InputManager>();
        transform.rotation = transform.parent.rotation;
    }
    public void SetUp(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
    void AngleGhost()
    {
        float xRot = inputManager.inputValues.pitch * maxAngle;
        float zRot = inputManager.inputValues.roll * maxAngle;
        yRot += inputManager.inputValues.yaw * yawSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(xRot, yRot, -zRot);
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    private void FixedUpdate()
    {
        transform.position = quadBody.transform.position;
        AngleGhost();
    }

    private void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, collider.size);
    }
}
