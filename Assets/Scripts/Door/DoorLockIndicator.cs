using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockIndicator : MonoBehaviour
{
    [SerializeField] private Material _doorUnlockIndicatorColor;
    [SerializeField] private MeshRenderer _doorLockIndicatorRenderer;
    
    private bool _doorLocked = true;

    public bool IsDoorLocked()
    {
        return _doorLocked;
    }

    public void UnlockDoor()
    {
        _doorLockIndicatorRenderer.material = _doorUnlockIndicatorColor;
        _doorLocked = false;
    }
}
