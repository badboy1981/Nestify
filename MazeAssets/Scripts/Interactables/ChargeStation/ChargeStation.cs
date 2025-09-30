using System.Collections;
using System.Linq;
using UnityEngine;

//using static ChargeStationEvent;
//using static ChargeStationStateEnum;

internal class ChargeStation : MonoBehaviour//Interactive
{
    //[SerializeField] BatteryDiagram _BatteryDiagram;
    [SerializeField] ChargeManagment chargeManagment;
    //[SerializeField] SaveLevelDataSObject PlayerData;
    //[SerializeField] float ChargerCapacity = 100f;
    //[SerializeField] float IncreaseValue = 10f;
    //[SerializeField] float WaitSeconds = 1f;
    //[SerializeField] float RechargeRate = 10f;
    //[SerializeField] float RechargeInterval = 10f;


    private Coroutine chargingCoroutine;
    [SerializeField] ChargeStationEvent chargeStationEvent;

    //[SerializeField] ChargeStationEventListener eventListener;

    // ChargeStationEventListener Block
    //internal void HandleChargeState(ChargeStationState state)
    //{
    //    Debug.Log($"ChargeStation: Handling state {state}");
    //}

    internal void HandleChargeState(ChargeStationStateEnum status)
    {
        switch (status)
        {
            case ChargeStationStateEnum.Charging:
                //Debug.Log("Station is now charging.");
                //PlaySound("Charge"); // Looping charge sound
                break;
            case ChargeStationStateEnum.FullyCharged:
                //Debug.Log("Station or Volt is fully charged.");
                //StopSound("Charge");
                //PlaySound("Charge");
                //PlaySound("ChargeFull"); // One-shot sound
                break;
            case ChargeStationStateEnum.Empty:
                //Debug.Log("Station is empty.");
                //StopSound("Charge");
                //PlaySound("Charge");
                //PlaySound("Empty"); // One-shot sound or silence
                break;
            case ChargeStationStateEnum.Depleted:
                //Debug.Log("Volt is depleted.");
                //StopSound("Charge");
                //PlaySound("Depleted"); // One-shot sound for Volt depletion
                break;
        }
    }
    //protected override void Awake()
    private void Awake()
    {
        //base.Awake();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chargeStationEvent.ChargeStatus = ChargeStationStateEnum.VoltEnter;
            var CsStatus = new ChargeStationStatus()
            {
                StationID = name,
                CurrentChargeLevel = chargeManagment.ChargeStationProperties.Capacity
            };
            if (!chargeManagment.ChargeStationStatusList.Any(s => s.StationID == name))
            {
                chargeManagment.ChargeStationStatusList.Add(CsStatus);
            }
            if (chargeManagment.ActiveChargeStation.StationID != name)
            {
                chargeManagment.ActiveChargeStation = CsStatus;
            }

            var stationStatus = chargeManagment.GetStationStatus(name);

            if (stationStatus != null &&
                chargeManagment.ChargeVoltStatus.VoltChargeLevel <
                chargeManagment.ChargeVoltStatus.MaxVoltCharge)
            {
                if (stationStatus.CurrentChargeLevel > 0)
                {
                    chargeStationEvent.ChargeStatus = ChargeStationStateEnum.Charging;
                    chargingCoroutine = StartCoroutine(DischargeBattery());
                }
                else
                {
                    chargeStationEvent.ChargeStatus = ChargeStationStateEnum.Empty;
                    if (chargingCoroutine != null)
                    {
                        StopCoroutine(chargingCoroutine);
                        chargingCoroutine = null;
                    }
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") &&
            chargeStationEvent.ChargeStatus ==
            ChargeStationStateEnum.Charging)
        {
            chargeStationEvent.ChargeStatus = ChargeStationStateEnum.Charging;
            var stationStatus = chargeManagment.GetStationStatus(name);
            if (stationStatus != null &&
                stationStatus.CurrentChargeLevel > 0 &&
                chargeManagment.ChargeVoltStatus.VoltChargeLevel <
                chargeManagment.ChargeVoltStatus.MaxVoltCharge)
            {
                float chargeAmount = chargeManagment.ChargeStationProperties.ChargeRate * Time.deltaTime;
                chargeManagment.UpdateVoltCharge();
                stationStatus.CurrentChargeLevel = Mathf.Max(stationStatus.CurrentChargeLevel - chargeAmount, 0);

                if (chargeManagment.ChargeVoltStatus.VoltChargeLevel >=
                    chargeManagment.ChargeVoltStatus.MaxVoltCharge)
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
        if (other.CompareTag("Player")) return;
        chargeStationEvent.ChargeStatus = ChargeStationStateEnum.VoltExit;

        if (chargingCoroutine != null)
        {
            StopCoroutine(chargingCoroutine);
        }
        chargingCoroutine = StartCoroutine(RechargeBattery());
        chargeStationEvent.ChargeStatus = ChargeStationStateEnum.Empty;
    }

    private IEnumerator DischargeBattery()
    {
        var currentChargeLevel = chargeManagment.ChargeStationStatusList.FirstOrDefault(c => c.StationID == name)?.CurrentChargeLevel ?? 0f;
        while (currentChargeLevel > 0f)
        {
            currentChargeLevel -= chargeManagment.ChargeStationProperties.ChargeRate;
            yield return new WaitForSeconds(chargeManagment.ChargeStationProperties.RechargeRate);
        }
        chargeManagment.ChargeStationStatusList.FirstOrDefault(c => c.StationID == name).CurrentChargeLevel = currentChargeLevel;

        chargingCoroutine = null;
    }
    private IEnumerator RechargeBattery()
    {
        var chargeStation = chargeManagment.ChargeStationStatusList.FirstOrDefault(c => c.StationID == name);
        while (chargeStation.CurrentChargeLevel < chargeManagment.ChargeStationProperties.Capacity)
        {
            chargeStation.CurrentChargeLevel = Mathf.Min(
                chargeStation.CurrentChargeLevel +
                chargeManagment.ChargeStationProperties.ChargeRate, chargeManagment.ChargeStationProperties.Capacity);

            yield return new WaitForSeconds(chargeManagment.ChargeStationProperties.RechargeRate);
        }
        chargingCoroutine = null;
    }
}