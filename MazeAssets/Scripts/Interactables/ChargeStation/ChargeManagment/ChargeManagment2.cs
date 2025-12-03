using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeManagment2", menuName = "Charge Managment/ChargeManagment2")]
public class ChargeManagment2 : ScriptableObject
{
    [Header("Charge Event")]
    public ChargeStationEvent chargeStationEvent;
    [Header("----------------------")]
    [Header("Volt Charge")]
    public ChargeVoltStatus CVStatus;
    public bool VoltInside;
    [Header("----------------------")]
    [Header("Battery")]
    public BatteryProperty battery;

    public IEnumerator DeChargeVoltRoutine()
    {
        while (CVStatus.VoltChargeLevel >= 0f)
        {
            if (VoltInside)
            {
                CVStatus.VoltChargeLevel++;
                CVStatus.VoltChargeLevel = Mathf.Clamp(CVStatus.VoltChargeLevel, 0, CVStatus.MaxVoltCharge);
                chargeStationEvent.VoltChargeStatus = CVStatus.UpdateChargeState();
                //if (CVStatus.VoltChargeLevel >= CVStatus.MaxVoltCharge)
                //{
                //    chargeStationEvent.ChargeStatus = ChargeStationStateEnum.VoltFullCharged;
                //    yield return null;
                //    continue;
                //}
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