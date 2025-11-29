using System;
using UnityEngine;

[Serializable]
public class ChargeStationProperty
{
    public float Capacity; // Maximum charge capacity
    public float ChargeRate; // Charge transfer rate per second
    public float ChargeInterval; // Interval for charge transfer (seconds)
    public float RechargeDelay; // Delay before recharging starts (seconds)
    public float RechargeRate; // Charge Station Recharge rate per second

    private float timer = 0f;

    public float Charge(float currentCharge)
    {
        timer += Time.deltaTime;
        if (timer >= ChargeInterval)
        {
            timer = 0;
            currentCharge -= ChargeRate;
            currentCharge = Mathf.Clamp(currentCharge, 0f, Capacity);
        }
        return currentCharge;
    }
}