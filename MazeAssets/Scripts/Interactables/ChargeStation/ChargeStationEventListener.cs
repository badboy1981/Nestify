using UnityEngine;

//[CreateAssetMenu(fileName = "ChargeStationEventListener", menuName = "Charge Managment/ChargeStation Event Listener")]
internal class ChargeStationEventListener : MonoBehaviour
{
    [SerializeField] SoundData soundDataEvent;
    [SerializeField] ChargeManagment2 chargeManagment;
    [SerializeField] AudioLibrary ChargeStationAudioLibrary;
   

    private void OnEnable()
    {
        chargeManagment.chargeStationEvent.OnChargeStationStatus += OnChargeStationStatusChanged;
        chargeManagment.chargeStationEvent.OnVoltChargeStatus += OnVoltChargeStatus;
        chargeManagment.chargeStationEvent.OnVoltInsideStation += OnVoltInsideStationStatusChange;
    }

    private void OnDisable()
    {
        chargeManagment.chargeStationEvent.OnChargeStationStatus -= OnChargeStationStatusChanged;
        chargeManagment.chargeStationEvent.OnVoltChargeStatus -= OnVoltChargeStatus;
        chargeManagment.chargeStationEvent.OnVoltInsideStation -= OnVoltInsideStationStatusChange;
    }
    private void OnVoltInsideStationStatusChange(bool status)
    {
        //Debug.Log($"Volt Inside Station: {status}");
        if(status)
        {
            PlaySound("Charge");
        }
        else
        {
            StopSound("Charge");
        }
    }
    private void OnChargeStationStatusChanged(ChargeStationStateEnum status)
    {
        //Debug.Log($"CCCharge Station State: {status}");
        switch (status)
        {
            case ChargeStationStateEnum.VoltEnter:
                //PlaySound("Charge");
                //PlaySound("Empty");
                break;
            case ChargeStationStateEnum.VoltExit:
                //StopSound("Charge");
                //PlaySound("Empty");
                break;
            case ChargeStationStateEnum.HasCharge:
                //PlaySound("Charge");
                break;
            case ChargeStationStateEnum.Full:
                //StopSound("Charge");
                //PlaySound("ChargeFull");
                break;
            case ChargeStationStateEnum.Empty:
                //StopSound("Charge");
                //PlaySound("Empty");
                break;
            case ChargeStationStateEnum.NoCharge:
                //StopSound("Charge");
                //PlaySound("Empty");
                break;
            case ChargeStationStateEnum.VoltFullCharged:
                //StopSound("Charge");
                break;
        }
    }
    private void OnVoltChargeStatus(VoltChargeStateEnum status)
    {
        //Debug.Log($"Volt Charge State: {status}");
         switch (status)
        {
            case VoltChargeStateEnum.Empty:
                //PlaySound("Empty");
                break;
            case VoltChargeStateEnum.Partial:
                //PlaySound("Charge");
                break;
            case VoltChargeStateEnum.Charging:
                //PlaySound("Charge");
                //StopSound("Charge");
                break;
            case VoltChargeStateEnum.FullyCharged:
                //PlaySound("ChargeFull");
                StopSound("Charge");
                break;
            case VoltChargeStateEnum.StopDrain:
                //PlaySound("Charge");
                //ChargeVoltStatus.DeChargeVoltRoutine();
                break;
                //default:
                //    break;
        }
    }
    private void PlaySound(string soundName)
    {
        if (soundDataEvent != null)
        {
            soundDataEvent.Play(soundName, ChargeStationAudioLibrary);
        }
    }
    private void StopSound(string soundName)
    {
        if (soundDataEvent != null)
        {
            soundDataEvent.StopSound(soundName);
        }
    }
}