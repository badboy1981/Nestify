using UnityEngine;

//[CreateAssetMenu(fileName = "ChargeStationEventListener", menuName = "Charge Managment/ChargeStation Event Listener")]
internal class ChargeStationEventListener : MonoBehaviour
{
    [SerializeField] SoundData soundDataEvent;
    [SerializeField] ChargeManagment2 chargeManagment;
    [SerializeField] AudioLibrary ChargeStationAudioLibrary;

    //private bool VoltInsidestation;

    private void OnEnable()
    {
        //chargeManagment.chargeStationEvent.OnVoltInsideStation += OnVoltInsideStationStatusChange;
        chargeManagment.chargeStationEvent.OnChargeStationStatus += OnChargeStationStatusChanged;
        chargeManagment.chargeStationEvent.OnVoltChargeStatus += OnVoltChargeStatus;
    }

    private void OnDisable()
    {
        //chargeManagment.chargeStationEvent.OnVoltInsideStation -= OnVoltInsideStationStatusChange;
        chargeManagment.chargeStationEvent.OnChargeStationStatus -= OnChargeStationStatusChanged;
        chargeManagment.chargeStationEvent.OnVoltChargeStatus -= OnVoltChargeStatus;
    }
    //private void OnVoltInsideStationStatusChange(bool status)
    //{
        //VoltInsidestation = status;
        //Debug.Log($"Volt Inside Station: {status}");
        //if (status)
        //{
        //    PlaySound("Charge");
        //}
        //else
        //{
        //    StopSound("Charge");
        //}
    //}
    private void OnChargeStationStatusChanged(ChargeStationStateEnum status)
    {
        //Debug.Log($"CCCharge Station State: {status}");
        //if (!VoltInsidestation) return;
        
        switch (status)
        {
            case ChargeStationStateEnum.Idle:
                //PlaySound("Charge");
                //PlaySound("Empty");
                break;
            case ChargeStationStateEnum.Available:
                //StopSound("Charge");
                //PlaySound("Empty");
                break;
            case ChargeStationStateEnum.Charging:
                //PlaySound("Charge");
                break;
            case ChargeStationStateEnum.CoolingDown:
                //StopSound("Charge");
                //PlaySound("ChargeFull");
                break;
            case ChargeStationStateEnum.Empty:
                //StopSound("Charge");
                //PlaySound("Empty");
                break;
            case ChargeStationStateEnum.Disabled:
                //StopSound("Charge");
                //PlaySound("Empty");
                break;
            case ChargeStationStateEnum.Overloaded:
                //StopSound("Charge");
                break;
        }
    }
    private void OnVoltChargeStatus(VoltChargeStateEnum status)
    {
        //Debug.Log($"Volt Charge State: {status}");
        Debug.Log($"Volt is {status}!!!");
        //if (VoltInsidestation) return;
        if (status != VoltChargeStateEnum.Charging)
            StopSound("Charge");
        
        switch (status)
        {
            case VoltChargeStateEnum.Empty:
                //PlaySound("Empty");
                break;
            case VoltChargeStateEnum.Emergency:
                //PlaySound("Empty");
                break;
            case VoltChargeStateEnum.Critical:
                //PlaySound("Empty");
                break;
            case VoltChargeStateEnum.Low:
                //PlaySound("Empty");
                break;
            case VoltChargeStateEnum.Partial:
                //PlaySound("Charge");
                break;
            case VoltChargeStateEnum.Normal:
                //PlaySound("Charge");
                //StopSound("Charge");
                break;
            case VoltChargeStateEnum.FullyCharged:
                //PlaySound("ChargeFull");
                //StopSound("Charge");
                break;
            case VoltChargeStateEnum.Charging:
                PlaySound("Charge");                
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