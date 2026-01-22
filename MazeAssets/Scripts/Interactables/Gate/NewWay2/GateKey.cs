using UnityEngine;

namespace GateSystem2
{
    public class GateKey : Collectable.Collectable
    {
        [SerializeField] GateManagment gateManagment;
        protected override void OnTriggerEnter(Collider other)
        {
            gateManagment.gateEvent.OnKeyCollected(tag);
            base.OnTriggerEnter(other);
        }
    }
}