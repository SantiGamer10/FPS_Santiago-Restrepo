using UnityEngine;

public abstract class DataSource<T> : ScriptableObject
{
    [SerializeField] protected T reference;
    [SerializeField] private bool logEnabled = true;

    public T Reference
    {
        get => reference;
        set
        {
            if (value != null && logEnabled)
                Debug.Log($"{name}: Changed value to {value}");
            reference = value;
        }
    }
}