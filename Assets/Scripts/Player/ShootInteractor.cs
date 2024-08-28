using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private InputType _inputType;
    [SerializeField] private ObjectPool _bulletPool;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _shootForce;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _finalShootVelocity;

    public enum InputType
    {
        Primary,
        Secondary
    }

    public override void Interact()
    {
        if (_inputType == InputType.Primary && PlayerInput.instance.PrimaryShootPressed ||
            _inputType == InputType.Secondary && PlayerInput.instance.SecondaryShootPressed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _finalShootVelocity = _playerMovement.GetForwardSpeed() + _shootForce;

        PooledObject bulletFromPool = _bulletPool.GetPooledObject();

        bulletFromPool.gameObject.SetActive(true);
        
        Rigidbody bullet = bulletFromPool.GetComponent<Rigidbody>();

        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.transform.rotation = _bulletSpawnPoint.rotation;
        bullet.velocity = _cameraTransform.forward * _finalShootVelocity;

        _bulletPool.DestroyPooledObject(bulletFromPool, 5.0f);
    }
}
