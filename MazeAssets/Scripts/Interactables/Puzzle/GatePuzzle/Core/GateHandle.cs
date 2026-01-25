using UnityEngine;

namespace GateSystem3
{
    internal class GateHandle : Interactive
    {
        [Header("Managment")]
        [SerializeField] GateManagement gateManagement;
        private void Start()
        {
            gateManagement =
                GetComponentInParent<Transform>().
                GetComponentInParent<GatePuzzleManager>().gateManagement;
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (!gateManagement.gateHandleActive) return;
            gateManagement.gateEvent.OnPushHandle = true;
        }
    }
}