using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text textHealth;
    public GameObject textGameOver;

    [SerializeField] private PlayerHealth _playerHealth;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnEnable()
    {
        // Add subscriptions
        _playerHealth.OnHealthUpdate += OnHealthUpdate;
        _playerHealth.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        // Remove subscriptions
        _playerHealth.OnHealthUpdate -= OnHealthUpdate;
    }

    private void OnHealthUpdate(float value)
    {
        textHealth.text = $"Health: {Mathf.Floor(value).ToString()}";
    }

    private void OnDeath()
    {
        textGameOver.SetActive(true);
    }
}
