using System.Net;
using Unity.VisualScripting;
using UnityEngine;

namespace GateSystem3
{
    internal class GateHandle : Interactive
    {
        [Header("Managment")]
        [SerializeField] GateManagement gateManagement;
        //[SerializeField] Animator handleAnimator;
        //[Header("----------------------")]

        //private void Start()
        //{
        //    handleAnimator = GetComponentInChildren<Animator>();
        //}
        ////private void OnEnable() { gateManagement.gateEvent.OnStoneHatchActiveEvent += ActiveHandle; }
        ////private void OnDisable() { gateManagement.gateEvent.OnStoneHatchActiveEvent -= ActiveHandle; }

        //private void ActiveHandle(bool state)
        //{
        //    if (gateManagement.gateHandleActive) return;
        //    gateManagement.gateHandleActive = state;
        //    gateManagement.missionComplete = true;
        //    handleAnimator.SetBool("ActiveHandle", state);
        //}
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            gateManagement =
                GetComponentInParent<Transform>().
                GetComponentInParent<GatePuzzleManager>().gateManagement;
            if (!gateManagement.gateHandleActive) return;
            gateManagement.gateEvent.OnPushHandle = true;
        }
    }
}