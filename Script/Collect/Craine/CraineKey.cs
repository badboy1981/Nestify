using UnityEngine;

namespace Collectable
{
    public class CraineKey : Collectable
    {
        [SerializeField] CrainKeySB KeyCollected;

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            KeyCollected.KeysName.Add(name);
        }
    }
}