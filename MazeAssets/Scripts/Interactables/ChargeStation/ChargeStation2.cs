using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChargeStation2 : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;

    //private Coroutine chargingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //0
        chargeManagment.VoltInSide = true;

        //VoltChargeStateChange(VoltChargeStateEnum.StopDrain);        
        //chargeManagment.UpdateVoltChargeState(VoltChargeStateEnum.StopDrain);
        //chargeManagment.UpdateChargeStationState(ChargeStationStateEnum.Charging);

        chargeManagment.ActiveChargeStation = ChargeStation3();

        //if (TimeSinceExit())
        //{
        //chargeManagment.ActiveChargeStation.CurrentChargeLevel =
        //    chargeManagment.ChargeStationProperties.Capacity;
        //}

        if (chargeManagment.CheckChargeStationCharge())
        {
            //ChargeStationChangeState(ChargeStationStateEnum.HasCharge);
            //chargeManagment.UpdateChargeStationState(ChargeStationStateEnum.HasCharge);
        }
        else
        {
            //ChargeStationChangeState(ChargeStationStateEnum.NoCharge);
            //chargeManagment.UpdateChargeStationState(ChargeStationStateEnum.NoCharge);
        }
    }
    private ChargeStationStatus ChargeStation3()
    {
        //var station = chargeManagment.ChargeStationStatusList.Find(s => s.StationID == name);
        var station = chargeManagment.GetStationStatus(name);
        if (station != null)
        {
            if (station.LastExitTime +
                      chargeManagment.ChargeStationProperties.RechargeDelay < Time.time)
            {
                station.CurrentChargeLevel =
                chargeManagment.ChargeStationProperties.Capacity;
            }
            return station;
        }
        return new()
        {
            StationID = name,
            CurrentChargeLevel = chargeManagment.ChargeStationProperties.Capacity,
            //ChargeStationState = ChargeStationStateEnum.VoltEnter
        };
    }
    private ChargeStationStatus ChargeStation()
    {
        return chargeManagment.ChargeStationStatusList
            .Find(s => s.StationID == name)
            ?? new()
            {
                StationID = name,
                CurrentChargeLevel = chargeManagment.ChargeStationProperties.Capacity,
                //ChargeStationState = ChargeStationStateEnum.VoltEnter
            };
    }
    private bool EvaluateBlocks(List<List<Func<bool>>> blocks)
    {
        return blocks.Any(block => block.All(cond => cond()));
    }
    private bool ChargeStatusChack2()
    {
        var block = new List<Func<bool>>
           {
              //() => chargeManagment.ActiveChargeStation.ChargeStationState == ChargeStationStateEnum.Full,
              //() => chargeManagment.ActiveChargeStation.ChargeStationState == ChargeStationStateEnum.HasCharge,
              //() => chargeManagment.ActiveChargeStation.ChargeStationState == ChargeStationStateEnum.Partial,
           };
        return chargeManagment.VoltInSide && block.Any(condition => condition());
    }
    private bool ChargeStatusChack()
    {
        // return chargeManagment.VoltInSide
        //&& (chargeManagment.ActiveChargeStation.CurrentChargeLevel > 0
        //&& chargeManagment.ChargeVoltStatus.VoltChargeLevel <
        //    chargeManagment.ChargeVoltStatus.MaxVoltCharge);

        var conditions = new List<Func<bool>>
        {
            () => chargeManagment.VoltInSide,
            () => chargeManagment.ActiveChargeStation.CurrentChargeLevel > 0,
            () => chargeManagment.ChargeVoltStatus.VoltChargeLevel <
                  chargeManagment.ChargeVoltStatus.MaxVoltCharge,
        };
        //return chargeManagment.VoltInSide && conditions.Any(condition => condition());
        return conditions.All(condition => condition());
    }

    private void OnTriggerStay(Collider other)
    {
        if (ChargeStatusChack2())
        {
            ChargeProce2();
            //chargeManagment.ActiveChargeStation.UpdateVoltChargeState(chargeManagment.ChargeStationProperties.Capacity);
        }
    }
    //private float ChargeAmount;
    //private float DeChargeAmount;
    private void ChargeProce2()
    {
        if (chargeManagment.ChargeVoltStatus.VoltChargeLevel >=
            chargeManagment.ChargeVoltStatus.MaxVoltCharge)
            return;

        chargeManagment.ActiveChargeStation.CurrentChargeLevel =
        chargeManagment.ChargeStationProperties.Charge(chargeManagment.ActiveChargeStation.CurrentChargeLevel);

        //chargeManagment.ChargeVoltStatus.VoltChargeLevel =
    }
    private void ChargeProce()
    {        
        //ChargeAmount = chargeManagment.ChargeStationProperties.Charge(chargeManagment.ActiveChargeStation.CurrentChargeLevel);
        //DeChargeAmount = 100 - ChargeAmount;
        //chargeManagment.ActiveChargeStation.CurrentChargeLevel = ChargeAmount;
        //chargeManagment.ChargeVoltStatus.ChargeVolt(DeChargeAmount);
        //chargeManagment.ActiveChargeStation.UpdateVoltChargeState(chargeManagment.ChargeStationProperties.Capacity);
    }
    private void OnTriggerExit(Collider other)
    {
        //if (!chargeManagment.VoltInSide) return;

        //VoltChargeStateChange(VoltChargeStateEnum.Charging);
        chargeManagment.UpdateVoltChargeState(VoltChargeStateEnum.Normal);

        UpdateExitTime();


        chargeManagment.VoltInSide = false;
        //ChargeStationChangeState(ChargeStationStateEnum.VoltExit);
        //chargeManagment.UpdateChargeStationState(ChargeStationStateEnum.VoltExit);
        //if (chargingCoroutine != null)
        //{
        //    StopCoroutine(chargingCoroutine);
        //}
        ////chargingCoroutine = StartCoroutine(chargeManagment.RechargeStation());
        //chargingCoroutine = null;
    }
    private void UpdateExitTime()
    {
        var Station = chargeManagment.ChargeStationStatusList.Find(s => s.StationID == name);
        if (Station == null)
        {
            if (!string.IsNullOrEmpty(chargeManagment.ActiveChargeStation.StationID))
            {
                chargeManagment.ActiveChargeStation.LastExitTime = Time.time;
                chargeManagment.ChargeStationStatusList.Add(chargeManagment.ActiveChargeStation);
            }
        }
        else
        {
            if (TimeSinceExit())
            {
                Station.LastExitTime = Time.time;
                //Station.CurrentChargeLevel = chargeManagment.ChargeStationProperties.Capacity;
            }
        }
    }
    private void UpdateExitTime1()
    {
        bool timeSinceExit = (Time.time - chargeManagment.ActiveChargeStation.LastExitTime) >=
            chargeManagment.ChargeStationProperties.RechargeDelay;
        if (timeSinceExit)
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



    private IEnumerator DischargeBattery()
    {
        var currentChargeLevel = chargeManagment.ActiveChargeStation?.CurrentChargeLevel ?? 0f;
        while (currentChargeLevel > 0f)
        {
            currentChargeLevel -= chargeManagment.ChargeStationProperties.ChargeRate;
            yield return new WaitForSeconds(chargeManagment.ChargeStationProperties.RechargeRate);
        }
        chargeManagment.ActiveChargeStation.CurrentChargeLevel = currentChargeLevel;
        //chargingCoroutine = null;
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
        //chargingCoroutine = null;
    }
}