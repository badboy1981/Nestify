using UnityEngine;

public class ChargeManager : MonoBehaviour
{
    [SerializeField] ChargeManagment chargeManagment;

    Coroutine drainRoutine;
    private void Start()
    {
        drainRoutine = StartCoroutine(chargeManagment.DeChargeVoltRoutine());
    }
    //private void StartDrain()
    //{
    //    drainRoutine = StartCoroutine(chargeManagment.DeChargeVoltRoutine());
    //}
    //private void StopDrain()
    //{
    //    if (drainRoutine != null)
    //        StopCoroutine(drainRoutine);
    //    drainRoutine = null;
    //}
}