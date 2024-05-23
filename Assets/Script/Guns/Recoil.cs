using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Recoil : MonoBehaviour
{
    [Header("Recoil Parameters")]
    [SerializeField] private RecoilSO recoilData;
    [Header("Channels")]
    [SerializeField] private EmptyAction shootMoment;

    [SerializeField] private float threshold = 0.0001f;

    private Quaternion currentRotation;
    private Quaternion targetRotation;

    private void OnEnable()
    {
        if(shootMoment)
            shootMoment.Subscribe(HandleShootMoment);
    }

    private void OnDisable()
    {
        if (shootMoment)
            shootMoment.Unsubscribe(HandleShootMoment);
    }

    private void Awake()
    {
        if (!recoilData)
        {
            Debug.LogError($"{name}: Data is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (Quaternion.Dot(targetRotation, targetRotation) < threshold * threshold)
        {
            targetRotation = Quaternion.Euler(Vector3.one * threshold);
            return;
        }
        targetRotation = Quaternion.Lerp(targetRotation, Quaternion.identity, recoilData.returnSpeed * Time.deltaTime);
        currentRotation = Quaternion.Slerp(currentRotation, targetRotation, recoilData.snappiness * Time.fixedDeltaTime);
        transform.localRotation = currentRotation;
    }

    private void HandleShootMoment()
    {
        Vector3 rotation = Vector3.zero;
        rotation.x = recoilData.recoilX;
        rotation.y = Random.Range(recoilData.minRecoilY, recoilData.maxRecoilY);
        rotation.z = Random.Range(recoilData.minRecoilZ, recoilData.maxRecoilZ);
        targetRotation *= Quaternion.Euler(rotation);
    }
}