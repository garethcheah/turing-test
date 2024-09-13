using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyCard : MonoBehaviour
{
    public UnityEvent OnKeyCardAcquired;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnKeyCardAcquired?.Invoke();
            Destroy(gameObject);
        }
    }
}
