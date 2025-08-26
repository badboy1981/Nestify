using UnityEngine;
//using UnityEngine.Events;

namespace Collectable
{
    internal class Coin : Collectable
    {
        //public static event UnityAction CoinCollectedEvent = delegate { };
        //public static event UnityAction<string> TestEvent = delegate { };

        [SerializeField] SaveSystem.SaveLevelDataSObject CoinCounter;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            CoinCounter.CoinCounter += 1;
        }
    }
}