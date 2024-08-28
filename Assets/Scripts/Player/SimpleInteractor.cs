using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractor : Interactor
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _interationMask;
    [SerializeField] private float _interactionDistance;

    private RaycastHit _rayCastHit;
    private ISelectable _selectable;

    public override void Interact()
    {
        Ray ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out _rayCastHit, _interactionDistance, _interationMask))
        {
            // Temporarily hold last ray cast
            _selectable = _rayCastHit.transform.GetComponent<ISelectable>();

            if (_selectable != null)
            {
                _selectable.OnHoverEnter();

                if (PlayerInput.instance.InteractPressed)
                {
                    _selectable.OnSelect();
                }
            }
        }

        if (_rayCastHit.transform == null && _selectable != null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }
}
