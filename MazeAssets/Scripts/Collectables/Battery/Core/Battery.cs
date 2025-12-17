using SaveSystem;
using System;
using UnityEngine;

namespace Collectable
{
    [Serializable]
    public class Battery : Collectable
    {
        [SerializeField] SaveLevelDataSObject PlayerData;
        [SerializeField] ChargeManagment chargeManagment;
        //[SerializeField] BatteryDiagram _BatteryDiagram;
        //[SerializeField] float IncreaseValue = 20f;

        protected override void OnTriggerEnter(Collider other)
        {
            //_BatteryDiagram = GameObject.Find("BatteryDiagram").GetComponent<BatteryDiagram>();
            base.OnTriggerEnter(other);
            //chargeManagment.CVStatus.VoltChargeLevel += chargeManagment.chargeEffect.Battery;
            chargeManagment.ChargeModifier("Battery");
            chargeManagment.CVStatus.VoltChargeLevel = Mathf.Clamp
                (
                    chargeManagment.CVStatus.VoltChargeLevel,
                    0,
                    chargeManagment.CVStatus.MaxVoltCharge
                );
        }
    }
}