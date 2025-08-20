using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    internal class OtherThings : Collectable
    {
        internal static event UnityAction OtherThingsCollectedEvent = delegate { };
        internal override void Collect()
        {
            base.Collect();
            OtherThingsCollectedEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}