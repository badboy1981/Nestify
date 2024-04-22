using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    public class Arrow : Collectable
    {
        public static event UnityAction ArrowCollectedEvent = delegate { };
        public static event UnityAction SpeedChangeEvent = delegate { };
        int Counter = 0;
        public override void Collect()
        {
            base.Collect();
            Counter++;
            ArrowCollectedEvent?.Invoke();
            Destroy(gameObject);
            //Debug.Log($"Arrow Collected {Counter}");            
        }
        public override void SpeedChange()
        {
            //base.SpeedChange();
            SpeedChangeEvent?.Invoke();
        }
    }
}