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

        chargeManagment.CVStatus.VoltChargeLevel = 100f;
        chargeManagment.CVStatus.MaxVoltCharge = 100f;
        chargeManagment.CVStatus.DeChargeRate = 1f;
        chargeManagment.CVStatus.ChargeDelaySeconds = 1f;
        //chargeManagment.CVStatus.VoltChargeState = VoltChargeStateEnum.FullyCharged;
    }
}
    //private void ResetManagment()
    //{
    //    chargeManagment.ChargeStationStatusList.Clear();


    //    chargeManagment.ActiveChargeStation = null;

    //    chargeManagment.ChargeStationProperties.Capacity = 100f;
    //    chargeManagment.ChargeStationProperties.ChargeRate = 1f;
    //    chargeManagment.ChargeStationProperties.ChargeInterval = 0.5f;
    //    chargeManagment.ChargeStationProperties.RechargeDelay = 5f;
    //    chargeManagment.ChargeStationProperties.RechargeRate = 1f;

    //    chargeManagment.ChargeVoltStatus.MaxVoltCharge = 100f;
    //    chargeManagment.ChargeVoltStatus.VoltChargeLevel = 100f;
    //    chargeManagment.ChargeVoltStatus.DeChargeRate = 1f;
    //    chargeManagment.ChargeVoltStatus.ChargeDelaySeconds = 1f;
    //    chargeManagment.ChargeVoltStatus.VoltChargeState = VoltChargeStateEnum.FullyCharged;
    //    chargeManagment.VoltInSide = false;

    //    chargeManagment.BatteryProperties.Capacity = 20f;
    //}
//}