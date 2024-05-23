using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected HealthPoints healthPoints;
    [SerializeField] protected float waitForDie;

    public event Action<bool> onDie = delegate { };


    protected virtual void OnEnable()
    {
        healthPoints.dead += HandleDie;
    }

    protected virtual void OnDisable()
    {
        healthPoints.dead -= HandleDie;
    }

    protected virtual void Awake()
    {
        if (!healthPoints)
        {
            Debug.LogError($"{name}: HealthPoints is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    protected virtual void HandleDie()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        onDie?.Invoke(true);
        yield return new WaitForSeconds(waitForDie);
        gameObject.SetActive(false);
        onDie?.Invoke(false);
    }
}