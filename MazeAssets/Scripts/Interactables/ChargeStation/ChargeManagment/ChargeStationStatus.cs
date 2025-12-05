using System;

[Serializable]
public class ChargeStationStatus
{
    public string StationID;              // Unique identifier for the station
    public float CurrentChargeLevel;      // Current charge level of the station
    public float MaxCapacity;             // Maximum charge capacity of the station
    public float LastExitTime;            // Last time Volt exited the station


    /// <summary>
    /// Evaluates the current state of the charge station based on its charge level.
    /// </summary>

    public ChargeStationStateEnum UpdateStationState(bool isCharging, bool isDisabled)
    {
        if (isDisabled)
            return ChargeStationStateEnum.Disabled;

        if (CurrentChargeLevel <= 0f)
            return ChargeStationStateEnum.Empty;

        if (CurrentChargeLevel > MaxCapacity)
            return ChargeStationStateEnum.Overloaded;

        if (isCharging)
            return ChargeStationStateEnum.Charging;

        // Capacity-based ranges
        if (CurrentChargeLevel < MaxCapacity * 0.1f)
            return ChargeStationStateEnum.Critical;

        if (CurrentChargeLevel < MaxCapacity * 0.3f)
            return ChargeStationStateEnum.Low;

        if (CurrentChargeLevel < MaxCapacity * 0.8f)
            return ChargeStationStateEnum.Normal;

        if (CurrentChargeLevel < MaxCapacity)
            return ChargeStationStateEnum.High;

        if (CurrentChargeLevel == MaxCapacity)
            return ChargeStationStateEnum.Full;

        return ChargeStationStateEnum.Idle; // Default fallback
    }
    // Note: 'Available' should be used when the station has charge but no Volt is connected yet.
    // Note: 'CoolingDown' should be used when the station needs a recovery period after heavy use or full charge.
}