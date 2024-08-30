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
#if UNITY_EDITOR

#endif
        private void Awake()
        {
            _CollectorScript = _Collector.GetComponent<Collector>();
        }
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log($"Crain Name: {CrainHandle.name} || Other Name: {other.name} || Key List: {CrainHandle.name.Replace("CH", "Key")}");
            if (_CollectorScript._PlayerData.KeyLists.Contains(CrainHandle.name.Replace("CH", "Key")))
            {
                _Animator.SetBool("CheckKey", true);
                //Debug.Log($"Connect: {CrainHandle.name}");
            }
            //else
            //{
            //    Debug.Log($"Crain Handle Name: {CrainHandle.name} || Key List: {string.Join(',', _CollectorScript._PlayerData.KeyLists)}");
            //}
        }
        private void OnTriggerExit(Collider other)
        {
            _Animator.SetBool("CheckKey", false);
        }
    }
}