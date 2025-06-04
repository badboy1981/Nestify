using UnityEngine;
using System.Collections;

public class ChargeStation : MonoBehaviour
{
    [SerializeField] BatteryDiagram _BatteryDiagram;
    [SerializeField] float ChargerCapacity = 100f;
    [SerializeField] float IncreaseValue = 10f;
    [SerializeField] float WaitSeconds = 1f;
    [SerializeField] float RechargeRate = 10f; 
    [SerializeField] float RechargeInterval = 10f; 

    private Coroutine chargingCoroutine; 

    public void OnTriggerEnter(Collider other)
    {        
        if (chargingCoroutine != null)
        {
            StopCoroutine(chargingCoroutine);
        }
        chargingCoroutine = StartCoroutine(DischargeBattery());
    }
    private void OnTriggerExit(Collider other)
    {        
        if (chargingCoroutine != null)
        {
            StopCoroutine(chargingCoroutine);
        }        
        chargingCoroutine = StartCoroutine(RechargeBattery());
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