using System.Collections;
using System.Linq;
using SaveSystem;
using UnityEngine;
using static ChargeStationEvent;

internal class ChargeStation : Interactive
{
    //[SerializeField] BatteryDiagram _BatteryDiagram;
    //[SerializeField] ChargeManagment _ChargeManagment;
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
    internal void HandleChargeState(ChargeStationState state)
    {
        Debug.Log($"ChargeStation: Handling state {state}");
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!chargeManagment.ChargeStationStatus.Any(s => s.StationID == name))
            {
                chargeManagment.ChargeStationStatus.Add(new ChargeStationStatus
                {
                    StationID = name,
                    CurrentChargeLevel = chargeManagment.ChargeStationProperties.Capacity
                });
            }

            var stationStatus = chargeManagment.GetStationStatus(name);
            if (stationStatus != null && chargeManagment.ChargeVoltStatus.VoltChargeLevel < chargeManagment.ChargeVoltStatus.MaxVoltCharge)
            {
                if (stationStatus.CurrentChargeLevel > 0)
                {
                    chargeStationEvent.ChargeStatus = ChargeStationState.Charging;
                    chargingCoroutine = StartCoroutine(DischargeBattery());
                }
                else
                {
                    chargeStationEvent.ChargeStatus = ChargeStationState.Empty;
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
        if (other.CompareTag("Player") && chargeStationEvent.ChargeStatus == ChargeStationEvent.ChargeStationState.Charging)
        {
            var stationStatus = chargeManagment.GetStationStatus(name);
            if (stationStatus != null && stationStatus.CurrentChargeLevel > 0 && chargeManagment.ChargeVoltStatus.VoltChargeLevel < chargeManagment.ChargeVoltStatus.MaxVoltCharge)
            {
                float chargeAmount = chargeManagment.ChargeStationProperties.Rate * Time.deltaTime;
                chargeManagment.UpdateVoltCharge(chargeAmount);
                stationStatus.CurrentChargeLevel = Mathf.Max(stationStatus.CurrentChargeLevel - chargeAmount, 0);

                if (chargeManagment.ChargeVoltStatus.VoltChargeLevel >= chargeManagment.ChargeVoltStatus.MaxVoltCharge)
                {
                    chargeStationEvent.ChargeStatus = ChargeStationEvent.ChargeStationState.FullyCharged;
                }
                else if (stationStatus.CurrentChargeLevel <= 0)
                {
                    chargeStationEvent.ChargeStatus = ChargeStationEvent.ChargeStationState.Empty;
                }
            }
        }
    }
    private void ChargeState()
    {
        if (chargeManagment.ChargeStationStatus.Find(ChargeStationStatus => ChargeStationStatus.StationID == name).CurrentChargeLevel <= 0)
        {
            //chargeStationEvent = GetComponent<ChargeStationEvent>();
            chargeStationEvent.ChargeStatus = ChargeStationState.Empty;
            //Debug.Log("ChargeStation: Station is empty");
            StopSoundByList(PrefabAudioLibrary.SoundCategoryLists);
            if (chargingCoroutine != null)
            {
                StopCoroutine(chargingCoroutine);
                chargingCoroutine = null;
            }
        }
        //else if (PlayerData.ChargeStatus >= PlayerData.ChargeCapacityMax)
        else if (chargeManagment.ChargeVoltStatus.VoltChargeLevel >= chargeManagment.ChargeVoltStatus.MaxVoltCharge)
        {
            //chargeStationEvent = GetComponent<ChargeStationEvent>();
            chargeStationEvent.ChargeStatus = ChargeStationState.FullyCharged;
            //Debug.Log("ChargeStation: Volt is fully charged");
            StopSoundByList(PrefabAudioLibrary.SoundCategoryLists);
            if (chargingCoroutine != null)
            {
                StopCoroutine(chargingCoroutine);
                chargingCoroutine = null;
            }
        }
        else
        {
            //chargeStationEvent = GetComponent<ChargeStationEvent>();
            chargeStationEvent.ChargeStatus = ChargeStationState.Charging;
            //Debug.Log("ChargeStation: Volt is charging");
            //PlaySoundByList(PrefabAudioLibrary.SoundCategoryLists);
            PlaySound("Charge");
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))// && isCharging)
        {
            //Debug.Log("ChargeStation: Volt exited, stopping charge sound");
            StopSoundByList(PrefabAudioLibrary.SoundCategoryLists);
            if (chargingCoroutine != null)
            {
                StopCoroutine(chargingCoroutine);
            }
            chargingCoroutine = StartCoroutine(RechargeBattery());

         
                chargeStationEvent.ChargeStatus = ChargeStationState.Empty;
                //isRecharging = true;
                //rechargeTimer = 0f;
           
        }
    }

    private IEnumerator DischargeBattery()
    {
        var currentChargeLevel = chargeManagment.ChargeStationStatus.FirstOrDefault(c => c.StationID == name)?.CurrentChargeLevel ?? 0f;
        while (currentChargeLevel > 0f)
        {
            currentChargeLevel -= chargeManagment.ChargeStationProperties.Rate;
            yield return new WaitForSeconds(chargeManagment.ChargeStationProperties.RechargeRate);
        }
        chargeManagment.ChargeStationStatus.FirstOrDefault(c => c.StationID == name).CurrentChargeLevel = currentChargeLevel;

        chargingCoroutine = null;
    }
    private IEnumerator RechargeBattery()
    {
        var chargeStation = chargeManagment.ChargeStationStatus.FirstOrDefault(c => c.StationID == name);
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