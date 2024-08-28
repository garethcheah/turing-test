using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectToPool;
    public int startSize;

    [SerializeField] private List<PooledObject> _objectPool = new List<PooledObject>();
    [SerializeField] private List<PooledObject> _usedPool = new List<PooledObject>();

    private PooledObject _tempObject;

    public PooledObject GetPooledObject()
    {
        PooledObject pooledObject;

        if (_objectPool.Count > 0)
        {
            pooledObject = _objectPool[0];
            _usedPool.Add(pooledObject);
            _objectPool.RemoveAt(0);
        }
        else
        {
            AddNewObject();
            pooledObject = GetPooledObject();
        }

        pooledObject.gameObject.SetActive(true);
        pooledObject.ResetObject();

        return pooledObject;
    }

    public void RestoreObject(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        _usedPool.Remove(pooledObject);
        _objectPool.Add(pooledObject);
    }

    public void DestroyPooledObject(PooledObject pooledObject, float time = 0)
    {
        if (time == 0)
        {
            pooledObject.Destroy();
        }
        else
        {
            pooledObject.Destroy(time);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < startSize; i++)
        {
            AddNewObject();
        }
    }

    private void AddNewObject()
    {
        _tempObject = Instantiate(objectToPool, transform).GetComponent<PooledObject>();
        _tempObject.gameObject.transform.parent = null;
        _tempObject.gameObject.SetActive(false);
        _tempObject.SetObjectPool(this);
        _objectPool.Add(_tempObject);
    }
}
