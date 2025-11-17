using System;

[Serializable]
public class ChargeStationProperty
{
    public float Capacity; // Maximum charge capacity
    public float ChargeRate; // Charge transfer rate per second
    public float RechargeDelay; // Delay before recharging starts (seconds)
    public float RechargeRate; // Charge Station Recharge rate per second
    public MazeCore.MazeTimer timer;
}