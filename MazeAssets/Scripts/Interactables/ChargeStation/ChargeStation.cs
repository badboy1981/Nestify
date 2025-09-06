using System.Collections;
using System.Linq;
using SaveSystem;
using UnityEngine;
using static ChargeStationEvent;

internal class ChargeStation : Interactive
{
    //[SerializeField] BatteryDiagram _BatteryDiagram;
    [SerializeField] ChargeManagment _ChargeManagment;
    [SerializeField] SaveLevelDataSObject PlayerData;
    [SerializeField] float ChargerCapacity = 100f;
    [SerializeField] float IncreaseValue = 10f;
    [SerializeField] float WaitSeconds = 1f;
    [SerializeField] float RechargeRate = 10f;
    [SerializeField] float RechargeInterval = 10f;


    private Coroutine chargingCoroutine;
    private  ChargeStationEvent chargeStationEvent;

    [SerializeField] ChargeStationEventListener eventListener;

    protected override void Awake()
    {
        base.Awake();
        eventListener = GetComponent<ChargeStationEventListener>();
        if (eventListener == null)
        {
            Debug.Log("ChargeStationEventListener component is missing on this GameObject.");
        }
        if (_ChargeManagment == null)
        {
            Debug.Log("ChargeManagment ScriptableObject is not assigned.");
        }
        else
        {
            _ChargeManagment.Initialize();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_ChargeManagment.ChargeStationStatus.Any(s => s.StationID == name))
            {
                _ChargeManagment.ChargeStationStatus.Add(new ChargeStationStatus
                {
                    StationID = name,
                    CurrentChargeLevel = _ChargeManagment.ChargeStationProperties.Capacity
                });
            }
            //Debug.Log("ChargeStation: Volt entered, starting charge sound");

            //chargeStationEvent.OnStatusChanged(ChargeStationEvent.ChargeStationState.Charging);
            //chargeStationEvent.ChargeStatus = ChargeStationState.Charging;

            //PlaySoundByList(PrefabAudioLibrary.SoundCategoryLists);


            if (chargingCoroutine != null)
            {
                StopCoroutine(chargingCoroutine);
            }
            chargingCoroutine = StartCoroutine(DischargeBattery());

            //PlaySound("Charge");
            ChargeState();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        //ChargeState();
    }
    private void ChargeState()
    {
        if (_ChargeManagment.ChargeStationStatus.Find(ChargeStationStatus => ChargeStationStatus.StationID == name).CurrentChargeLevel <= 0)
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
        else if (_ChargeManagment.ChargeVoltStatus.VoltChargeLevel >= _ChargeManagment.ChargeVoltStatus.MaxVoltCharge)
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
            Debug.Log("ChargeStation: Volt exited, stopping charge sound");
            StopSoundByList(PrefabAudioLibrary.SoundCategoryLists);
            if (chargingCoroutine != null)
            {
                StopCoroutine(chargingCoroutine);
            }
            chargingCoroutine = StartCoroutine(RechargeBattery());
        }
    }

    private IEnumerator DischargeBattery()
    {
        //while (ChargerCapacity > 0)
        //{
        //    //_BatteryDiagram.IncreaseValue(IncreaseValue);
        //    //PlayerData.ChargeStatus = Mathf.Min(PlayerData.ChargeStatus + IncreaseValue, PlayerData.ChargeCapacityMax);
        //    ChargerCapacity -= IncreaseValue;
        //    yield return new WaitForSeconds(WaitSeconds);
        //}
        var currentChargeLevel = _ChargeManagment.ChargeStationStatus.FirstOrDefault(c => c.StationID == name)?.CurrentChargeLevel ?? 0f;
        while (currentChargeLevel > 0f)
        {
            currentChargeLevel -= chargeManagment.ChargeStationProperties.Rate;
            yield return new WaitForSeconds(WaitSeconds);
        }
        _ChargeManagment.ChargeStationStatus.FirstOrDefault(c => c.StationID == name).CurrentChargeLevel = currentChargeLevel;
        //while (currentChargeLevel > 0f)
        //{
        //    float theftRate = _ChargeManagment.ChargeStationProperties.ChargeTheftRate;
        //    _ChargeManagment.UpdateVoltCharge(theftRate);
        //    currentChargeLevel = Mathf.Max(currentChargeLevel - theftRate, 0f);
        //    var stationStatus = _ChargeManagment.GetStationStatus(name);
        //    if (stationStatus != null)
        //    {
        //        stationStatus.CurrentChargeLevel = currentChargeLevel;
        //    }
        //    Debug.Log($"ChargeStation: {name} transferred {theftRate} charge to Volt. Current VoltChargeLevel: {_ChargeManagment.ChargeVoltStatus.VoltChargeLevel}");
        //    if (_ChargeManagment.ChargeVoltStatus.VoltChargeLevel <= 0)
        //    {
        //        Debug.Log("Volt's charge is depleted!");
        //    }
        //    yield return new WaitForSeconds(1f);
        //}
        chargingCoroutine = null;
    }
    private IEnumerator RechargeBattery()
    {
        while (ChargerCapacity < 100f)
        {
            ChargerCapacity = Mathf.Min(ChargerCapacity + RechargeRate, 100f);
            yield return new WaitForSeconds(RechargeInterval);
        }
        chargingCoroutine = null;
    }
}