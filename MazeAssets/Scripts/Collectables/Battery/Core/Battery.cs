using SaveSystem;
using UnityEngine;

namespace Collectable
{
    internal class Battery : Collectable
    {
        [SerializeField] SaveLevelDataSObject PlayerData;
        [SerializeField] ChargeManagment chargeManagment;
        //[SerializeField] BatteryDiagram _BatteryDiagram;
        //[SerializeField] float IncreaseValue = 20f;

        protected override void OnTriggerEnter(Collider other)
        {
            //_BatteryDiagram = GameObject.Find("BatteryDiagram").GetComponent<BatteryDiagram>();
            base.OnTriggerEnter(other);
            chargeManagment.ChargeVoltStatus.VoltChargeLevel += chargeManagment.BatteryProperties.Capacity;
            //_BatteryDiagram.IncreaseValue(IncreaseValue);
            //AddCharge(IncreaseValue);
            //PlayerData.ChargeStatus = IncreaseValue;
        }
    }
}