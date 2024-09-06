using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{
    private ShootInteractor _shootInteractor;
    private Transform _shootPoint;
    private Transform _cameraTransform;

    public BulletShootStrategy(ShootInteractor shootInteractor, Transform shootPoint, Transform cameraTransform)
    {
        _shootInteractor = shootInteractor;
        _shootPoint = shootPoint;
        _cameraTransform = cameraTransform;

        // Change Gun Color
        _shootInteractor.gunRenderer.material.color = _shootInteractor.bulletGunColor;
    }

    public void Shoot()
    {
        PooledObject bulletFromPool = _shootInteractor.bulletPool.GetPooledObject();

        bulletFromPool.gameObject.SetActive(true);

        Rigidbody bullet = bulletFromPool.GetComponent<Rigidbody>();

        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;
        bullet.velocity = _cameraTransform.forward * _shootInteractor.GetShootVelocity();

        _shootInteractor.bulletPool.DestroyPooledObject(bulletFromPool, 5.0f);
    }
}
