using UnityEngine;

//[CreateAssetMenu(fileName = "ChargeStationEventListener", menuName = "Charge Managment/ChargeStation Event Listener")]
internal class ChargeStationEventListener : MonoBehaviour
{
    //[SerializeField] ChargeStationEvent chargeStationEvent;
    [SerializeField] ChargeManagment chargeManagment;
    [SerializeField] AudioLibrary ChargeStationAudioLibrary;
    [SerializeField] SoundData soundDataEvent;

    private void OnEnable()
    {
        chargeManagment.chargeStationEvent.OnChargeStationStatusChanged += OnChargeStationStatusChanged;
        chargeManagment.chargeStationEvent.OnVoltChargeStatus += OnVoltChargeStatus;
    }

    private void OnDisable()
    {
        chargeManagment.chargeStationEvent.OnChargeStationStatusChanged -= OnChargeStationStatusChanged;
        chargeManagment.chargeStationEvent.OnVoltChargeStatus -= OnVoltChargeStatus;
    }
    private void OnChargeStationStatusChanged(ChargeStationStateEnum status)
    {
        chargeManagment.ActiveChargeStation.ChargeStationState = status;
        switch (status)
        {
            case ChargeStationStateEnum.HasCharge:
                PlaySound("Charge");
                break;
            case ChargeStationStateEnum.Full:
                //StopSound("Charge");
                //PlaySound("ChargeFull");
                break;
            case ChargeStationStateEnum.Empty:
                StopSound("Charge");
                PlaySound("Empty");
                break;
            case ChargeStationStateEnum.VoltExit:
                StopSound("Charge");
                //PlaySound("Empty");
                break;
            case ChargeStationStateEnum.NoCharge:
                StopSound("Charge");
                PlaySound("Empty");
                break;
            case ChargeStationStateEnum.VoltFullCharged:
                StopSound("Charge");
                break;
        }

        if (status == ChargeStationStateEnum.VoltExit)
        {
            StopSound("Charge");
            StopSound("ChargeFull");
            //soundDataEvent.StopSound("Empty");
        }
    }
    private void PlaySound(string soundName)
    {
        soundDataEvent?.Play(soundName, ChargeStationAudioLibrary);
    }
    private void StopSound(string soundName)
    {
        soundDataEvent?.StopSound(soundName);
    }
    private void OnVoltChargeStatus(VoltChargeStateEnum status)
    {
        //Debug.Log($"Volt Charge State: {status}");
        chargeManagment.ChargeVoltStatus.VoltChargeState = status;
        switch (status)
        {
            case VoltChargeStateEnum.Empty:
                //PlaySound("Empty");
                Debug.Log($"Volt Charge State: {status}");
                break;
            case VoltChargeStateEnum.Partial:
                //PlaySound("Charge");
                Debug.Log($"Volt Charge State: {status}");
                break;
            case VoltChargeStateEnum.Charging:
                //PlaySound("Charge");
                Debug.Log($"Volt Charge State: {status}");
                break;
            case VoltChargeStateEnum.FullyCharged:
                //PlaySound("ChargeFull");
                Debug.Log($"Volt Charge State: {status}");
                break;
            case VoltChargeStateEnum.StopDrain:
                Debug.Log($"Volt Charge State: {status}");
                //ChargeVoltStatus.DeChargeVoltRoutine();
                break;
                //default:
                //    break;
        }
    }
}