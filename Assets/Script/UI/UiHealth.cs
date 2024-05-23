using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealth : MonoBehaviour
{
    [SerializeField] private Image maxHP;
    [SerializeField] private HealthPoints healthPoints;

    private void OnEnable()
    {
        healthPoints.damagedEvent += HandleChangeHealth;
    }

    private void OnDisable()
    {
        healthPoints.damagedEvent -= HandleChangeHealth;
    }

    private void Awake()
    {
        if (healthPoints == null)
        {
            Debug.LogError($"{name}: HealthPoints is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (maxHP == null)
        {
            Debug.LogError($"{name}: MaxHP is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void HandleChangeHealth(float health)
    {
        maxHP.fillAmount = health;
    }
}
