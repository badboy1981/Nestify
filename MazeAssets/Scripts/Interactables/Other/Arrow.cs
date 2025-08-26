using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    internal class Arrow : Collectable
    {
        public static event UnityAction ArrowCollectedEvent = delegate { };
        public static event UnityAction SpeedChangeEvent = delegate { };
        int Counter = 0;
        internal override void Collect()
        {
            base.Collect();
            Counter++;
            ArrowCollectedEvent?.Invoke();
            Destroy(gameObject);
            //Debug.Log($"Arrow Collected {Counter}");            
        }
        protected override void SpeedChange()
        {
            //base.SpeedChange();
            SpeedChangeEvent?.Invoke();
        }
    }
}