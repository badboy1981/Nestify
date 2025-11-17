using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ChargeStationStatus
{
    public string StationID;
    public float CurrentChargeLevel; // Current charge level for the station
    public float LastExitTime;
    public ChargeStationProperty ChargeStationProperties;
    public ChargeStationStateEnum ChargeStationState;

    private readonly WaitForSeconds _waitForSeconds = new(1f);
    //private float _PartialRange;


    public IEnumerator RechargeStation()
    {
        yield return new WaitForSeconds(ChargeStationProperties.RechargeDelay);
        while (CurrentChargeLevel < ChargeStationProperties.Capacity)
        {
            //float rechargeAmount = ChargeStationProperties.RechargeRate * Time.deltaTime;
            CurrentChargeLevel =
                Mathf.Clamp(
                    CurrentChargeLevel +
                    ChargeStationProperties.RechargeRate,
                    0,
                    ChargeStationProperties.Capacity);
            UpdateVoltChargeState();
            yield return _waitForSeconds;
        }
    }
    private void UpdateVoltChargeState()
    {
        if (CurrentChargeLevel <= 0)
            ChargeStationState = ChargeStationStateEnum.Empty;
        else if (CurrentChargeLevel <= 0.3f * ChargeStationProperties.Capacity)
            ChargeStationState = ChargeStationStateEnum.Partial;
        else if (CurrentChargeLevel < ChargeStationProperties.Capacity)
            ChargeStationState = ChargeStationStateEnum.HasCharge;
        else if (CurrentChargeLevel == ChargeStationProperties.Capacity)
            ChargeStationState = ChargeStationStateEnum.Full;
    }
}