using UnityEngine;
using UnityEngine.Events;
//using static ChargeStationStateEnum;

[CreateAssetMenu(fileName = "ChargeStationEventHandler", menuName = "Audio Maze/Charge Station Event Handler")]
internal class ChargeStationEvent : ScriptableObject
{
    [SerializeField] ChargeManagment chargeManagment;
    private ChargeStationStateEnum _chargeStatus;
    private VoltChargeStateEnum _VoltChargeChange;


    //[System.NonSerialized]
    //internal UnityAction<ChargeStationStateEnum> OnStatusChanged;
    internal UnityAction<ChargeStationStateEnum> OnChargeStationStatusChanged;
    internal UnityAction<VoltChargeStateEnum> OnVoltChargeStatus;

    internal ChargeStationStateEnum ChargeStatus
    {
        get => _chargeStatus;
        set
        {
            if (_chargeStatus == value) return;
            _chargeStatus = value;
            OnChargeStationStatusChanged?.Invoke(_chargeStatus);
        }
    }
    internal VoltChargeStateEnum VoltChargeStatus
    {
        get => _VoltChargeChange; set
        {
            if (_VoltChargeChange == value) return;
            _VoltChargeChange = value;
            OnVoltChargeStatus?.Invoke(_VoltChargeChange);
        }
    }
    internal ChargeStationStateEnum SelectState(string stationId)
    {
        var stationStatus = chargeManagment.GetStationStatus(stationId);
        if (stationStatus != null &&
            chargeManagment.ChargeVoltStatus.VoltChargeLevel <
            chargeManagment.ChargeVoltStatus.MaxVoltCharge)
        {
            if (stationStatus.CurrentChargeLevel > 0)
            {
                //Debug.Log($"Charge State: Charging");
                ChargeStatus = ChargeStationStateEnum.Charging;
            }
            else
            {
                //Debug.Log($"Charge State: Station Is Empty!");
                ChargeStatus = ChargeStationStateEnum.Empty;
            }
        }
        else if
            (
            chargeManagment.ChargeVoltStatus.VoltChargeLevel ==
            chargeManagment.ChargeVoltStatus.MaxVoltCharge
            )
        {
            //Debug.Log("ChargeStationEvent: Volt is fully charged.");
            ChargeStatus = ChargeStationStateEnum.FullyCharged;
        }
        else if (chargeManagment.GetStationStatus(stationId).CurrentChargeLevel == 0)
        {
            //Debug.Log("ChargeStationEvent: Station is empty.");
            ChargeStatus = ChargeStationStateEnum.Depleted;
        }
        else
        {
            Debug.LogWarning("ChargeStationEvent: Unable to determine state.");
        }
        return ChargeStatus;
    }
}