using UnityEngine;

namespace GateSystem3
{
    internal class GateHandle : Interactive
    {
        [Header("Managment")]
        [SerializeField] GateManagement gateManagement;

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