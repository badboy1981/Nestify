using UnityEngine;

public class ChargeStation : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;

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