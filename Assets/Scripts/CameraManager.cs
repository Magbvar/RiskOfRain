using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float distanceToPlayer;
  
    private Vector2 input;

    [SerializeField] private MouseSensitivity mouseSensitivity;
    [SerializeField] private CameraAngle cameraAngle;

    private CameraRotaton cameraRotaton;
    
    private void Awake() =>distanceToPlayer = Vector3.Distance(transform.position, target.position);

    public void Update()
    {
        cameraRotaton.Yaw += input.x * mouseSensitivity.horizontal * Time.deltaTime;
        cameraRotaton.Pitch += input.y * mouseSensitivity.vertical * Time.deltaTime;
        cameraRotaton.Pitch = Mathf.Clamp(cameraRotaton.Pitch, cameraAngle.min, cameraAngle.max);

    }
    public void Look(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        Vector3 offset = Vector3.up * (distanceToPlayer / 4);
        transform.position = target.position - transform.forward * distanceToPlayer + offset;

        transform.eulerAngles = new Vector3(cameraRotaton.Pitch, cameraRotaton.Yaw, 0.0f);

    }
}

[Serializable]
public struct MouseSensitivity
{
    public float horizontal;
    public float vertical;
}

public struct CameraRotaton
{
    //Pitch is the rotation of the camera on the x axis, Yaw is on the y Axis
    public float Pitch;

    public float Yaw;
}
[Serializable]
public struct CameraAngle
{
    public float min;
    public float max;
}