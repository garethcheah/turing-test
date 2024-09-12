using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _health;

    public Action<float> OnHealthUpdate;
    public Action OnDeath;

    public bool IsDead {  get; private set; }

    public void DeductHealth(float value)
    {
        if (IsDead) return;

        _health -= value;

        if (_health <= 0)
        {
            IsDead = true;
            OnDeath();
            _health = 0;
        }

        if (OnHealthUpdate != null)
            OnHealthUpdate(_health);
    }

    private void Start()
    {
        _health = _maxHealth;

        if (OnHealthUpdate != null)
            OnHealthUpdate(_health);
    }
}
