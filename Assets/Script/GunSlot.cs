using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GunSlot : MonoBehaviour
{
    [SerializeField] private WeaponChanger WC;

    private void OnEnable()
    {
        StartCoroutine(WaitForEnable());
    }

    private void Awake()
    {
        if (!WC)
        {
            Debug.LogError($"{name}: WeaponChanger is null\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    public void ChangeGun(WeaponChanger newWeapon)
    {
        Transform tempTransform = newWeapon.transform;
        newWeapon.transform.position = WC.transform.position;
        WC.transform.position = tempTransform.position;
        WC.transform.parent = null;
        newWeapon.transform.parent = transform;
        newWeapon.transform.rotation = transform.rotation;
        WC.DesactiveWeapon();
        newWeapon.ActiveWeapon();
        WC = newWeapon;
    }

    private IEnumerator WaitForEnable()
    {
        yield return new WaitForSeconds(1);
        WC.ActiveWeapon();
    }
}