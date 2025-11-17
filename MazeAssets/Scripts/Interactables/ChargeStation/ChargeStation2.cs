using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChargeStation2 : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;

    private Coroutine chargingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //0
        chargeManagment.VoltInSide = true;

        //VoltChargeStateChange(VoltChargeStateEnum.StopDrain);        
        chargeManagment.UpdateVoltChargeState(VoltChargeStateEnum.StopDrain);
        //chargeManagment.UpdateChargeStationState(ChargeStationStateEnum.Charging);

        AddActiveStation();
        if (TimeSinceExit())
        {
            chargeManagment.ActiveChargeStation.CurrentChargeLevel =
                chargeManagment.ChargeStationProperties.Capacity;
        }

        if (chargeManagment.CheckChargeStationCharge())
        {
            //ChargeStationChangeState(ChargeStationStateEnum.HasCharge);
            chargeManagment.UpdateChargeStationState(ChargeStationStateEnum.HasCharge);
        }
        else
        {
            //ChargeStationChangeState(ChargeStationStateEnum.NoCharge);
            chargeManagment.UpdateChargeStationState(ChargeStationStateEnum.NoCharge);
        }
    }
    private void AddActiveStation()
    {
        var existingStation = chargeManagment.ChargeStationStatusList
            .Find(s => s.StationID == name);

        if (existingStation == null)
        {
            chargeManagment.ActiveChargeStation = StationStatus();
            chargeManagment.ChargeStationStatusList.Add(chargeManagment.ActiveChargeStation);
            chargeManagment.InitChargeStationProperty();
        }
        else
        {
            chargeManagment.ActiveChargeStation = existingStation;
        }
    }
    private ChargeStationStatus StationStatus()
    {
        return new()
        {
            StationID = name,
            CurrentChargeLevel = chargeManagment.ChargeStationProperties.Capacity,
            ChargeStationState = ChargeStationStateEnum.VoltEnter
        };
    }
    private bool ChargeStatusChack()
    {
        var conditions = new List<Func<bool>>
        {
            () => chargeManagment.ActiveChargeStation.CurrentChargeLevel > 0,
            () => chargeManagment.ChargeVoltStatus.VoltChargeLevel < chargeManagment.ChargeVoltStatus.MaxVoltCharge,
            () => chargeManagment.VoltInSide
        };
        bool inSide = chargeManagment.VoltInSide;
        return inSide && conditions.Any(condition => condition());
    }

    private void OnTriggerStay(Collider other)
    {
        if (!ChargeStatusChack()) return;

        //if (!chargeManagment.VoltInSide) return;
        //if (!ChargeStatusChack()) return;
        //if (chargeManagment.ActiveChargeStation.State == ChargeStationStateEnum.HasCharge)
        //{
        //    if (chargeManagment.ChargeVoltStatus.VoltChargeLevel ==
        //        chargeManagment.ChargeVoltStatus.MaxVoltCharge)
        //    {
        //        ChargeStationChangeState(ChargeStationStateEnum.VoltFullCharged);
        //    }
        //    else
        //    {
        //        ChargeStationChangeState(ChargeStationStateEnum.HasCharge);
        //        chargeManagment.ChargeStationProperties.timer.currentValue =
        //            chargeManagment.ChargeStationProperties.timer.CalCurrentValue();
        ChargeProce();
        //    }
        //}
        //else
        //{
        //    ChargeStationChangeState(ChargeStationStateEnum.NoCharge);
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        //if (!chargeManagment.VoltInSide) return;

        //VoltChargeStateChange(VoltChargeStateEnum.Charging);
        chargeManagment.UpdateVoltChargeState(VoltChargeStateEnum.Charging);

        UpdateExitTime();

        chargeManagment.VoltInSide = false;
        //ChargeStationChangeState(ChargeStationStateEnum.VoltExit);
        chargeManagment.UpdateChargeStationState(ChargeStationStateEnum.VoltExit);
        if (chargingCoroutine != null)
        {
            StopCoroutine(chargingCoroutine);
        }
        chargingCoroutine = StartCoroutine(chargeManagment.RechargeStation());
        chargingCoroutine = null;
    }
    private void UpdateExitTime()
    {
        if (TimeSinceExit())
        {
            chargeManagment.ActiveChargeStation.LastExitTime = Time.time;
        }
    }
    private bool TimeSinceExit()
    {
        return (Time.time - chargeManagment.ActiveChargeStation.LastExitTime) >=
            chargeManagment.ChargeStationProperties.RechargeDelay;
    }
    //private void ChargeStationChangeState(ChargeStationStateEnum State)
    //{
    //    //chargeManagment.ActiveChargeStation.State = State;
    //    //chargeStationEvent.ChargeStatus = State;
    //    chargeManagment.chargeStationEvent.ChargeStatus = State;        
    //}
    //private void VoltChargeStateChange(VoltChargeStateEnum State)
    //{
    //    chargeManagment.chargeStationEvent.VoltChargeStatus = State;
    //    chargeManagment.ChargeVoltStatus.VoltChargeState = State;
    //}

    private void ChargeProce()
    {
        //Debug.Log($"");
        //if (chargeManagment.ActiveChargeStation.CurrentChargeLevel < chargeManagment.ChargeStationProperties.Capacity)
        //{
        if (chargeManagment.ChargeStationProperties.timer.timer <=
            chargeManagment.ChargeStationProperties.timer.minValue)
        {
            chargeManagment.ChargeStationProperties.timer.timer =
                chargeManagment.ChargeStationProperties.timer.duration;
            chargeManagment.ChargeStationProperties.timer.currentValue =
                chargeManagment.ChargeStationProperties.timer.minValue;
        }
        chargeManagment.ActiveChargeStation.CurrentChargeLevel =
        chargeManagment.ChargeStationProperties.timer.CalCurrentValue();

        chargeManagment.ChargeStationProperties.timer.currentValue =
                chargeManagment.ActiveChargeStation.CurrentChargeLevel;
        //}
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