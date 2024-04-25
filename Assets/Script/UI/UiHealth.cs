using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealth : MonoBehaviour
{
    [SerializeField] private Image _maxHP;
    [SerializeField] private HealthPoints _healthPoints;

    private void OnEnable()
    {
        _healthPoints.damagedEvent += HandleChangeHealth;
    }

    private void OnDisable()
    {
        _healthPoints.damagedEvent -= HandleChangeHealth;
    }

    private void Awake()
    {
        if (_healthPoints == null)
        {
            Debug.LogError($"{name}: HealthPoints is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (_maxHP == null)
        {
            Debug.LogError($"{name}: MaxHP is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void HandleChangeHealth(float health)
    {
        _maxHP.fillAmount = health;
    }
}
