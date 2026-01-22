using System.Collections.Generic;
using UnityEngine;

namespace GateSystem2
{
    [CreateAssetMenu(fileName = "GateManagment", menuName = "Interactables/Gate Managment/GateManagment")]
    public class GateManagment : ScriptableObject
    {
        [Header("Gate Open")]
        public bool gateOpen;
        [Header("----------------------")]

        [Header("Gate Event")]
        public GateEvent gateEvent;
        [Header("----------------------")]

        [Header("Default Value")]
        public int numberOfKeyNeeded;
        [Header("----------------------")]

        [Header("Key List")]
        public List<string> collectedKeyList;
        //[Header("----------------------")]

        //[Header("Gate Handle State")]
        //public bool activeGateHandle;

        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            
        }


        internal bool CheckAllKeyCollected()
        {
            return collectedKeyList.Count == numberOfKeyNeeded;
        }
        internal void ValueReset()
        {
            numberOfKeyNeeded = 3;
            collectedKeyList.Clear();
            gateEvent.RestValue();
            //activeGateHandle = false;
        }
    }
}