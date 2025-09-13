using UnityEngine;

public class VoltCharge : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;
    [SerializeField] float drainRate = 20f;
    [SerializeField] float batteryLevel;

    private void Start()
    {
        batteryLevel = chargeManagment.ChargeVoltStatus.MaxVoltCharge;

    }
    private void Update()
    {
        DisCharge1();
    }
    private void DisCharge1()
    {
        if (!chargeManagment.VoltInSide)
        {
            batteryLevel -= drainRate * Time.deltaTime;
            batteryLevel = Mathf.Clamp(batteryLevel, 0f, chargeManagment.ChargeVoltStatus.MaxVoltCharge);
            chargeManagment.ChargeVoltStatus.VoltChargeLevel = batteryLevel;
        }
        else
        {
            batteryLevel = chargeManagment.ChargeVoltStatus.VoltChargeLevel;
        }
    }
    private void DisCharge2()
    {
        float duration = 10f;
        float timer = 0f;
        float currentValue = 100f;
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            currentValue = Mathf.Lerp(chargeManagment.ChargeVoltStatus.MaxVoltCharge, 0f, t);
        }
    }
}