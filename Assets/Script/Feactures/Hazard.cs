using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter(Collider col)
    {
        HealthPoints hp;
        Debug.Log($"{name} collided with {col.name}");

        if (col.gameObject.TryGetComponent(out hp))
        {
            Debug.Log($"{name} try damaged {col.name}");
            hp.TakeDamage(damage);
        }
    }
}

public interface IHazard
{
    private void DoDamage() { }
}