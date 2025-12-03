using System;
using UnityEngine.Events;
//using static ChargeStationStateEnum;

//[CreateAssetMenu(fileName = "ChargeStationEventHandler", menuName = "Audio Maze/Charge Station Event Handler")]
[Serializable]
public class ChargeStationEvent //: ScriptableObject
{
    public bool _voltInsideStation;
    public ChargeStationStateEnum _chargeStatus;
    public VoltChargeStateEnum _VoltChargeStatus;    

    //[System.NonSerialized]
    //internal UnityAction<ChargeStationStateEnum> OnStatusChanged;
    public UnityAction<bool> OnVoltInsideStation;
    public UnityAction<ChargeStationStateEnum> OnChargeStationStatus;
    public UnityAction<VoltChargeStateEnum> OnVoltChargeStatus;
    
    public bool VoltInsideStation
    {
        get => _voltInsideStation;
        set
        {
            if (_voltInsideStation == value) return;
            _voltInsideStation = value;
            OnVoltInsideStation?.Invoke(_voltInsideStation);
        }
    }
    public ChargeStationStateEnum ChargeStatus
    {
        get => _chargeStatus;
        set
        {
            if (_chargeStatus == value) return;
            _chargeStatus = value;
            OnChargeStationStatus?.Invoke(_chargeStatus);
        }
    }
    public VoltChargeStateEnum VoltChargeStatus
    {
        get => _VoltChargeStatus;
        set
        {
            if (_VoltChargeStatus == value) return;
            _VoltChargeStatus = value;
            OnVoltChargeStatus?.Invoke(_VoltChargeStatus);
        }
    }
}