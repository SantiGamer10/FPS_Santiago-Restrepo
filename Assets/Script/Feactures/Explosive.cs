using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private ParticlesEffect pEffect;
    [SerializeField] private HealthPoints HP;

    [SerializeField] private float _radius = 10f;
    [SerializeField] private float _powerExplotion = 10f;

    private void OnEnable()
    {
        HP.dead += HandleExplotionOnDead;   
    }

    private void OnDisable()
    {
        HP.dead -= HandleExplotionOnDead;
    }

    private void Awake()
    {
        if (!HP)
        {
            Debug.LogError($"{name}: Health points is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;

        Gizmos.DrawSphere(transform.position, _radius);
    }

    private void HandleExplotionOnDead()
    {
        pEffect.ActiveParticles();
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);

        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent<Rigidbody>(out Rigidbody rib))
            {
                rib.AddExplosionForce(_powerExplotion, explosionPos, _radius, 3.0F);
            }
        }

    }
}