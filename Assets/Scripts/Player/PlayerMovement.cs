using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    [Header("Player Movement")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _sprintMultiplier;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckDistance;

    private CharacterController _characterController;
    private Vector3 _playerVelocity;
    private float _speedMultiplier = 1.0f;

    public bool IsGrounded {  get; private set; }

    public void SetYVelocity(float value)
    {
        _playerVelocity.y = value;
    }

    public float GetForwardSpeed()
    {
        return _playerInput.Vertical * _playerSpeed * _speedMultiplier;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        GroundCheck();
        MovePlayer();
    }

    private void MovePlayer()
    {
        _speedMultiplier = _playerInput.SprintHeld ? _sprintMultiplier : 1.0f;

        _characterController.Move((transform.forward * _playerInput.Vertical + transform.right * _playerInput.Horizontal) * _playerSpeed * Time.deltaTime * _speedMultiplier);

        // Ground check
        if (IsGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2.0f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        IsGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }
}
