using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    [SerializeField] protected PlayerInput input;

    public abstract void Interact();

    private void Update()
    {
        Interact();
    }
}
