using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    internal class Key : Collectable
    {
        //[SerializeField] SaveSystem.SaveLevelDataSObject KeyList;
        [Header("Internal Parameter")]
        [SerializeField] GameEntityProperty keyProperty;
        //[SerializeField] GateManagment gateManagment;
        //private KeyCollectedEvent keyCollected;
        private event UnityAction<GameEntityProperty> OnCollectedKeyEvent;

        private void Start()
        {
            keyProperty.ID = ($"{name[0]}{name[1]}");    // Assigns the first character of the GameObject's name as the ID
            keyProperty.Value = name;                    // Assigns the GameObject's name as the Value
            keyProperty.Type = EntityTypeEnum.GateKey;   // Sets the Type to Key
            keyProperty.IsCollected = false;
            //gateManagment.KeyCollectedCounter = 0;
        }
        protected override void OnTriggerEnter(Collider other)
        {
            keyProperty.IsCollected = true;
            //gateManagment.gateConfig.RequiredKeys.Add(keyProperty);
            //OnCollectedKeyEvent?.Invoke(keyProperty);
            //gateManagment.KeyCollectedCounter++;
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
            //gateManagment.UpdateKeyStatus(property.UniqueID);
        }
    }
}