using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channels/Empty Channels", fileName = "EmptyChannel")]
public class EmptyAction : ScriptableObject
{
    [SerializeField] private ActionConfig configurations;
    private Action _event = delegate { };

    public void Subscribe(Action action)
    {
        _event += action;
        if (configurations.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was suscribed at Event.");
        }
    }

    public void Unsubscribe(Action action)
    {
        _event += action;
        if (configurations.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was unsubscribed at Event.");
        }
    }

    public void InvokeEvent()
    {
        _event?.Invoke();
        if (configurations.eventLog)
        {
            Debug.Log($"{name}: The event was invoked.");
        }
    }
}
