using System;
using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    [Serializable]
    public class CollectableEvent
    {
        [SerializeField] string _collectedItem;

        public event UnityAction OnCoinCollectedEvent=delegate { };     

        public string CollectedItem
        {
            get => _collectedItem;
            set
            {
                _collectedItem = value;
                switch (_collectedItem)
                {
                    case ("Coin"):
                        OnCoinCollectedEvent?.Invoke();
                        break;
                    default:
                        break;
                }
            }
        }

    }
}