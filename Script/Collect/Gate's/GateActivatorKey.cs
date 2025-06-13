using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

namespace Collectable
{
    public class GateActivatorKey : Collectable
    {
        [SerializeField] SaveSystem.SaveLevelDataSObject ActivatorKeyList;

        private void Awake()
        {
            ActivatorKeyList.CollectedGateActivatorListKey.Clear();
        }
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            ActivatorKeyList.CollectedGateActivatorListKey.Add(name);
        }
    }
}