using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private RawImage _crossHair;
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private Image _maxHP;

    [SerializeField] private Gun _gun;
    [SerializeField] private HealthPoints _healthPoints;

    private int _maxAmmo = 0;

    private void OnEnable()
    {
        _gun.actualAmmo += HandleChangeAmmo;
        _gun.maxAmmo += HandleMaxAmmo;
    }

    private void OnDisable()
    {
        _gun.actualAmmo -= HandleChangeAmmo;
        _gun.maxAmmo -= HandleMaxAmmo;
    }

    private void Awake()
    {
        if (!_gun)
        {
            Debug.LogError($"{name}: Gun is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!_healthPoints)
        {
            Debug.LogError($"{name}: HealthPoitns is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (_crossHair == null)
        {
            Debug.LogError($"{name}: CrossHair is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!_ammoText)
        {
            Debug.LogError($"{name}: AmmoText is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void LookEnemy(bool look)
    {

    }

    private void HandleChangeLife(float life)
    {

    }

    private void HandleChangeAmmo(int actualAmmo)
    {
        _ammoText.text = $"{actualAmmo} / {_maxAmmo}";
    }

    private void HandleMaxAmmo(int maxAmmo)
    {
        _maxAmmo = maxAmmo;
    }
}