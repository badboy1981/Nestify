using UnityEngine;

namespace GateSystem2
{
    internal class GateHandleController : Interactive
    {
        [SerializeField] GateManagment gateManagment;
        [SerializeField] Animator handleAnimator;

        private void Start()
        {
            handleAnimator = GetComponentInChildren<Animator>();
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            gateManagment.gateEvent.GateActive = true;
        }
        private void OnEnable()
        {
            gateManagment.gateEvent.OnHatchActiveCompleteEvent += OnGateHandleActive;
            gateManagment.gateEvent.OnGateHandleEvent += OnRotateHandle;
        }
        private void OnDisable()
        {
            gateManagment.gateEvent.OnHatchActiveCompleteEvent -= OnGateHandleActive;
            gateManagment.gateEvent.OnGateHandleEvent -= OnRotateHandle;
        }
        private void OnGateHandleActive()
        {
            handleAnimator.SetBool("ActiveHandle", true);
        }
        private void OnRotateHandle(bool state)
        {
            handleAnimator.SetBool("RotateHandle", state);
        }
    }
}