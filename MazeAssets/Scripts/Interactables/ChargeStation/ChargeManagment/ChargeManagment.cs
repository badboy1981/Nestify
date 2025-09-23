using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void UpdateVoltCharge()
    {
        ChargeVoltStatus.VoltChargeLevel = Mathf.Clamp
            (
              ChargeVoltStatus.VoltChargeLevel + RateAmount(),
              0,
              ChargeVoltStatus.MaxVoltCharge
            );
    }
    public void DeCahrgeVolt()
    {
        //ChargeVoltStatus.VoltChargeLevel = Mathf.Clamp
        //    (

        //    );
    }
    public void DeChargeStation()
    {
        if (ActiveChargeStation == null) return;
        ActiveChargeStation.CurrentChargeLevel =
            Mathf.Clamp
            (
                ActiveChargeStation.CurrentChargeLevel - RateAmount(),
                0,
                ChargeStationProperties.Capacity
            );
        //Mathf.Max(chargeManagment.ActiveChargeStation.CurrentChargeLevel - chargeAmount, 0);
    }
    private float RateAmount()
    {
        return ChargeStationProperties.Rate * Time.deltaTime;
    }
    public bool CheckChargeStationCharge()
    {
        if (ActiveChargeStation == null) return false;
        return ActiveChargeStation.CurrentChargeLevel > 0;
    }
    public IEnumerator RechargeStation()
    {
        yield return new WaitForSeconds(ChargeStationProperties.RechargeDelay);
        while (ActiveChargeStation.CurrentChargeLevel < ChargeStationProperties.Capacity)
        {
            //float rechargeAmount = ChargeStationProperties.RechargeRate * Time.deltaTime;
            ActiveChargeStation.CurrentChargeLevel =
                Mathf.Clamp(
                    ActiveChargeStation.CurrentChargeLevel +
                    ChargeStationProperties.RechargeRate,
                    0,
                    ChargeStationProperties.Capacity);
            yield return new WaitForSeconds(1f);
        }
    }
}