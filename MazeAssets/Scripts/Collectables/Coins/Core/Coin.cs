using UnityEngine;

namespace Collectable
{
    internal class Coin : Collectable
    {
        //public static event UnityAction CoinCollectedEvent = delegate { };
        //public static event UnityAction<string> TestEvent = delegate { };

        [SerializeField] SaveSystem.SaveLevelDataSObject CoinCounter;
        [SerializeField] CollectableEventManagement CollectetedManagement;

        protected override void OnTriggerEnter(Collider other)
        {
            CollectetedManagement.collectableEvent.CollectedItem = tag;
            base.OnTriggerEnter(other);
            CoinCounter.CoinCounter += 1;
        }
    }
}