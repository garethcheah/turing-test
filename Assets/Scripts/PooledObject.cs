using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnReset;

    private ObjectPool _associatedPool;
    private float _timer;
    private float _destroyTime;
    private bool _setToDestroy;

    public void SetObjectPool(ObjectPool objectPool)
    {
        _associatedPool = objectPool;
        _timer = 0;
        _destroyTime = 0;
        _setToDestroy = false;
    }

    public void ResetObject()
    {
        OnReset?.Invoke();
    }

    public void Destroy()
    {
        if (_associatedPool != null)
        {
            _associatedPool.RestoreObject(this);
        }
    }

    public void Destroy(float time)
    {
        _setToDestroy = true;
        _destroyTime = time;
    }

    private void Update()
    {
        if (_setToDestroy)
        {
            _timer += Time.deltaTime;

            if (_timer >= _destroyTime)
            {
                _setToDestroy = false;
                _timer = 0;
                Destroy();
            }
        }
    }
}
