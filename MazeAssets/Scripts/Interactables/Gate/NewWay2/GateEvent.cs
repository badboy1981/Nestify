using System;
using UnityEngine;
using UnityEngine.Events;

namespace GateSystem2
{
    [Serializable]
    public class GateEvent
    {
        [SerializeField] bool _hatchKeyCheck;
        [SerializeField] bool _hatchActiveComplete;
        [SerializeField] bool _handleActive;
        [SerializeField] bool _gateActive;

        //Reset value wothout raise the event. On start reset.
        public void RestValue()
        {
            _hatchKeyCheck = false;
            _hatchActiveComplete = false;
            _handleActive = false;
            _gateActive = false;
        }
        //Key: Send Key name collected
        public UnityAction<string> OnKeyCollectedEvent;
        public void OnKeyCollected(string keyName)
        {
            OnKeyCollectedEvent?.Invoke(keyName);
        }

        //StoneHatch 1: Check Key Collected
        public UnityAction OnKeyNeedEvent;
        public UnityAction OnAllKeyCollectedEvent;
        public bool HatchKeyCheck
        {
            get => _hatchKeyCheck;
            set
            {
                if (_hatchKeyCheck == value)
                {
                    OnKeyNeedEvent?.Invoke();
                }
                else
                {
                    _hatchKeyCheck = value;
                    OnAllKeyCollectedEvent?.Invoke();
                }
            }
        }

        //StoneHatch 2: Active Complete
        public UnityAction OnHatchActiveCompleteEvent;
        public bool HatchActiveComplete
        {
            get => _hatchActiveComplete;
            set
            {
                if (_hatchActiveComplete == value) return;
                _hatchActiveComplete = value;
                OnHatchActiveCompleteEvent?.Invoke();
            }
        }

        public bool HandleActive
        {
            get => _handleActive;
            set
            {//باید یک چک تعداد کلید اضافه کنم که بتونم خط بعد رو فعال کنم
                if (_handleActive == value) return;
                _handleActive = value;
                OnAllKeyCollectedEvent?.Invoke();
            }
        }

        //Gate Handle:
        public UnityAction<bool> OnGateHandleEvent;
        public bool GateActive
        {
            get => _gateActive;
            set
            {
                if (_gateActive == value) return;
                _gateActive = value;
                OnGateHandleEvent?.Invoke(_gateActive);
            }
        }

        //Gate Open-Close

    }
}