using UnityEngine;

public class ChargeManager : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;
    //[SerializeField] ChargeStationEvent chargeStationEvent;

    Coroutine drainRoutine;
    private void Start()
    {
        chargeManagment.ChargeVoltStatus.ChargeState = VoltChargeStateEnum.FullyCharged;
        StartDrain();
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