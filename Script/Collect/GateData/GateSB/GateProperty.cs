using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Collectable.Gate
{
    [CreateAssetMenu(fileName = "GateProperty", menuName = "Gate/Gate Property")]
    public class GateProperty : ScriptableObject
    {
        [Header("Bool Check")]
        public bool ActiveGateHandleState;
        public bool gateIsBusy = false;

        [Header("Animation Wait Time")]
        public float AnimationWaitTime;

        [Header("Key List")]
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
        public void ResetProperty()
        {
            ActiveGateHandleState = false;
            gateIsBusy = false;
            AnimationWaitTime = 5;
            keysLists.Add(new KeysList() { KeyName = "KeyA", Collected = false });
            keysLists.Add(new KeysList() { KeyName = "KeyB", Collected = false });
            keysLists.Add(new KeysList() { KeyName = "KeyC", Collected = false });
        }
    }
}