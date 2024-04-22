using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    public class OtherThings : Collectable
    {
        public static event UnityAction OtherThingsCollectedEvent = delegate { };
        public override void Collect()
        {
            base.Collect();
            OtherThingsCollectedEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}