using UnityEngine;
namespace Collectable
{
    public class MagnetKey : Collectable
    {
        [Header("Target Gate Property")]
        [SerializeField] Transform GateTarget;
        [SerializeField] Animator GateTargetAnimator;
        //[SerializeField] float GateOpen;
        private Vector3 OriginPos;
        private Vector3 DestinationPos;

        private void Start()
        {
            //GateOpen = 0;
            OriginPos = GateTarget.position;
            DestinationPos = new(OriginPos.x, OriginPos.y + 5, OriginPos.z);
        }
    }
}