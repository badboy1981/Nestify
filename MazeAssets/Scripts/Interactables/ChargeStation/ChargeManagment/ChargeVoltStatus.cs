using System;

[Serializable]
public class ChargeVoltStatus
{
    public float VoltChargeLevel; // Current charge level for Volt
    public float MaxVoltCharge; // Maximum charge capacity for Volt
    public float DeChargeRate; // Rate at which Volt drains per second
    public float ChargeDelaySeconds; // Delay Step 
    //public VoltChargeStateEnum VoltChargeState;
    public VoltChargeStateEnum UpdateChargeState()
    {
        if (VoltChargeLevel <= 0f)
            return VoltChargeStateEnum.Empty;
        else if (VoltChargeLevel < MaxVoltCharge * 0.3f)
            return VoltChargeStateEnum.Partial;
        else if (VoltChargeLevel < MaxVoltCharge)
            return VoltChargeStateEnum.Charging;
        else
            return VoltChargeStateEnum.FullyCharged;
    }
}