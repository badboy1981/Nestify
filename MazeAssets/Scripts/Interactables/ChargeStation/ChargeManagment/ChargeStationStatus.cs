using System;

[Serializable]
public class ChargeStationStatus
{
    public string StationID;
    public float CurrentChargeLevel; // Current charge level for the station
    public float LastExitTime;
    //public ChargeStationStateEnum ChargeStationState;

    public ChargeStationStateEnum UpdateVoltChargeState(float capacity)
    {
        if (CurrentChargeLevel <= 0)
            return ChargeStationStateEnum.NoCharge;
        else if (CurrentChargeLevel <= 0.3f * capacity)
            return ChargeStationStateEnum.Partial;
        else if (CurrentChargeLevel < capacity)
            return ChargeStationStateEnum.HasCharge;
        else if (CurrentChargeLevel == capacity)
            return ChargeStationStateEnum.Full;
        else
            return ChargeStationStateEnum.Depleted;
    }
}