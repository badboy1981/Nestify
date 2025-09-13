using UnityEngine;

//[CreateAssetMenu(fileName = "ChargeStationEventListener", menuName = "Charge Managment/ChargeStation Event Listener")]
internal class ChargeStationEventListener : MonoBehaviour
{
    [SerializeField] ChargeStationEvent chargeStationEvent;
    [SerializeField] ChargeManagment chargeManagment;
    [SerializeField] AudioLibrary ChargeStationAudioLibrary;
    [SerializeField] SoundData soundDataEvent;
    //[SerializeField] ChargeStation chargeStation;
    //private void Awake()
    //{
    //    chargeStationEvent ??= chargeStationEvent = new();
    //}
    //internal UnityAction<ChargeStationStateEnum> OnStatusChangedEvent;
    private void OnEnable()
    {
        //if (chargeStationEvent == null)
        //{
        //    //Debug.LogError("ChargeStationEvent is not assigned in the inspector.");
        //    return;
        //}
        chargeStationEvent.OnChargeStationStatusChanged += OnChargeStationStatusChanged;
        chargeStationEvent.OnVoltChargeStatus += OnVoltChargeStatus;
        //OnStatusChangedEvent += OnStatusChanged;
    }

    private void OnDisable()
    {
        chargeStationEvent.OnChargeStationStatusChanged -= OnChargeStationStatusChanged;
        chargeStationEvent.OnVoltChargeStatus -= OnVoltChargeStatus;
    }
    private void OnChargeStationStatusChanged(ChargeStationStateEnum status)
    {
        switch (status)
        {
            //case ChargeStationStateEnum.Charging:
            //    PlaySound("Charge");
            //    break;
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
        chargeManagment.ChargeVoltStatus.ChargeState = status;
    }
}