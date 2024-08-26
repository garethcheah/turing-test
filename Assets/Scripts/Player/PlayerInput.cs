using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Execute this before all other scripts
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    private bool _clear;

    public float Horizontal { get; private set; }

    public float Vertical { get; private set; }

    public float MouseX { get; private set; }

    public float MouseY { get; private set; }

    public bool SprintHeld {  get; private set; }

    public bool JumpPressed { get; private set; }

    public bool InteractPressed { get; private set; }

    public bool PrimaryShootPressed { get; private set; }

    public bool SecondaryShootPressed { get; private set; }

    // Update is called once per frame
    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        _clear = true;
        ClearInputs();
    }

    private void ProcessInputs()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        SprintHeld = SprintHeld || Input.GetButton("Fire3");
        JumpPressed = JumpPressed || Input.GetButtonDown("Jump");
        InteractPressed = InteractPressed || Input.GetKeyDown(KeyCode.F);
        PrimaryShootPressed = PrimaryShootPressed || Input.GetButtonDown("Fire1");
        SecondaryShootPressed = SecondaryShootPressed || Input.GetButtonDown("Fire2");
    }

    private void ClearInputs()
    {
        if (!_clear)
            return;

        Horizontal = 0;
        Vertical = 0;
        MouseX = 0;
        MouseY = 0;

        SprintHeld = false;
        JumpPressed = false;
        InteractPressed = false;
        PrimaryShootPressed = false;
        SecondaryShootPressed = false;
    }
}
