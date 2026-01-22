using System.Collections.Generic;
using UnityEngine;

namespace GateSystem2
{
    public class GatePuzzleManager : MonoBehaviour
    {
        [SerializeField] GateManagment gateManagment;

        [Header("Gate Componenet")]
        [SerializeField] List<GameObject> KeyList;
        [Header("----------------------")]
        [SerializeField] GameObject StoneHatch;
        [Header("----------------------")]
        [SerializeField] List<GameObject> GateConteroller;
        [Header("----------------------")]
        [SerializeField] GameObject GateFence;

        private void Start()
        {
            gateManagment.ValueReset();
        }

        private void OnEnable()
        {
            gateManagment.gateEvent.OnKeyCollectedEvent += KeyCollected;
            gateManagment.gateEvent.OnAllKeyCollectedEvent += StoneHatchReAction;

        }
        private void OnDisable()
        {
            gateManagment.gateEvent.OnKeyCollectedEvent -= KeyCollected;
            gateManagment.gateEvent.OnAllKeyCollectedEvent -= StoneHatchReAction;
        }

        //Key: Key Collected Event Handler
        private void KeyCollected(string keyName)
        {
            gateManagment.collectedKeyList.Add(keyName);
        }

        //Stone Hatch: Check number of key's collected
        private void StoneHatchReAction()
        {
            if (true)
            {
                //Active Handle
            }
            else
            {

            }
        }
    }
}