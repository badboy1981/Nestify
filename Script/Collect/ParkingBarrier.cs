using Collectable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Collectable
{
    public class ParkingBarrier : Collectable
    {
        [SerializeField] Animator _Animator;
        [SerializeField] GameObject _Collector;
        [SerializeField] GameObject CrainHandle;
        Collector _CollectorScript;

        [SerializeField] SaveSystem.SaveLevelDataSObject Gate;

        private void Awake()
        {
            _CollectorScript = _Collector.GetComponent<Collector>();
        }
        public override void OnTriggerEnter(Collider other)
        {
            if (Gate.KeyList.Contains(CrainHandle.name.Replace("CH", "Key")))
            {
                _Animator.SetBool("CheckKey", true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            _Animator.SetBool("CheckKey", false);
        }

        private void OpenGateOldWay()
        {
            if (_CollectorScript._PlayerData.KeyLists.Contains(CrainHandle.name.Replace("CH", "Key")))
            {
                _Animator.SetBool("CheckKey", true);
            }
        }
    }
}