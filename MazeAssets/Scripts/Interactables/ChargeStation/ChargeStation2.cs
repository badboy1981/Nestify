using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChargeStation2 : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;
    [SerializeField] ChargeStationEvent chargeStationEvent;

    private Coroutine chargingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //0
        chargeManagment.VoltInSide = true;

        AddActiveStation();
        if (TimeSinceExit())
        {
            chargeManagment.ActiveChargeStation.CurrentChargeLevel =
                chargeManagment.ChargeStationProperties.Capacity;
        }

        if (chargeManagment.CheckChargeStationCharge())
        {
            ChangeState(ChargeStationStateEnum.HasCharge);
        }
        else
        {
            ChangeState(ChargeStationStateEnum.NoCharge);
        }
    }
    private void AddActiveStation()
    {
        var existingStation = chargeManagment.ChargeStationStatusList
            .Find(s => s.StationID == name);

        if (existingStation == null)
        {
            chargeManagment.ActiveChargeStation = stationStatus();
            chargeManagment.ChargeStationStatusList.Add(chargeManagment.ActiveChargeStation);
            chargeManagment.InitChargeStationProperty();
        }
        else
        {
            chargeManagment.ActiveChargeStation = existingStation;
        }
    }
    private ChargeStationStatus stationStatus()
    {
        return new()
        {
            StationID = name,
            CurrentChargeLevel = chargeManagment.ChargeStationProperties.Capacity,
            State = ChargeStationStateEnum.VoltEnter
        };
    }
    private bool InitCheck()
    {
        var conditions = new List<Func<bool>>
        {
         //() => chargeManagment.ChargeStationProperties.timer < chargeManagment.ChargeStationProperties.duration,
         //() => chargeManagment.ChargeStationProperties.timer > 0,
        };
        return conditions.All(condition => condition());
    }
    private void OnTriggerStay(Collider other)
    {
        if (!chargeManagment.VoltInSide) return;
        if (chargeManagment.ActiveChargeStation.State == ChargeStationStateEnum.HasCharge)
        {
            if (chargeManagment.ChargeVoltStatus.VoltChargeLevel ==
                chargeManagment.ChargeVoltStatus.MaxVoltCharge)
            {
                ChangeState(ChargeStationStateEnum.VoltFullCharged);
            }
            else
            {
                ChangeState(ChargeStationStateEnum.HasCharge);
                ChargeProce();
            }
        }
        else
        {
            ChangeState(ChargeStationStateEnum.NoCharge);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!chargeManagment.VoltInSide) return;

        ChangeExitTime();

        chargeManagment.VoltInSide = false;
        ChangeState(ChargeStationStateEnum.VoltExit);
        if (chargingCoroutine != null)
        {
            StopCoroutine(chargingCoroutine);
        }
        chargingCoroutine = StartCoroutine(chargeManagment.RechargeStation());
        chargingCoroutine = null;
    }
    private void ChangeExitTime()
    {
        if(TimeSinceExit())
        {
            chargeManagment.ActiveChargeStation.LastExitTime = Time.time;
        }
    }
    private bool TimeSinceExit()
    {
        return (Time.time - chargeManagment.ActiveChargeStation.LastExitTime) >=
            chargeManagment.ChargeStationProperties.RechargeDelay;
    }
    private void ChangeState(ChargeStationStateEnum State)
    {
        chargeManagment.ActiveChargeStation.State = State;
        chargeStationEvent.ChargeStatus = State;
    }


    private void ChargeProce()
    {
        if (chargeManagment.ActiveChargeStation.CurrentChargeLevel < chargeManagment.ChargeStationProperties.Capacity)
        {
            chargeManagment.ActiveChargeStation.CurrentChargeLevel = chargeManagment.ChargeStationProperties.timer.CalCurrentValue();
        }
    }
    private IEnumerator DischargeBattery()
    {
        var currentChargeLevel = chargeManagment.ActiveChargeStation?.CurrentChargeLevel ?? 0f;
        while (currentChargeLevel > 0f)
        {
            currentChargeLevel -= chargeManagment.ChargeStationProperties.ChargeRate;
            yield return new WaitForSeconds(chargeManagment.ChargeStationProperties.RechargeRate);
        }
        chargeManagment.ActiveChargeStation.CurrentChargeLevel = currentChargeLevel;
        chargingCoroutine = null;
    }
    private IEnumerator RechargeBattery()
    {
        var chargeStation = chargeManagment.ActiveChargeStation;
        while (chargeStation.CurrentChargeLevel < chargeManagment.ChargeStationProperties.Capacity)
        {
            chargeStation.CurrentChargeLevel = Mathf.Min(
                chargeStation.CurrentChargeLevel +
                chargeManagment.ChargeStationProperties.ChargeRate,
                chargeManagment.ChargeStationProperties.Capacity);

            yield return new WaitForSeconds(chargeManagment.ChargeStationProperties.RechargeRate);
        }
        chargingCoroutine = null;
    }
}