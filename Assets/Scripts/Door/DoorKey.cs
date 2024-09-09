using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKey : MonoBehaviour
{
    [SerializeField] private UnityEvent DoorKeyPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorKeyPicked.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
