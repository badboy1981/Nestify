using UnityEngine;

public class ChargeManager : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;
    Coroutine drainRoutine;

    private void Start()
    {

        StartDrain();
    }
    private void Update()
    {
        chargeManagment.chargeStationEvent.VoltChargeStatus = chargeManagment.ChargeVoltStatus.ChargeState;
    }
    private void StartDrain()
    {
        drainRoutine = StartCoroutine(chargeManagment.ChargeVoltStatus.DeChargeVoltRoutine());
    }
    private void StopDrain()
    {
        if (drainRoutine != null)
            StopCoroutine(drainRoutine);
        drainRoutine = null;
    }
}