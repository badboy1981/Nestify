using UnityEngine;

namespace GateSystem3
{
    public class GateKey : Collectable.Collectable
    {
        [SerializeField] GateManagement gateManagement;

        protected override void OnTriggerEnter(Collider other)
        {
            gateManagement = GetComponentInParent<Transform>().GetComponentInParent<GatePuzzleManager>().gateManagement;
            gateManagement.gateEvent.OnKeyCollected(tag);
            //gateManagment.collectedKeyList.Add(tag);
            base.OnTriggerEnter(other);
        }
    }
}