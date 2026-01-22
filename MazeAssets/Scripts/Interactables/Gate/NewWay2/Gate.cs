using UnityEngine;
using UnityEngine.Rendering;

namespace GateSystem2
{
    [RequireComponent(typeof(Animator))]

    internal class Gate : Interactive
    {
        [Header("Managment")]
        [SerializeField] GateManagment gateManagment;
        [SerializeField] Animator gateAnimator;
        //[Header("----------------------")]

        private void Start()
        {
            gateAnimator = GetComponent<Animator>();
        }
        private void OnEnable() { gateManagment.gateEvent.OnGateHandleEvent += GateAnimate; }
        private void OnDisable() { gateManagment.gateEvent.OnGateHandleEvent -= GateAnimate; }

        private void GateAnimate(bool state)
        {
            gateAnimator.SetBool("OpenGate", state);
        }
        public void PlayGateSound()
        {
            PlaySound("OpenGateSound");
        }
        public void GateDefualtPosition()
        {
            gateManagment.gateEvent.GateActive = false;
        }
    }
}