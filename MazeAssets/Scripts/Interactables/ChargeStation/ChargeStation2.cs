using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChargeStation2 : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;
    [SerializeField] ChargeStationEvent chargeStationEvent;
    [SerializeField] ChargeStationStatus chargeStationStatus;
    private Coroutine chargingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //0
        chargeManagment.VoltInSide = true;
        //1
        chargeStationStatus = new()
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
        }
        else
        {
            ChangeState(ChargeStationStateEnum.NoCharge);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (chargeManagment.CheckChargeStationCharge())
        {
            if (chargeManagment.ChargeVoltStatus.VoltChargeLevel ==
                chargeManagment.ChargeVoltStatus.MaxVoltCharge)
            {
                ChangeState(ChargeStationStateEnum.VoltFullCharged);
            }
            else
            {
                ChangeState(ChargeStationStateEnum.HasCharge);
                chargeManagment.UpdateVoltCharge();
                chargeManagment.DeChargeStation();
            }
        }
        else
        {
            ChangeState(ChargeStationStateEnum.NoCharge);
        }
    }
    private bool StayConditions(Collider other)
    {
        var conditions = new List<Func<bool>>
        {
            () => other.CompareTag("Player"),
            () => chargeManagment.CheckChargeStationCharge(),
        };
        return conditions.All(condition => condition());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chargeManagment.VoltInSide = false;
            ChangeState(ChargeStationStateEnum.VoltExit);
            if (chargingCoroutine != null)
            {
                StopCoroutine(chargingCoroutine);
            }
            chargingCoroutine = StartCoroutine(chargeManagment.RechargeStation());
            chargingCoroutine = null;
        }
    }
    private void ChangeState(ChargeStationStateEnum State)
    {
        chargeManagment.ActiveChargeStation.State = State;
        chargeStationEvent.ChargeStatus = State;
    }
    private IEnumerator DischargeBattery()
    {
        var currentChargeLevel = chargeManagment.ActiveChargeStation?.CurrentChargeLevel ?? 0f;
        while (currentChargeLevel > 0f)
        {
            currentChargeLevel -= chargeManagment.ChargeStationProperties.Rate;
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
                chargeManagment.ChargeStationProperties.Rate,
                chargeManagment.ChargeStationProperties.Capacity);

            yield return new WaitForSeconds(chargeManagment.ChargeStationProperties.RechargeRate);
        }
        chargingCoroutine = null;
    }
}