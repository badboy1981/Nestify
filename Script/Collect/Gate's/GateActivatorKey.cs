using UnityEngine;

namespace Collectable
{
    public class GateActivatorKey : Collectable
    {
        [SerializeField] SaveSystem.SaveLevelDataSObject ActivatorKeyList;

        private void Awake()
        {
            ActivatorKeyList.GateKeyActivatorList.Clear();
        }
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            ActivatorKeyList.GateKeyActivatorList.Add(name);
        }
    }
}