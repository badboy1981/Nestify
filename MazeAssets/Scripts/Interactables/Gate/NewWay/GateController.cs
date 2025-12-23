using UnityEngine;

namespace GateSystem
{
    internal class GateController : Interactive
    {
        [SerializeField] private GateSystemManager manager;
        [SerializeField] Animator animator;
        private void Start()
        {
            gameObject.SetActive(false);
        }
        void OnEnable() { manager.OnGateOpen += StartGateSequence; }
        void OnDisable() { manager.OnGateOpen -= StartGateSequence; }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            manager.OpenGate();
        }
        protected override void OnTriggerExit(Collider other)
        {
            animator.SetBool("OpenGate", false);
            
        }
        void StartGateSequence(bool state)
        {
            animator.SetBool("OpenGate", state);
            //PlaySound("OpenGateSound");
        }
    }
}