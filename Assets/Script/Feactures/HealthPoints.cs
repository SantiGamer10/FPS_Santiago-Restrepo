using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    [Tooltip("Health points must be greater than 0.")]
    [SerializeField] private int _maxHealthPoints;

    private int _currentHP;

    public Action<float> damagedEvent;
    public Action dead;

    private void Awake()
    {
        if (_maxHealthPoints <= 0)
        {
            Debug.LogError($"{name}: Health points cannot be 0 or less.\nCheck and assigned another number.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        _currentHP = _maxHealthPoints;
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;
        Debug.Log($"{name} was damaged, life: {_currentHP}");
        if (_currentHP <= 0)
        {
            Dead();
            return;
        }
        damagedEvent?.Invoke((float)_currentHP / (float)_maxHealthPoints);

    }

    [ContextMenu("Take 1 point of damage")]
    private void BasicDamage()
    {
        _currentHP--;
        damagedEvent?.Invoke(_currentHP / _maxHealthPoints);
        if (_currentHP <= 0)
        {
            Dead();
        }
    }

    [ContextMenu("Take total damage")]
    private void TakeTotalDamage()
    {
        TakeDamage(_currentHP);
    }

    private void Dead()
    {
        dead?.Invoke();
        _currentHP = _maxHealthPoints;
    }
}
