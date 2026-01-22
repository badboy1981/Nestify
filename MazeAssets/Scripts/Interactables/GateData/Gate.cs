using UnityEngine;

namespace GateSystem
{
    [RequireComponent(typeof(Animator))]
    internal class Gate : Interactive
    {
        [SerializeField] private GateSystemManager manager;
        [SerializeField] private Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void OnAnimateGate()
        {
            PlaySound("OpenGateSound");
        }
        public void GateDefualtPosition()
        {
            manager.CloseGate();
        }
    }
}