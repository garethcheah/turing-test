using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : Interactor
{
    [SerializeField] private float _jumpVelocity;

    private PlayerMovement _playerMovement;

    public override void Interact()
    {
        if (input.JumpPressed && _playerMovement.IsGrounded)
        {
            _playerMovement.SetYVelocity(_jumpVelocity);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }
}
