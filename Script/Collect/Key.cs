using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectable
{
    public class Key : Collectable
    {
        [SerializeField] SaveSystem.SaveLevelDataSObject KeyList;

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            KeyList.KeyList.Add(gameObject.name);
        }
    }
}