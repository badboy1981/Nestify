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
        //1
        ChargeStationStatus chargeStationStatus = new()
        {
            StationID = name,
            CurrentChargeLevel = chargeManagment.ChargeStationProperties.Capacity,
            State = ChargeStationStateEnum.VoltEnter
        };
        //2
        if (chargeManagment.ActiveChargeStation.StationID != name)
        {
            chargeManagment.ActiveChargeStation = chargeStationStatus;
        }
        if (chargeManagment.CheckChargeStationCharge())
        {
            ChangeState(ChargeStationStateEnum.HasCharge);
            InitChargrSetting();
        }
        else
        {
            ChangeState(ChargeStationStateEnum.NoCharge);
        }
    }
    private bool InitCheck()
    {
        var conditions = new List<Func<bool>>
        {
         //() => chargeManagment.ChargeSettings.timer < chargeManagment.ChargeSettings.duration,
         //() => chargeManagment.ChargeSettings.timer > 0,
        };
        return conditions.All(condition => condition());
    }
    private void InitChargrSetting()
    {
        if (!InitCheck())
        {
            //chargeManagment.ChargeSettings.timer = chargeManagment.ChargeSettings.duration;
            //chargeManagment.ChargeSettings.currentValue = 45f;// chargeManagment.ActiveChargeStation.CurrentChargeLevel;
        }
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
                //chargeManagment.UpdateVoltCharge();
                //chargeManagment.DeChargeStation();
                //chargeManagment.ChargeSettings.CalCurrentValue();
                //chargeManagment.ActiveChargeStation.CurrentChargeLevel = chargeManagment.ChargeSettings.currentValue;
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
        chargeManagment.VoltInSide = false;
        ChangeState(ChargeStationStateEnum.VoltExit);        
        if (chargingCoroutine != null)
        {
            StopCoroutine(chargingCoroutine);
        }
        chargingCoroutine = StartCoroutine(chargeManagment.RechargeStation());
        chargingCoroutine = null;

    }
    private void ChangeState(ChargeStationStateEnum State)
    {
        chargeManagment.ActiveChargeStation.State = State;
        chargeStationEvent.ChargeStatus = State;
    }
    private bool StayConditions()
    {
        var conditions = new List<Func<bool>>
        {
            //() => chargeManagment.CheckChargeStationCharge(),
            () => chargeManagment.ActiveChargeStation.State == ChargeStationStateEnum.HasCharge,
        };
        return conditions.All(condition => condition());
    }

    //private void ChargeProce()
    //{
    //    if (chargeManagment.ChargeSettings.timer < chargeManagment.ChargeSettings.duration)
    //    {
    //        chargeManagment.ChargeSettings.timer += Time.deltaTime;
    //        float t = Mathf.Clamp01(chargeManagment.ChargeSettings.timer / chargeManagment.ChargeSettings.duration);
    //        chargeManagment.ChargeSettings.currentValue = Mathf.Lerp(chargeManagment.ChargeSettings.maxValue, 0f, t);
    //        //Debug.Log($"Current Value: {chargeManagment.ChargeSettings.currentValue}");
    //    }
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