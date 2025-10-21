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

        chargeManagment.ChargeStationProperties.timer.duration = 20f;
        chargeManagment.ChargeStationProperties.timer.timer = 0;
        chargeManagment.ChargeStationProperties.timer.currentValue = 100f;
        chargeManagment.ChargeStationProperties.timer.maxValue = 100f;
        chargeManagment.ChargeStationProperties.timer.minValue = 0f;

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