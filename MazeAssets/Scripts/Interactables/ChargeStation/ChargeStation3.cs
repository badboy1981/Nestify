using UnityEngine;

public class ChargeStation3 : MonoBehaviour
{
    [SerializeField] ChargeManagment2 chargeManagment;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        VoltEnter();
    }
    private void OnTriggerStay(Collider other)
    {
        //if (!other.CompareTag("Player")) return;
        //VoltStay();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        VoltExit();
    }
    private void VoltEnter()
    {
        chargeManagment.VoltInside = true;
        chargeManagment.chargeStationEvent.ChargeStatus = ChargeStationStateEnum.VoltEnter;
    }
    private void VoltStay()
    {

    }
    private void VoltExit()
    {
        chargeManagment.VoltInside = false;
        chargeManagment.chargeStationEvent.ChargeStatus = ChargeStationStateEnum.VoltExit;
    }
}