using UnityEngine.Events;

internal class ChargeStationEvent
{
    private ChargeStationState _chargeStatus;
    internal enum ChargeStationState
    {
        Charging,
        FullyCharged,
        Empty
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
}