using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Parameters")]
    [SerializeField] protected LayerMask p_enemyMask;
    [SerializeField] protected int p_damage;
    [SerializeField] protected GunSlot p_slot;

    [SerializeField] protected EmptyAction clickMoment;
    [SerializeField] protected BoolChanel isTriggerEvent;

    [Header("Manager")]
    [SerializeField] protected FirstPersonController firstPersonController;

    protected bool  p_isPressTrigger;

    protected virtual void OnEnable()
    {
        if (isTriggerEvent)
            isTriggerEvent.Subscribe(HandleSetPressTrigger);
    }

    protected virtual void OnDisable()
    {
        if(isTriggerEvent)
            isTriggerEvent.Unsubscribe(HandleSetPressTrigger);
    }

    protected virtual void HandleSetPressTrigger(bool pressTrigger)
    {
        p_isPressTrigger = pressTrigger;
    }

    public virtual void SendWeaponParameters()
    {

    }
}