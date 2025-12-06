using UnityEngine;

public class ChargeManagmentReset : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;
    void Start()
    {
        ResetManagment2();
    }
    private void ResetManagment2()
    {
        chargeManagment.chargeStationEvent.ChargeStatus = ChargeStationStateEnum.Available;
        chargeManagment.chargeStationEvent.VoltChargeStatus = VoltChargeStateEnum.FullyCharged;
        chargeManagment.voltInsideStation = false;

        chargeManagment.CVStatus.VoltChargeLevel = 100f;
        chargeManagment.CVStatus.MaxVoltCharge = 100f;
        chargeManagment.CVStatus.DeChargeRate = 1f;
        chargeManagment.CVStatus.ChargeDelaySeconds = 1f;

        chargeManagment.battery.Capacity = 20f;
    }
}