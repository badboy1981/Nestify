using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectable
{
    public class Key : Collectable
    {
        [SerializeField] DataTest.ScriptableObjectDataTEST KeyList;
        public override void Collect()
        {
            base.Collect();
        }
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            KeyList.KeyLists.Add(gameObject.name);
            Debug.Log($"Get Key:{gameObject.name}");
        }
    }
}