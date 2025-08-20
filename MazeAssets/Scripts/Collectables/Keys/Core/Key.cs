using UnityEngine;

namespace Collectable
{
    internal class Key : Collectable
    {
        [SerializeField] SaveSystem.SaveLevelDataSObject KeyList;

        protected override void Awake()
        {
            KeyList.KeyList.Clear();
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            KeyList.KeyList.Add(gameObject.name);
        }
    }
}