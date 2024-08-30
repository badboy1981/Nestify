using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectable
{
    public class Key : Collectable
    {
        public override void Collect()
        {
            base.Collect();
        }
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            //Debug.Log($"Get Key:{gameObject.name}");
        }
    }
}