using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    public UnityEvent OnPush;

    [SerializeField] private Animator _platformAnimator;
    [SerializeField] private Material _hoverColor;

    private Material _defaultColor;
    private MeshRenderer _renderer;

    public void OnHoverEnter()
    {
        _renderer.material = _hoverColor;
    }

    public void OnHoverExit()
    {
        _renderer.material = _defaultColor;
    }

    public void OnSelect()
    {
        // Turn on animation
        OnPush?.Invoke();
    }

    public void ActivateLift()
    {
        _platformAnimator.SetBool("isActive", !_platformAnimator.GetBool("isActive"));
    }

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _defaultColor = _renderer.material;
    }
}
