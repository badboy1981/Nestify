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
        chargeManagment.ChargeStationStatus.Clear();

        chargeManagment.ChargeStationProperties.Capacity = 100f;
        chargeManagment.ChargeStationProperties.Rate = 20f;
        chargeManagment.ChargeStationProperties.RechargeDelay = 5f;
        chargeManagment.ChargeStationProperties.RechargeRate = 10f;

        chargeManagment.ChargeVoltStatus.MaxVoltCharge = 100f;
        chargeManagment.ChargeVoltStatus.VoltChargeLevel = 0;

        chargeManagment.BatteryProperties.Capacity = 20f;
    }
}