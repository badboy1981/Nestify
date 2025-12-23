
using UnityEngine;

namespace GateSystem
{
    public class GateKey : Collectable.Collectable
    {
        [SerializeField] private GateSystemManager manager; 

        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            manager.CollectKey();
            base.OnTriggerEnter(other);
        }
    }
}