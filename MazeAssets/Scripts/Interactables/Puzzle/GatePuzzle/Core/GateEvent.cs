using System;
using UnityEngine;
using UnityEngine.Events;

namespace GateSystem3
{
    [Serializable]
    public class GateEvent
    {
        [SerializeField] bool _StoneHatchActive;
        [SerializeField] bool _PushHandle;
        public void ResetValues()
        {
            _StoneHatchActive = false;
            _PushHandle = false;
        }

        //1- Key Collected Event
        public UnityAction<string> OnKeyCollectedEvent;
        public void OnKeyCollected(string keyName)
        {
            OnKeyCollectedEvent?.Invoke(keyName);
        }

        //2- StoneHatch Active Event
        public UnityAction<bool> OnStoneHatchActiveEvent;
        public bool OnStoneHatchActive
        {
            get => _StoneHatchActive;
            set
            {
                if (_StoneHatchActive == value) return;
                _StoneHatchActive = value;
                OnStoneHatchActiveEvent?.Invoke(_StoneHatchActive);
            }
        }

        //3- Push Gate Handle Event
        public UnityAction<bool> OnPushHandleEvent;
        public bool OnPushHandle
        {
            get => _PushHandle;
            set
            {
                if (_PushHandle == value) return;
                _PushHandle = value;
                OnPushHandleEvent?.Invoke(_PushHandle);
            }
        }
    }
}