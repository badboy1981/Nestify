using System.Collections.Generic;
using UnityEngine;

namespace GateSystem3
{
    internal class GatePuzzleManager : MonoBehaviour
    {
        public GateManagement gateManagement;
        [SerializeField] List<Animator> HandleList;
        [SerializeField] Animator gate;

        private void Start()
        {
            gateManagement.ResetValues();
        }
        private void OnEnable()
        {
            gateManagement.gateEvent.OnKeyCollectedEvent += KeyAddToList;
            gateManagement.gateEvent.OnStoneHatchActiveEvent += ActiveHandle;
            gateManagement.gateEvent.OnPushHandleEvent += OpneGate;
        }
        private void OnDisable()
        {
            gateManagement.gateEvent.OnKeyCollectedEvent -= KeyAddToList;
            gateManagement.gateEvent.OnStoneHatchActiveEvent -= ActiveHandle;
            gateManagement.gateEvent.OnPushHandleEvent -= OpneGate;
        }
        private void KeyAddToList(string keyName)
        {
            if (gateManagement.allKeyCollected) return;
            if (!gateManagement.collectedKeyList.Contains(keyName))
            {
                gateManagement.collectedKeyList.Add(keyName);
            }
            gateManagement.allKeyCollected = gateManagement.CheckAllKeyCollected();
        }
        private void ActiveHandle(bool state)
        {
            if (gateManagement.gateHandleActive) return;
            gateManagement.gateHandleActive = state;
            foreach (var item in HandleList)
            {
                item.SetBool("ActiveHandle", state);
            }
        }
        private void OpneGate(bool state)
        {
            foreach (var item in HandleList)
            {
                item.SetBool("PushHandle", state);
            }
            gate.SetBool("OpenGate", state);
        }
    }
}
