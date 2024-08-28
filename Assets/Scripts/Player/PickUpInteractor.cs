using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpInteractor : Interactor
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _pickUpMask;
    [SerializeField] private float _pickUpDistance;
    [SerializeField] private Transform _attachPoint;

    private RaycastHit _rayCastHit;
    private IPickable _pickable;
    private bool _isPicked;

    public override void Interact()
    {
        Ray ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out _rayCastHit, _pickUpDistance, _pickUpMask))
        {
            // Temporarily hold last ray cast
            _pickable = _rayCastHit.transform.GetComponent<IPickable>();

            if (PlayerInput.instance.InteractPressed && !_isPicked)
            {
                _pickable.OnPicked(_attachPoint);
                _isPicked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && _isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked = false;
        }
    }
}
