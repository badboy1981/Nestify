using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChargeStationEventHandler", menuName = "Audio Maze/Charge Station Event Handler")]
internal class ChargeStationEvent : ScriptableObject
{
    [SerializeField] ChargeManagment chargeManagment;
    private ChargeStationState _chargeStatus;
    internal enum ChargeStationState
    {
        Charging,
        FullyCharged,
        Empty,
        Depleted // When Volt's charge reaches zero
    }

    [System.NonSerialized]
    internal UnityAction<ChargeStationState> OnStatusChanged;

    internal ChargeStationState ChargeStatus
    {
        get => _chargeStatus;
        set
        {
            if (_chargeStatus == value) return;
            _chargeStatus = value;
            OnStatusChanged?.Invoke(_chargeStatus);
        }
    }
    internal ChargeStationState SelectState(string stationId)
    {
        var stationStatus = chargeManagment.GetStationStatus(stationId);
        if (stationStatus != null &&
            chargeManagment.ChargeVoltStatus.VoltChargeLevel <
            chargeManagment.ChargeVoltStatus.MaxVoltCharge)
        {
            if (stationStatus.CurrentChargeLevel > 0)
            {
                //Debug.Log($"Charge State: Charging");
               ChargeStatus = ChargeStationState.Charging;
            }
            else
            {
                //Debug.Log($"Charge State: Station Is Empty!");
                ChargeStatus = ChargeStationState.Empty;
            }
        }
        else if
            (
            chargeManagment.ChargeVoltStatus.VoltChargeLevel ==
            chargeManagment.ChargeVoltStatus.MaxVoltCharge
            )
        {
            //Debug.Log("ChargeStationEvent: Volt is fully charged.");
            ChargeStatus = ChargeStationState.FullyCharged;
        }
        else if (chargeManagment.GetStationStatus(stationId).CurrentChargeLevel == 0)
        {
            //Debug.Log("ChargeStationEvent: Station is empty.");
            ChargeStatus = ChargeStationState.Depleted;
        }
        else
        {
            Debug.LogWarning("ChargeStationEvent: Unable to determine state.");
        }
        return ChargeStatus;
    }
}