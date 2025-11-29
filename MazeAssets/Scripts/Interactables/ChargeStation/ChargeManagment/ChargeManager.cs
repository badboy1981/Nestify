using UnityEngine;

public class ChargeManager : MonoBehaviour
{
    [SerializeField] ChargeManagment2 chargeManagment;
    Coroutine drainRoutine;
    private void Start()
    {
        StartDrain();
    }
    //private void Update()
    //{
    //    chargeManagment.chargeStationEvent.VoltChargeStatus = chargeManagment.ChargeVoltStatus.VoltChargeState;
    //}
    private void StartDrain()
    {
        drainRoutine = StartCoroutine(chargeManagment.DeChargeVoltRoutine());
    }
    private void StopDrain()
    {
        if (drainRoutine != null)
            StopCoroutine(drainRoutine);
        drainRoutine = null;
    }
}