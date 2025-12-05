using System;

[Serializable]
public class ChargeVoltStatus
{
    public float VoltChargeLevel;    // Current charge level for Volt
    public float MaxVoltCharge;      // Maximum charge capacity for Volt
    public float DeChargeRate;       // Rate at which Volt drains per second
    public float ChargeDelaySeconds; // Delay Step 

    /// <summary>
    /// Determines the current charge state of Volt based on its charge level.
    /// Evaluates VoltChargeLevel against MaxVoltCharge thresholds and returns
    /// the corresponding VoltChargeStateEnum (Empty, Emergency, Critical, Low,
    /// Partial, Normal, or FullyCharged).
    /// </summary>

    public VoltChargeStateEnum UpdateChargeState()
    {
        if (VoltChargeLevel <= 0f)
            return VoltChargeStateEnum.Empty;

        else if (VoltChargeLevel < MaxVoltCharge * 0.05f)
            return VoltChargeStateEnum.Emergency;

        else if (VoltChargeLevel < MaxVoltCharge * 0.1f)
            return VoltChargeStateEnum.Critical;

        else if (VoltChargeLevel < MaxVoltCharge * 0.2f)
            return VoltChargeStateEnum.Low;

        else if (VoltChargeLevel < MaxVoltCharge * 0.3f)
            return VoltChargeStateEnum.Partial;

        else if (VoltChargeLevel < MaxVoltCharge)
            return VoltChargeStateEnum.Normal;

        return VoltChargeStateEnum.FullyCharged;
    }
}