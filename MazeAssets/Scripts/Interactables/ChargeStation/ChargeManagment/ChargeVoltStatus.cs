using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ChargeVoltStatus
{
    public float VoltChargeLevel; // Current charge level for Volt
    public float MaxVoltCharge; // Maximum charge capacity for Volt
    public float DeChargeRate; // Rate at which Volt drains per second
    public float ChargeDelaySeconds; // Delay Step 
    public VoltChargeStateEnum VoltChargeState;

    public IEnumerator DeChargeVoltRoutine()
    {
        while (VoltChargeLevel > 0f)
        {
            if (VoltChargeState == VoltChargeStateEnum.StopDrain)
            {
                yield return null;
                continue;
            }
            //VoltChargeLevel -= DeChargeRate * Time.deltaTime;
            VoltChargeLevel -= DeChargeRate;
            VoltChargeLevel =
                Mathf.Clamp(VoltChargeLevel,
                0f,
                MaxVoltCharge);

            UpdateChargeState();
            yield return new WaitForSeconds(ChargeDelaySeconds);
        }
        VoltChargeState = VoltChargeStateEnum.Empty;
    }
    private void UpdateChargeState()
    {
        if (VoltChargeLevel <= 0f)
            VoltChargeState = VoltChargeStateEnum.Empty;
        else if (VoltChargeLevel < MaxVoltCharge * 0.3f)
            VoltChargeState = VoltChargeStateEnum.Partial;
        else if (VoltChargeLevel < MaxVoltCharge)
            VoltChargeState = VoltChargeStateEnum.Charging;
        else
            VoltChargeState = VoltChargeStateEnum.FullyCharged;
    }
}