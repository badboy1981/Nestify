using UnityEngine;

namespace Collectable
{
    public class GateActivatorKey : Collectable
    {
        [SerializeField] Gate.GateProperty gateProperty;
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            gateProperty.keysLists.Find(g => CompareTag(g.KeyName)).Collected = true;
        }
    }
}