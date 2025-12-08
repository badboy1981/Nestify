using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class KeyGetEvent
{
    [SerializeField] KeyProperty keyProperty;

    public UnityAction<KeyProperty> OnKeyGet;

    public KeyProperty KeyProperty
    {
        get => keyProperty;
        set
        {
            if (keyProperty == value)
                return;
            keyProperty = value;
            OnKeyGet?.Invoke(keyProperty);
        }
    }
}