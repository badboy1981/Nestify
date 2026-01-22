using UnityEngine;

namespace GateSystem
{
    internal class GateController : Interactive
    {
        [SerializeField] GateSystemManager manager;
        [SerializeField] Animator gateAnimator;
        //[SerializeField] GameObject gateHandle;
        [SerializeField] Animator handleAnimator;
        private void Start()
        {
            //gameObject.SetActive(false);
        }
        void OnEnable()
        {
            manager.OnAllKeysCollected += ActiveHandle;
            manager.OnGateOpen += StartGateSequence;
            manager.OnGateClose += StartGateSequence;
        }
        void OnDisable()
        {
            manager.OnAllKeysCollected -= ActiveHandle;
            manager.OnGateOpen -= StartGateSequence;
            manager.OnGateClose -= StartGateSequence;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            manager.OpenGate();
        }
        protected override void OnTriggerExit(Collider other)
        {
            //animator.SetBool("OpenGate", false);

        }
        private void StartGateSequence(bool state)
        {
            handleAnimator.SetBool("RotateHandle", state);
            //gateAnimator.SetBool("OpenGate", state);
            //PlaySound("OpenGateSound");
        }
        private void ActiveHandle()
        {
            handleAnimator.SetBool("ActiveHandle", true);
        }
    }
}