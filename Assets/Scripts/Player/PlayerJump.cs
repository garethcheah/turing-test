using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJump : Interactor
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _jumpVelocity;

    public override void Interact()
    {
        if (PlayerInput.instance.JumpPressed && _playerMovement.IsGrounded)
        {
            _playerMovement.SetYVelocity(_jumpVelocity);
        }
    }
}
