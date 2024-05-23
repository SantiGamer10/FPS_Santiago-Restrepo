using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Xml.Linq;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints;

    private int currentHealth;

    public Action<float> damagedEvent;
    public Action dead;

    private void Awake()
    {
        if (maxHealthPoints <= 0)
        {
            Debug.LogError($"{name}: Health points cannot be 0 or less.\nPlease check and assign another number.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        currentHealth = maxHealthPoints;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{name} was damaged, health: {currentHealth}");
        if (currentHealth <= 0)
        {
            Dead();
            return;
        }
        damagedEvent?.Invoke((float)currentHealth / (float)maxHealthPoints);

    }

    [ContextMenu("Take 1 point of p_damage")]
    private void BasicDamage()
    {
        currentHealth--;
        damagedEvent?.Invoke(currentHealth / maxHealthPoints);
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    [ContextMenu("Take total p_damage")]
    private void TakeTotalDamage()
    {
        TakeDamage(currentHealth);
    }

    private void Dead()
    {
        dead?.Invoke();
        currentHealth = maxHealthPoints;
    }
}

public interface IHealth
{
    public void TakeDamage(int damage){}

    [ContextMenu("Take 1 point of p_damage")]
    private void BasicDamage(){}

    [ContextMenu("Take total p_damage")]
    private void TakeTotalDamage(){}

    private void Dead(){}
}