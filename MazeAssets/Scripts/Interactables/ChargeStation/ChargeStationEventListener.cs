using UnityEngine;
using static ChargeStationEvent;

internal class ChargeStationEventListener : Interactive
{
    [SerializeField] private ChargeStationEvent chargeStationEvent;

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
                PlaySound("Charge"); // Looping charge sound
                break;
            case ChargeStationState.FullyCharged:
                Debug.Log("Station or Volt is fully charged.");
                StopSound("Charge");
                PlaySound("ChargeFull"); // One-shot sound
                break;
            case ChargeStationState.Empty:
                Debug.Log("Station is empty.");
                StopSound("Charge");
                PlaySound("Empty"); // One-shot sound or silence
                break;
            case ChargeStationState.Depleted:
                Debug.Log("Volt is depleted.");
                StopSound("Charge");
                PlaySound("Depleted"); // One-shot sound for Volt depletion
                break;
        }
    }
}