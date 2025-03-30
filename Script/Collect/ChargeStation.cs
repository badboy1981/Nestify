using UnityEngine;
using System.Collections;

public class ChargeStation : MonoBehaviour
{
    [SerializeField] BatteryDiagram _BatteryDiagram;
    [SerializeField] float ChargerCapacity = 100f;
    [SerializeField] float IncreaseValue = 10f;
    [SerializeField] float WaitSeconds = 1f;
    [SerializeField] float WaitTimeChargeItself = 60f;

    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(IncreaseValueOverTime());
    }

    private IEnumerator IncreaseValueOverTime()
    {
        while (ChargerCapacity > 0)
        {
            _BatteryDiagram.IncreaseValue(IncreaseValue);
            ChargerCapacity -= IncreaseValue;
            yield return new WaitForSeconds(WaitSeconds);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //The charging station starts charging itself.
        StartCoroutine(ResetChargerCapacity());
    }
    private IEnumerator ResetChargerCapacity()
    {
        yield return new WaitForSeconds(WaitTimeChargeItself);
        ChargerCapacity = 100f;
    }
}