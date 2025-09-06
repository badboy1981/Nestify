using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeManagment", menuName = "Charge Managment/Charge Managment")]
public class ChargeManagment : ScriptableObject
{
    [Header("Charge Station")]
    public List<ChargeStationStatus> ChargeStationStatus = new(4);
    public ChargeStationProperty ChargeStationProperties;
    [Header("Volt Charge")]
    public ChargeVoltStatus ChargeVoltStatus;
    [Header("Battery")]
    public BatteryProperty BatteryProperties;

    private Dictionary<string, ChargeStationStatus> stationStatusDict;

    public void Initialize()
    {
        stationStatusDict = new Dictionary<string, ChargeStationStatus>();
        foreach (var status in ChargeStationStatus)
        {
            if (!stationStatusDict.ContainsKey(status.StationID))
            {
                stationStatusDict.Add(status.StationID, status);
            }
        }
    }

    public ChargeStationStatus GetStationStatus(string id)
    {
        return stationStatusDict.TryGetValue(id, out var status) ? status : null;
    }

    public void UpdateVoltCharge(float amount)
    {
        ChargeVoltStatus.VoltChargeLevel = Mathf.Clamp(ChargeVoltStatus.VoltChargeLevel + amount, 0, ChargeVoltStatus.MaxVoltCharge);
    }
}