using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    internal class Key : Collectable
    {
        [SerializeField] GameEntityProperty KeyProperty;
        [SerializeField] SaveSystem.SaveLevelDataSObject KeyList;
        [SerializeField] GateManagment gateManagment;
        //private KeyCollectedEvent keyCollected;
        private UnityAction<GameEntityProperty> OnCollectedKeyEvent;
        protected override void OnTriggerEnter(Collider other)
        {
            OnCollectedKeyEvent?.Invoke(KeyProperty);
            base.OnTriggerEnter(other);
        }
        private void OnEnable()
        {
            OnCollectedKeyEvent += OnKeyCollected;
        }
        private void OnDisable()
        {
            OnCollectedKeyEvent -= OnKeyCollected;
        }
        private void OnKeyCollected(GameEntityProperty property)
        {
            gateManagment.UpdateNew(property.UniqueID);
        }
    }
}