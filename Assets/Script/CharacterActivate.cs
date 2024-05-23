using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CharacterActivate : MonoBehaviour
{
    [SerializeField] private FirstPersonController player;
    [SerializeField] private Transform look;
    [SerializeField] private float distance;
    [SerializeField] private EmptyAction interactEvent;

    public Action viewInteractObject = delegate { };

    private void OnEnable()
    {
        interactEvent.Subscribe(ViewActiveObject);
    }

    private void OnDisable()
    {
        interactEvent.Unsubscribe(ViewActiveObject);
    }

    private void Awake()
    {
        if (!player)
        {
            Debug.LogError($"{name}: First person vFXController is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void ViewActiveObject()
    {
        if (Physics.Raycast(look.position, look.forward, out var hit, distance))
        {
            if (hit.transform.TryGetComponent<IInteract>(out IInteract interact))
            {
                interact.InteractiveMoment();
            }
        }
    }
}