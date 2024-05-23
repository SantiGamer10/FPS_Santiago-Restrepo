using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour, IInteract
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private VisualEffectsGunController vFXController;
    [SerializeField] private GunSlot gunSlot;

    private void OnEnable()
    {
        DesactiveWeapon();
    }

    public void InteractiveMoment()
    {
        gunSlot.ChangeGun(this);
    }

    public void ActiveWeapon()
    {
        weapon.enabled = true;
        vFXController.enabled = true;
    }

    public void DesactiveWeapon()
    {
        weapon.enabled = false;
        vFXController.enabled = false;
    }
}
