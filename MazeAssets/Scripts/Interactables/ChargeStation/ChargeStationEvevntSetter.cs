using UnityEngine;
using static ChargeStationStateEnum;
public class ChargeStationEventSetter : MonoBehaviour
{
    [SerializeField] ChargeStationEvent chargeStationEvent;
    [SerializeField] ChargeManagment chargeManagment;
    [SerializeField] string stationId;
    private float rechargeTimer = 0f;
    private bool isRecharging = false;

    private void Start()
    {
        stationId = name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var stationStatus = chargeManagment.GetStationStatus(stationId);
            if (stationStatus != null && chargeManagment.ChargeVoltStatus.VoltChargeLevel < chargeManagment.ChargeVoltStatus.MaxVoltCharge)
            {
                if (stationStatus.CurrentChargeLevel > 0)
                {
                    chargeStationEvent.ChargeStatus = ChargeStationStateEnum.Charging;
                }
                else
                {
                    chargeStationEvent.ChargeStatus = ChargeStationStateEnum.Empty;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && chargeStationEvent.ChargeStatus == ChargeStationStateEnum.Charging)
        {
            var stationStatus = chargeManagment.GetStationStatus(stationId);
            if (stationStatus != null && stationStatus.CurrentChargeLevel > 0 && chargeManagment.ChargeVoltStatus.VoltChargeLevel < chargeManagment.ChargeVoltStatus.MaxVoltCharge)
            {
                float chargeAmount = chargeManagment.ChargeStationProperties.ChargeRate * Time.deltaTime;
                chargeManagment.UpdateVoltCharge();
                stationStatus.CurrentChargeLevel = Mathf.Max(stationStatus.CurrentChargeLevel - chargeAmount, 0);

                if (chargeManagment.ChargeVoltStatus.VoltChargeLevel >= chargeManagment.ChargeVoltStatus.MaxVoltCharge)
                {
                    chargeStationEvent.ChargeStatus = ChargeStationStateEnum.FullyCharged;
                }
                else if (stationStatus.CurrentChargeLevel <= 0)
                {
                    chargeStationEvent.ChargeStatus = ChargeStationStateEnum.Empty;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chargeStationEvent.ChargeStatus =   ChargeStationStateEnum.Empty;
            isRecharging = true;
            rechargeTimer = 0f;
        }
    }

    private void Update()
    {
        if (isRecharging)
        {
            var stationStatus = chargeManagment.GetStationStatus(stationId);
            if (stationStatus != null)
            {
                rechargeTimer += Time.deltaTime;
                if (rechargeTimer >= chargeManagment.ChargeStationProperties.RechargeDelay)
                {
                    stationStatus.CurrentChargeLevel = Mathf.Min(stationStatus.CurrentChargeLevel + chargeManagment.ChargeStationProperties.RechargeRate * Time.deltaTime, chargeManagment.ChargeStationProperties.Capacity);
                    if (stationStatus.CurrentChargeLevel >= chargeManagment.ChargeStationProperties.Capacity)
                    {
                        isRecharging = false;
                        chargeStationEvent.ChargeStatus = ChargeStationStateEnum.FullyCharged;
                    }
                }
            }
        }
    }
}