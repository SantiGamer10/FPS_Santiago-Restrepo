using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsGunController : MonoBehaviour
{
    [SerializeField] private EmptyAction shootingMoment;

    [SerializeField] private ParticleSystem spark;
    [SerializeField] private ParticleSystem flash;

    private void OnEnable()
    {
        if(shootingMoment)
            shootingMoment.Subscribe(HandleStartExplotion);
        spark.gameObject.SetActive(true);
        flash.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if(shootingMoment)
            shootingMoment.Unsubscribe(HandleStartExplotion);
        spark.gameObject.SetActive(false);
        flash.gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (!spark)
        {
            Debug.LogError($"{name}: Spark is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!flash)
        {
            Debug.LogError($"{name}: Flash is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void HandleStartExplotion()
    {
        spark.Play();
        flash.Play();
    }
}
