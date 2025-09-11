using UnityEngine;
using static ChargeStationEvent;

internal class ChargeStationEventListener : MonoBehaviour
{
    [SerializeField] ChargeStationEvent chargeStationEvent;
    //[SerializeField] ChargeStation chargeStation;
    //private void Awake()
    //{
    //    chargeStationEvent ??= chargeStationEvent = new();
    //}
    private void OnEnable()
    {
        if (chargeStationEvent == null)
        {
            //Debug.LogError("ChargeStationEvent is not assigned in the inspector.");
            return;
        }
        chargeStationEvent.OnStatusChanged += OnStatusChanged;
    }

    private void OnDisable()
    {
        chargeStationEvent.OnStatusChanged -= OnStatusChanged;
    }

    private void OnStatusChanged(ChargeStationState status)
    {
        Debug.Log($"ChargeStationEventListener: Charge status changed to {status}||{gameObject.name}");
        var chargeStation = GetComponent<ChargeStation>();
        chargeStation.HandleChargeState(status);
    }
}