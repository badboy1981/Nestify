using UnityEngine;
namespace Collectable
{
    public class MagnetKey : Collectable
    {
        [Header("Target Gate Property")]
        [SerializeField] Transform GateTarget;
        [SerializeField] Animator GateTargetAnimator;
        [SerializeField] float GateOpen;
        private Vector3 OriginPos;
        private Vector3 DestinationPos;

        private void Start()
        {
            GateOpen = 0;
            GateTargetAnimator = GetComponent<Animator>();
            OriginPos = GateTarget.position;
            DestinationPos = new(OriginPos.x, OriginPos.y + 5, OriginPos.z);
        }
        private void FixedUpdate()
        {
            GateTarget.position = Vector3.Lerp(OriginPos, DestinationPos, GateOpen);
        }
        public override void OnTriggerEnter(Collider other)
        {
            GateTargetAnimator.SetBool("PressKey", true);
        }
        private void OnTriggerExit(Collider other)
        {
            GateTargetAnimator.SetBool("PressKey", false);
            GateTarget.position = Vector3.Lerp(OriginPos, DestinationPos, GateOpen);
        }
    }
}