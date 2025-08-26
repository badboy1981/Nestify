using UnityEngine;

namespace Collectable
{
    internal class CraineKey : Collectable
    {
        [SerializeField] CrainKeySB KeyCollected;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            KeyCollected.KeysName.Add(name);
        }
    }
}