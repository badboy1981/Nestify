using System.Collections;
using UnityEngine;

public class ChargeStation2 : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;
    [SerializeField] ChargeStationEvent chargeStationEvent;
    [SerializeField] ChargeStationStatus chargeStationStatus;
    private Coroutine chargingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
            if (CheckChargeStationCharge())
            {
                ChangeState(ChargeStationStateEnum.HasCharge);
            }
            else
            {
                ChangeState(ChargeStationStateEnum.NoCharge);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CheckChargeStationCharge())
            {
                if (chargeManagment.ChargeVoltStatus.VoltChargeLevel ==
                    chargeManagment.ChargeVoltStatus.MaxVoltCharge)
                {
                    ChangeState(ChargeStationStateEnum.VoltFullCharged);
                }
                else
                {
                    ChangeState(ChargeStationStateEnum.HasCharge);
                    float chargeAmount = chargeManagment.ChargeStationProperties.Rate * Time.deltaTime;
                    chargeManagment.UpdateVoltCharge(chargeAmount);
                    chargeManagment.ActiveChargeStation.CurrentChargeLevel = Mathf.Max(chargeManagment.ActiveChargeStation.CurrentChargeLevel - chargeAmount, 0);

                    //chargeManagment.ActiveChargeStation.CurrentChargeLevel -= 0.25f;
                    //chargeManagment.ChargeVoltStatus.VoltChargeLevel += 0.25f;
                }
            }
            else
            {
                ChangeState(ChargeStationStateEnum.NoCharge);
            }
            Debug.Log($"{other.name}: Stay Charge Station! {State()}");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chargeManagment.VoltInSide = false;
            ChangeState(ChargeStationStateEnum.VoltExit);            
            //Debug.Log($"{other.name}: Exit Charge Station! {State()}");
            if (chargingCoroutine != null)
            {
                StopCoroutine(chargingCoroutine);
            }
            chargingCoroutine = StartCoroutine(RechargeBattery());
        }
    }
    private void ChangeState(ChargeStationStateEnum State)
    {
        chargeManagment.ActiveChargeStation.State = State;
        chargeStationEvent.ChargeStatus = State;
    }
    private ChargeStationStateEnum State()
    {
        return new ChargeStationStateEnum();
    }
    private bool CheckChargeStationCharge()
    {
        if (chargeManagment.ActiveChargeStation.CurrentChargeLevel > 0) return true;
        return false;
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
                chargeManagment.ChargeStationProperties.Rate, chargeManagment.ChargeStationProperties.Capacity);

            yield return new WaitForSeconds(chargeManagment.ChargeStationProperties.RechargeRate);
        }
        chargingCoroutine = null;
    }
}