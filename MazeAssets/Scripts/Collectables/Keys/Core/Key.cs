using UnityEngine;

namespace Collectable
{
    internal class Key : Collectable
    {
        [SerializeField] SaveSystem.SaveLevelDataSObject KeyList;
        [SerializeField] KeyManagment keyManagment;
        [SerializeField] KeyProperty keyProperty;
        private void Start()
        {
            KeyList.KeyList.Clear();
            keyManagment.collectedKeyIDs.Clear();
        }
        protected override void OnTriggerEnter(Collider other)
        {
            keyManagment.keyGetEvent.KeyProperty = keyProperty;
            base.OnTriggerEnter(other);
        }
    }
}