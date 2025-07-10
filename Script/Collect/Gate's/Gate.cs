using System.Collections.Generic;
using UnityEngine.Events;

namespace Collectable.Gate
{
    [System.Serializable]
    public class GateProperty
    {
        public string SignLabel;
        public string HatchName;
        public string TargetGateName;
        public bool _ActiveGateHandleState;
        public List<KeysList> keysLists = new();

        public UnityEvent<bool> OnStateChangedEvent = new();
        public UnityAction<bool> OnStateChangedEventB = delegate { };
        
        public bool ActiveGateHandleState
        {
            get => _ActiveGateHandleState;
            set
            {
                if (_ActiveGateHandleState != value)
                {
                    _ActiveGateHandleState = value;
                    OnStateChangedEvent?.Invoke(_ActiveGateHandleState); // Notify this specific gate's listeners
                }
            }
        }
    }

    [System.Serializable]
    public class KeysList
    {
        public string KeyName;
        public bool Collected;
    }
}