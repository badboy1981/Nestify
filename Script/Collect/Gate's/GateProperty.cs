using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Collectable.Gate
{
    [CreateAssetMenu(fileName = "BoolData", menuName = "My Asset/Gate Property")]
    public class GateProperty : ScriptableObject
    {
        public string SignLabel;
        public string HatchName;
        public string TargetGateName;
        public bool ActiveGateHandleState;
        public bool gateIsBusy = false;
        public float AnimationWaitTime;
        public List<KeysList> keysLists = new();

        [System.NonSerialized] public UnityAction OnGateUnlocked;
        [System.NonSerialized] public UnityAction OnGatelocked;

        public bool GateState
        {
            get => ActiveGateHandleState;
            set
            {
                if (ActiveGateHandleState == value)
                    return;

                ActiveGateHandleState = value;

                if (ActiveGateHandleState)
                    OnGateUnlocked?.Invoke();
                else
                {
                    OnGatelocked?.Invoke();
                }
            }
        }
    }
}