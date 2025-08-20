using UnityEngine;
using System.Collections;
using Assets.MazeAssets.Scripts.Parent;

internal class ChargeStation : Parent
{
    [SerializeField] BatteryDiagram _BatteryDiagram;
    [SerializeField] float ChargerCapacity = 100f;
    [SerializeField] float IncreaseValue = 10f;
    [SerializeField] float WaitSeconds = 1f;
    [SerializeField] float RechargeRate = 10f;
    [SerializeField] float RechargeInterval = 10f;

    private Coroutine chargingCoroutine;
    private bool isCharging;

    [SerializeField] private string[] soundNames = { "Beep", "Charge" };

    private void Start()
    {
        _BatteryDiagram = GameObject.Find("BatteryDiagram").GetComponent<BatteryDiagram>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCharging)
        {
            Debug.Log("ChargeStation: Volt entered, starting charge sound");
            isCharging = true;
            foreach (string soundName in soundNames)
            {
                PlaySound(soundName);
            }
            if (chargingCoroutine != null)
            {
                StopCoroutine(chargingCoroutine);
            }
            chargingCoroutine = StartCoroutine(DischargeBattery());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isCharging)
        {
            Debug.Log("ChargeStation: Volt exited, stopping charge sound");
            isCharging = false;
            foreach (string soundName in soundNames)
            {
                StopSound(soundName);
            }
            if (chargingCoroutine != null)
            {
                StopCoroutine(chargingCoroutine);
            }
            chargingCoroutine = StartCoroutine(RechargeBattery());
        }
    }

    private IEnumerator DischargeBattery()
    {
        while (ChargerCapacity > 0)
        {
            _BatteryDiagram.IncreaseValue(IncreaseValue);
            ChargerCapacity -= IncreaseValue;
            yield return new WaitForSeconds(WaitSeconds);
        }
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