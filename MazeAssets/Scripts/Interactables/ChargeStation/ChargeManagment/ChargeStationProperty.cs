using System;

[Serializable]
public class ChargeStationProperty 
{
    public float Capacity; // Maximum charge capacity
    public float Rate; // Charge transfer rate per second
    public float RechargeDelay; // Delay before recharging starts (seconds)
    public float RechargeRate; // Charge Station Recharge rate per second
}