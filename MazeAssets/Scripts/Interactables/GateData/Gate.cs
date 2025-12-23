using UnityEngine;

namespace GateSystem
{
    [RequireComponent(typeof(Animator))]
    internal class Gate : Interactive
    {
        [SerializeField] Animator animator;
        [SerializeField] bool GateFirstOpen;
        private void Start()
        {
            GateFirstOpen = true;
            animator = GetComponent<Animator>();
        }
        public void OnGateOpen()
        {
            if (animator.GetBool("OpenGate")) PlaySound("OpenGateSound");
            GateFirstOpen = false;
        }
        public void OnGateClose()
        {
            if (GateFirstOpen) return;
            if (!animator.GetBool("OpenGate")) PlaySound("OpenGateSound");
        }
        public void GateDefualtPosition()
        {
            animator.SetBool("OpenGate", false);
        }
    }
}