using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraMovement : MonoBehaviour
{
    [Header("Player Turn")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private bool _invertMouse;

    private float _cameraRotationX;

    // Start is called before the first frame update
    private void Start()
    {
        //Hides mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Camera up and down
        _cameraRotationX += Time.deltaTime * PlayerInput.instance.MouseY * _turnSpeed * (_invertMouse ? 1 : -1);
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, -85.0f, 85.0f);

        transform.localRotation = Quaternion.Euler(_cameraRotationX, 0, 0);
    }
}
