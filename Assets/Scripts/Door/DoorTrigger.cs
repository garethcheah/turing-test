using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorLockIndicator _doorLockIndicator;

    private Animator _doorAnimator;

    // Start is called before the first frame update
    private void Start()
    {
        _doorAnimator = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_doorLockIndicator.IsDoorLocked())
        {
            _doorAnimator.SetBool("isOpening", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_doorLockIndicator.IsDoorLocked())
        {
            _doorAnimator.SetBool("isOpening", false);
        }
    }
}
