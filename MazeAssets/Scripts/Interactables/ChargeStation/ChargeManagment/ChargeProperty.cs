using System;
using UnityEngine;
[Serializable]
public class ChargeProperty
{
    public float duration;
    public float timer;
    public float currentValue;
    public float maxValue;
    public float minValue;
    private float t;

    public void CalCurrentValue()
    {
        //if (timer < duration)
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, minValue, duration);
            t = Mathf.Clamp01(timer / duration);
            currentValue = (int)Mathf.Lerp(maxValue, minValue, t);
        }
    }
}