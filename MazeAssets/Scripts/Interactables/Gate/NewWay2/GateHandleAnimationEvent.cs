using UnityEngine;

namespace GateSystem2
{
    [RequireComponent(typeof(Animator))]
    internal class GateHandleAnimationEvent : MonoBehaviour
    {
        [SerializeField] GateManagment gateManagment;
        [SerializeField] Animator handleAnimator;

        private void Start()
        {
            handleAnimator = GetComponent<Animator>();
        }
        //private void OnStartRotate()
        //{

        //}
        private void OnStopRotate()
        {
            gateManagment.gateEvent.GateActive = false;
            handleAnimator.SetBool("RotateHandle", false);
        }
    }
}