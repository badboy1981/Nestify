using UnityEngine;

public class VoltCharge : MonoBehaviour
{
    [SerializeField] private ChargeManagment chargeManagment;

    private void Start()
    {
        if (chargeManagment.ChargeVoltStatus.VoltChargeLevel == 0)
        {
            chargeManagment.ChargeVoltStatus.VoltChargeLevel = chargeManagment.ChargeVoltStatus.MaxVoltCharge; // Initialize to full charge
        }
    }
}