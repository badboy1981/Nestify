using UnityEngine;

public class ChargeManagmentReset : MonoBehaviour
{
    [SerializeField] ChargeManagment2 chargeManagment;
    void Start()
    {
        ResetManagment2();
    }
    private void ResetManagment2()
    {
        chargeManagment.chargeStationEvent.ChargeStatus = ChargeStationStateEnum.VoltExit;
        chargeManagment.chargeStationEvent.VoltChargeStatus = VoltChargeStateEnum.FullyCharged;
        chargeManagment.chargeStationEvent.VoltInsideStation = false;

        chargeManagment.CVStatus.VoltChargeLevel = 100f;
        chargeManagment.CVStatus.MaxVoltCharge = 100f;
        chargeManagment.CVStatus.DeChargeRate = 1f;
        chargeManagment.CVStatus.ChargeDelaySeconds = 1f;

        chargeManagment.battery.Capacity = 20f;
    }
}