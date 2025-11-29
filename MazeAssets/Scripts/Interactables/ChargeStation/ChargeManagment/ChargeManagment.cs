using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeManagment", menuName = "Charge Managment/Charge Managment")]
public class ChargeManagment : ScriptableObject
{
    private static readonly WaitForSeconds _waitForSeconds1 = new(1f);

    //[Header("ChargeStation Event Listener")]
    //[SerializeField] ChargeStationEventListener Listener;
    //[Header("Charge Settings")]
    //public float time;
    [Header("Public ChargeStation Property")]
    [Header("ChargeStation Property")]
    public ChargeStationProperty ChargeStationProperties;
    [Header("----------------------")]
    [Header("Charge Event")]
    public ChargeStationEvent chargeStationEvent;
    [Header("----------------------")]
    [Header("Charge Station")]
    public List<ChargeStationStatus> ChargeStationStatusList;
    public ChargeStationStatus ActiveChargeStation;
    [Header("----------------------")]
    [Header("Volt Charge")]
    public ChargeVoltStatus ChargeVoltStatus;
    public bool VoltInSide;
    [Header("----------------------")]
    [Header("Battery")]
    public BatteryProperty BatteryProperties;

    public ChargeStationStatus GetStationStatus(string id)
    {
        return ChargeStationStatusList.Find(s => s.StationID == id);
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

    public void DrainVoltCharge()
    {
        //chargingCoroutine=
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
        return ChargeStationProperties.ChargeRate * Time.deltaTime;
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
            yield return _waitForSeconds1;
        }
    }
    public void UpdateVoltChargeState(VoltChargeStateEnum State)
    {
        chargeStationEvent.VoltChargeStatus = State;
        //ChargeVoltStatus.VoltChargeState = State;
    }
    public void UpdateChargeStationState(ChargeStationStateEnum State)
    {
        chargeStationEvent.ChargeStatus = State;
        //ActiveChargeStation.ChargeStationState = State;
    }
}