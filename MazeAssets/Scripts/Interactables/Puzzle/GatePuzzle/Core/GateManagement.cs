using System.Collections.Generic;
using UnityEngine;

namespace GateSystem3
{
    [CreateAssetMenu(fileName = "GateManagment2", menuName = "Interactables/Gate Managment/GateManagment 2")]
    public class GateManagement : ScriptableObject
    {
        [Header("Gate Property Status")]
        public bool allKeyCollected;
        public bool stoneHatchActive;
        public bool gateHandleActive;
        public bool missionComplete;
        [Header("----------------------")]

        [Header("Default Value")]
        public int numberOfKeyNeeded;
        [Header("----------------------")]

        [Header("Gate Event")]
        public GateEvent gateEvent;
        [Header("----------------------")]

        [Header("Key List")]
        public List<string> collectedKeyList;
        [Header("----------------------")]

        [Header("Element Position")]
        public ElementPosition elementPosition;

        public void ResetValues()
        {
            allKeyCollected = false;
            stoneHatchActive = false;
            gateHandleActive = false;
            missionComplete = false;
            numberOfKeyNeeded = 3;
            collectedKeyList.Clear();
            gateEvent.ResetValues();
        }
        public bool CheckAllKeyCollected()
        {
            return collectedKeyList.Count >= numberOfKeyNeeded;
        }
    }
}