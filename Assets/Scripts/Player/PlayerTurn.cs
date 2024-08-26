using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _turnSpeed;

    // Update is called once per frame
    private void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        // Turn player
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _playerInput.MouseX);
    }
}
