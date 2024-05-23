using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private BoolChanel isTriggerEvent;
    [SerializeField] private Vector2Channel directionEvent;
    [SerializeField] private Vector2Channel lookEvent;
    [SerializeField] private EmptyAction jumpEvent;
    [SerializeField] private BoolChanel sprintEvent;
    [SerializeField] private EmptyAction reloadEvent;
    [SerializeField] private EmptyAction interactEvent;

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        directionEvent.InvokeEvent(inputContext.ReadValue<Vector2>());
    }

    public void SetJump(InputAction.CallbackContext inputContext)
    {
        jumpEvent.InvokeEvent();
    }

    public void SetLook(InputAction.CallbackContext inputContext)
    {
        lookEvent.InvokeEvent(inputContext.ReadValue<Vector2>());
    }

    public void SetSprint(InputAction.CallbackContext inputContext)
    {
        sprintEvent.InvokeEvent(inputContext.ReadValueAsButton());
    }

    public void SetShoot(InputAction.CallbackContext inputContext)
    {
        isTriggerEvent.InvokeEvent(inputContext.ReadValueAsButton());
    }

    public void SetReload(InputAction.CallbackContext inputContext)
    {
        reloadEvent.InvokeEvent();
    }

    public void SetInteract(InputAction.CallbackContext inputContext)
    {
        interactEvent.InvokeEvent();
    }
}