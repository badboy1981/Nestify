using System;
using UnityEngine;

[Serializable]
public class ChargeStationProperty
{
    public float Capacity; // Maximum charge capacity
    public float ChargeRate; // Charge transfer rate per second
    public float RechargeDelay; // Delay before recharging starts (seconds)
    public float RechargeRate; // Charge Station Recharge rate per second

    public float duration; // total time to deplete from full to empty
    public float timer; // current time left
    public float currentValue; // current charge level
    //public float maxValue;
    public float minValue; // usually 0
    private float t; // normalized time (0 to 1)

    public float ACalCurrentValue
    {
        get => currentValue;
        set => currentValue = value;
    }
    public void CalCurrentValue()
    {
        //if (timer < duration)
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, minValue, duration);
            t = Mathf.Clamp01(timer / duration);
            currentValue = (int)Mathf.Lerp(Capacity, minValue, t);
        }
    }
}