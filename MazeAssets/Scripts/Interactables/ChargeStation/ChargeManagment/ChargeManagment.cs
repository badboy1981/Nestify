using System.Collections.Generic;
using UnityEngine;
using static ChargeStationEvent;

[CreateAssetMenu(fileName = "ChargeManagment", menuName = "Charge Managment/Charge Managment")]
public class ChargeManagment : ScriptableObject
{
    //[Header("ChargeStation Event Listener")]
    //[SerializeField] ChargeStationEventListener Listener;
    [Header("Charge Station")]
    public List<ChargeStationStatus> ChargeStationStatus;
    public ChargeStationStatus ActiveChargeStation;
    public ChargeStationProperty ChargeStationProperties;
    [Header("Volt Charge")]
    public ChargeVoltStatus ChargeVoltStatus;
    public bool VoltInSide;
    [Header("Battery")]
    public BatteryProperty BatteryProperties;

    public ChargeStationStatus GetStationStatus(string id)
    {
        return ChargeStationStatus.Find(s => s.StationID == id);
    }
    public void UpdateVoltCharge(float amount)
    {
        ChargeVoltStatus.VoltChargeLevel = Mathf.Clamp(ChargeVoltStatus.VoltChargeLevel + amount, 0, ChargeVoltStatus.MaxVoltCharge);
    }
}