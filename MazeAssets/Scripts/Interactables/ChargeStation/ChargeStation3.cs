using UnityEngine;

public class ChargeStation3 : MonoBehaviour
{
    [SerializeField] ChargeManagment2 chargeManagment;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        chargeManagment.voltInsideStation = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        chargeManagment.voltInsideStation = false;
    }
}