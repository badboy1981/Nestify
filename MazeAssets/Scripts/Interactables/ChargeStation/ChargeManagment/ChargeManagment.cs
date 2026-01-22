using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeManagment", menuName = "Interactables/Charge Managment/ChargeManagment")]
public class ChargeManagment : ScriptableObject
{
    [Header("Charge Event")]
    public ChargeStationEvent chargeStationEvent;
    [Header("----------------------")]
    [Header("Volt Charge Status")]
    public bool voltInsideStation;
    public ChargeVoltStatus CVStatus;
    [Header("----------------------")]
    [Header("Charge Effect")]
    //public BatteryProperty battery;
    public ChargeEffect chargeEffect;

    public void ChargeModifier(string Tag)
    {
        switch (Tag)
        {
            case "BulkBot":
                CalculateChargeModify(-chargeEffect.BulkBot);
                break;
            case "ShadowBot":
                CalculateChargeModify(-chargeEffect.ShadowBot);
                break;
            case "Battery":
                CalculateChargeModify(chargeEffect.Battery);
                break;
            default:
                break;
        }
    }
    private void CalculateChargeModify(int value)
    {
        CVStatus.VoltChargeLevel = Mathf.Clamp
           (
               CVStatus.VoltChargeLevel += value,
               0,
               CVStatus.MaxVoltCharge
           );
    }
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