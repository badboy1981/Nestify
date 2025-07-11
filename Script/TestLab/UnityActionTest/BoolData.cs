using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolData", menuName = "My Asset/Bool Data")]
public class BoolData : ScriptableObject
{
    public bool _value;

    [System.NonSerialized]
    public UnityAction onChangedToTrue;

    [System.NonSerialized]
    public UnityAction onChangedToFalse;

    public bool Value
    {
        get => _value;
        set
        {
            if (_value == value)
                return;

            _value = value;

            if (_value)
                onChangedToTrue?.Invoke();
            else
            {
                onChangedToFalse?.Invoke();
            }
        }
    }
}