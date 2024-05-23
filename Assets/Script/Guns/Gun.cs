using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : Weapon
{
    [SerializeField] private int shootDistance;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private FirstPersonController controller;
    [SerializeField] private bool isAutomatic;
    [SerializeField] private float fireRate;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float timeReload;

    [SerializeField] private RecoilSO recoilData;
    [SerializeField] private GunSlot slot;

    private bool isPressTrigger;
    private bool isShooting = false;
    private bool canShoot = true;
    private bool isReloaded;

    private float timeBetweenShots;
    private int ammoLeft;

    [SerializeField] private EmptyAction shootEvent;
    public Action<bool> viewEnemy = delegate { };
    [SerializeField] private ActionChannel<int> currentAmmoEvent;
    [SerializeField] private ActionChannel<int> maxAmmoEvent;
    [SerializeField] private ActionChannel<Transform> shootPointEvent;
    [SerializeField] private ActionChannel<int> damageValueEvent;
    [SerializeField] private BoolChanel p_isTriggerEvent;
    [SerializeField] private EmptyAction p_reloadEvent;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (maxAmmoEvent)
            maxAmmoEvent.InvokeEvent(maxAmmo);

        if (shootPointEvent)
            shootPointEvent.InvokeEvent(shootPoint);

        p_isTriggerEvent.Subscribe(HandleSetPressTrigger);
        p_reloadEvent.Subscribe(HandleReload);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        p_isTriggerEvent.Unsubscribe(HandleSetPressTrigger);
        p_reloadEvent.Unsubscribe(HandleReload);
    }

    private void Awake()
    {
        Validate();

        timeBetweenShots = 60 / fireRate;

        ammoLeft = maxAmmo;
    }

    private void Start()
    {
        if (maxAmmoEvent)
            maxAmmoEvent.InvokeEvent(maxAmmo);

        if (currentAmmoEvent)
            currentAmmoEvent.InvokeEvent(ammoLeft);
    }

    private void Update()
    {
        if (isPressTrigger && !isShooting && canShoot && ammoLeft > 0 && !isReloaded)
        {
            StartCoroutine(Shoot());
        }

        RaycastHit hit;

        viewEnemy?.Invoke(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, shootDistance, enemyMask));
    }

    protected override void HandleSetPressTrigger(bool pressTrigger)
    {
        isPressTrigger = pressTrigger;
        if (!isAutomatic && !pressTrigger)
        {
            canShoot = true;
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        canShoot = false;

        if (shootEvent)
            shootEvent.InvokeEvent();

        ammoLeft--;
        if (currentAmmoEvent)
            currentAmmoEvent.InvokeEvent(ammoLeft);

        yield return new WaitForSeconds(timeBetweenShots);

        if (isAutomatic)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        isShooting = false;
    }

    private void HandleReload()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        isReloaded = true;

        yield return new WaitForSeconds(timeReload);

        ammoLeft = maxAmmo;
        if (currentAmmoEvent)
            currentAmmoEvent.InvokeEvent(ammoLeft);

        isReloaded = false;
    }

    public override void SendWeaponParameters()
    {
        if (currentAmmoEvent)
            currentAmmoEvent.InvokeEvent(ammoLeft);
        if (maxAmmoEvent)
            maxAmmoEvent.InvokeEvent(maxAmmo);
        if (shootPointEvent)
            shootPointEvent.InvokeEvent(shootPoint);
        if (damageValueEvent)
            damageValueEvent.InvokeEvent(p_damage);
    }

    private void Validate()
    {
        if (!controller)
        {
            Debug.LogError($"{name}: FirstPersonController is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (enemyMask.value == 0)
        {
            Debug.LogError($"{name}: Select a LayerMask.\nDisabled component.");
            enabled = false;
            return;
        }
        if (fireRate <= 0)
        {
            Debug.LogError($"{name}: Rate fire cannot be 0 or less.\nPlease check and assign a valid number.\nDisabled component.");
            enabled = false;
            return;
        }
        if (maxAmmo <= 0)
        {
            Debug.LogError($"{name}: Max Ammo cannot be 0 or less.\nPlease check and assign a valid number.\nDisabled component.");
            enabled = false;
            return;
        }
        if (timeReload <= 0)
        {
            Debug.LogError($"{name}: TimeReload cannot be 0 or less.\nPlease check and assign a valid number.\nDisabled component.");
            enabled = false;
            return;
        }
    }
}