using UnityEngine;
using static ChargeStationEvent;

internal class ChargeStationEventListener : Interactive
{
    internal ChargeStationEvent chargeStationEvent;

    private void OnEnable()
    {
        chargeStationEvent.OnStatusChanged += OnStatusChanged;
    }

    private void OnDisable()
    {
        chargeStationEvent.OnStatusChanged -= OnStatusChanged;
    }

    private void OnStatusChanged(ChargeStationState status)
    {
        switch (status)
        {
            case ChargeStationState.Charging:
                Debug.Log("Station is now charging.");
                PlaySound("Charge");
                break;
            case ChargeStationState.FullyCharged:
                Debug.Log("Station is fully charged.");
                PlaySound("ChargeFull");
                break;
            case ChargeStationState.Empty:
                Debug.Log("Station is empty.");
                PlaySound("Empty");
                break;
        }
    }
}