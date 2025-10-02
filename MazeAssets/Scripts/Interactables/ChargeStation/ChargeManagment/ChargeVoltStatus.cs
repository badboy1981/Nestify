using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ChargeVoltStatus
{
    public float VoltChargeLevel; // Current charge level for Volt
    public float MaxVoltCharge; // Maximum charge capacity for Volt
    public float DeChargeRate; // Rate at which Volt drains per second
    public VoltChargeStateEnum ChargeState;

    public IEnumerator DeChargeVoltRoutine()
    {
        while (VoltChargeLevel > 0f)
        {
            if(ChargeState == VoltChargeStateEnum.StopDrain)
            {
                yield return null;
                continue;
            }
            VoltChargeLevel -= DeChargeRate * Time.deltaTime;
            VoltChargeLevel =
                Mathf.Clamp(VoltChargeLevel,
                0f,
                MaxVoltCharge);

            UpdateChargeState();
            yield return null;
        }
        ChargeState = VoltChargeStateEnum.Empty;
    }
    private void UpdateChargeState()
    {
        if (VoltChargeLevel <= 0f)
            ChargeState = VoltChargeStateEnum.Empty;
        else if (VoltChargeLevel < MaxVoltCharge * 0.3f)
            ChargeState = VoltChargeStateEnum.Partial;
        else if (VoltChargeLevel < MaxVoltCharge)
            ChargeState = VoltChargeStateEnum.Charging;
        else
            ChargeState = VoltChargeStateEnum.FullyCharged;
    }
}