using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeManagment", menuName = "Charge Managment/ChargeManagment")]
public class ChargeManagment : ScriptableObject
{
    [Header("Charge Event")]
    public ChargeStationEvent chargeStationEvent;
    [Header("----------------------")]
    [Header("Volt Charge Status")]
    public bool voltInsideStation;
    public ChargeVoltStatus CVStatus;
    [Header("----------------------")]
    [Header("Battery")]
    public BatteryProperty battery;

    public IEnumerator DeChargeVoltRoutine()
    {
        while (CVStatus.VoltChargeLevel >= 0f)
        {
            if (voltInsideStation)
            {
                if (CVStatus.VoltChargeLevel >= CVStatus.MaxVoltCharge)
                {
                    chargeStationEvent.VoltChargeStatus = VoltChargeStateEnum.FullyCharged;
                }
                else
                {
                    chargeStationEvent.VoltChargeStatus = VoltChargeStateEnum.Charging; // Volt is charging
                    CVStatus.VoltChargeLevel++;
                    CVStatus.VoltChargeLevel = Mathf.Clamp(CVStatus.VoltChargeLevel, 0, CVStatus.MaxVoltCharge);
                }
                yield return new WaitForSeconds(CVStatus.ChargeDelaySeconds / 2);
                continue;
            }
            CVStatus.VoltChargeLevel -= CVStatus.DeChargeRate;
            CVStatus.VoltChargeLevel =
                Mathf.Clamp(CVStatus.VoltChargeLevel,
                0f,
                CVStatus.MaxVoltCharge);

            chargeStationEvent.VoltChargeStatus = CVStatus.UpdateChargeState();
            yield return new WaitForSeconds(CVStatus.ChargeDelaySeconds);
        }
    }
}