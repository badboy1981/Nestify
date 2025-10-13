using UnityEngine;

public class ChargeManagmentReset : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;
    void Start()
    {
        ResetManagment();
    }
    private void ResetManagment()
    {
        chargeManagment.ChargeStationStatusList.Clear();

        //chargeManagment.ChargeSettings.duration = 20f;
        //chargeManagment.ChargeSettings.timer = 0;
        //chargeManagment.ChargeSettings.currentValue = 100f;
        //chargeManagment.ChargeSettings.maxValue = 100f;
        //chargeManagment.ChargeSettings.minValue = 0f;

        chargeManagment.ActiveChargeStation = null;

        chargeManagment.ChargeStationProperties.Capacity = 100f;
        chargeManagment.ChargeStationProperties.ChargeRate = 20f;
        chargeManagment.ChargeStationProperties.RechargeDelay = 5f;
        chargeManagment.ChargeStationProperties.RechargeRate = 1f;

        chargeManagment.ChargeVoltStatus.MaxVoltCharge = 100f;
        chargeManagment.ChargeVoltStatus.VoltChargeLevel = 100f;
        chargeManagment.ChargeVoltStatus.DeChargeRate = 1f;
        chargeManagment.ChargeVoltStatus.ChargeState = VoltChargeStateEnum.FullyCharged;
        chargeManagment.VoltInSide = false;

        chargeManagment.BatteryProperties.Capacity = 20f;
    }
}