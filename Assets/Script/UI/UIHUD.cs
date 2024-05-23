using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private RawImage crossHair;

    [SerializeField] private Color viewEnemyColor;
    [SerializeField] private Color dontViewEnemyColor;

    [SerializeField] private Gun gun;

    [SerializeField] private ActionChannel<int> currentAmmoEvent;
    [SerializeField] private ActionChannel<int> maxAmmoEvent;

    private int _maxAmmo = 0;

    private void OnEnable()
    {
        if (currentAmmoEvent)
            currentAmmoEvent.Subscribe(HandleChangeAmmo);
        if (maxAmmoEvent)
            maxAmmoEvent.Subscribe(HandleMaxAmmo);

        gun.viewEnemy += HandleLookEnemy;
    }

    private void OnDisable()
    {
        if (currentAmmoEvent)
            currentAmmoEvent.Unsubscribe(HandleChangeAmmo);
        if (maxAmmoEvent)
            maxAmmoEvent.Unsubscribe(HandleMaxAmmo);

        gun.viewEnemy += HandleLookEnemy;
    }

    private void Awake()
    {
        if (!gun)
        {
            Debug.LogError($"{name}: Gun is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (crossHair == null)
        {
            Debug.LogError($"{name}: CrossHair is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!ammoText)
        {
            Debug.LogError($"{name}: AmmoText is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void HandleLookEnemy(bool look)
    {
        if (look)
        {
            crossHair.color = viewEnemyColor;
        }
        else
        {
            crossHair.color = dontViewEnemyColor;
        }
    }

    private void HandleChangeAmmo(int actualAmmo)
    {
        ammoText.text = $"{actualAmmo} / {_maxAmmo}";
    }

    private void HandleMaxAmmo(int maxAmmo)
    {
        _maxAmmo = maxAmmo;
    }
}