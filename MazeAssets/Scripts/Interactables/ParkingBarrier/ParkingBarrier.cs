using Assets.MazeAssets.Scripts.Parent;
using UnityEngine;

namespace Collectable
{
    internal class ParkingBarrier : Interactive
    {
        [SerializeField] Animator _Animator;
        [SerializeField] GameObject _Collector;
        [SerializeField] GameObject CrainHandle;
        Collector _CollectorScript;

        [SerializeField] SaveSystem.SaveLevelDataSObject Gate;

        protected override void Awake()
        {
            _CollectorScript = _Collector.GetComponent<Collector>();
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (Gate.KeyList.Contains(CrainHandle.name.Replace("CH", "Key")))
            {
                base.OnTriggerEnter(other);
                _Animator.SetBool("CheckKey", true);
            }
        }
        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            _Animator.SetBool("CheckKey", false);
        }
    }
}