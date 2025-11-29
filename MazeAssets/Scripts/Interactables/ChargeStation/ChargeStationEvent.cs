using System;
using UnityEngine.Events;
//using static ChargeStationStateEnum;

//[CreateAssetMenu(fileName = "ChargeStationEventHandler", menuName = "Audio Maze/Charge Station Event Handler")]
[Serializable]
public class ChargeStationEvent //: ScriptableObject
{
    public ChargeStationStateEnum _chargeStatus;
    public VoltChargeStateEnum _VoltChargeChange;


    //[System.NonSerialized]
    //internal UnityAction<ChargeStationStateEnum> OnStatusChanged;
    public UnityAction<ChargeStationStateEnum> OnChargeStationStatusChanged;
    public UnityAction<VoltChargeStateEnum> OnVoltChargeStatus;

    public ChargeStationStateEnum ChargeStatus
    {
        get => _chargeStatus;
        set
        {
            if (_chargeStatus == value) return;
            _chargeStatus = value;
            OnChargeStationStatusChanged?.Invoke(_chargeStatus);
        }
    }
    public VoltChargeStateEnum VoltChargeStatus
    {
        get => _VoltChargeChange;
        set
        {
            if (_VoltChargeChange == value) return;
            _VoltChargeChange = value;
            OnVoltChargeStatus?.Invoke(_VoltChargeChange);
        }
    }
}