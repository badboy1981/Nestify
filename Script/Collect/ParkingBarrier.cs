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
        //[SerializeField] TextMeshProUGUI WinMessage;
        [SerializeField] GameObject CrainHandle;
        Collector _CollectorScript;
        public override void Collect()
        {
            //base.Collect();            
        }
        private void Awake()
        {
            _CollectorScript = _Collector.GetComponent<Collector>();
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(CrainHandle.name);
            if (_CollectorScript.CollectableName.Contains(CrainHandle.name.Replace("CH", "Key")))
            {
                _Animator.SetBool("CheckKey", true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            _Animator.SetBool("CheckKey", false);
        }
    }
}