using UnityEngine;

public class ChargeStationEvevntSetter : MonoBehaviour
{
    private readonly ChargeStationEvent ch;
    private void OnTriggerEnter(Collider other)
    {
        //ch.OnStatusChanged(ChargeStationEvent.ChargeStationState.Charging);//No. 1
        ch.ChargeStatus = ChargeStationEvent.ChargeStationState.Charging;//No. 2
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
