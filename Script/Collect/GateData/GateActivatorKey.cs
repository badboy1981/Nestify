using UnityEngine;
using Collectable.Gate;

namespace Collectable
{
    public class GateActivatorKey : Collectable
    {
        [Header("Gate Property")]
        [SerializeField] GatePropertyGroup gatePropertyGroup;
        [SerializeField] GateProperty gateProperty;

        private void Start()
        {
            gateProperty = FindGateProperty.GetGateProperty(gatePropertyGroup, name);            
        }
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            //gateProperty = FindGateProperty.GetGateProperty(gatePropertyGroup, name);
            gateProperty.keysLists.Find(g => CompareTag(g.KeyName)).Collected = true;
        }
    }
}