using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    internal class Key : Collectable
    {
        //[SerializeField] SaveSystem.SaveLevelDataSObject KeyList;
        [Header("Internal Parameter")]
        [SerializeField] GameEntityProperty keyProperty;        
        [SerializeField] GateManagment gateManagment;
        //private KeyCollectedEvent keyCollected;
        private event UnityAction<GameEntityProperty> OnCollectedKeyEvent;

        private void Start()
        {
            keyProperty.IsCollected = false;    
            gateManagment.KeyCollectedCounter = 0;
        } 
        protected override void OnTriggerEnter(Collider other)
        {
            keyProperty.IsCollected = true;
            OnCollectedKeyEvent?.Invoke(keyProperty);
            gateManagment.KeyCollectedCounter++;
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
            gateManagment.UpdateKeyStatus(property.UniqueID);
        }
    }
}