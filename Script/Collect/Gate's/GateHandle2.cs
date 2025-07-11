using Collectable.Gate;
using UnityEngine;

public class GateHandle2 : MonoBehaviour
{
    [SerializeField] GateProperty TargetGateProperty;
    [SerializeField] Transform HandleRotation;
    [SerializeField] float deActiveHandleAngle;
    [SerializeField] float StartHandleAngle;
    [SerializeField] float EndHandleAngle;

    private void Start()
    {
        TargetGateProperty.ActiveGateHandleState = false;
        deActiveHandleAngle = -90f;
        StartHandleAngle = -45f;
        EndHandleAngle = 45f;
    }
    private void OnEnable()
    {
        if (TargetGateProperty != null)
        {
            TargetGateProperty.OnGateUnlocked += ActivateHandleTure;
            TargetGateProperty.OnGatelocked += ActivateHandleFalse;
        }
    }
    private void OnDisable()
    {
        TargetGateProperty.OnGateUnlocked -= ActivateHandleTure;
        TargetGateProperty.OnGateUnlocked -= ActivateHandleFalse;
    }
    private void ActivateHandleTure()
    {
        HandleRotation.localEulerAngles = new(StartHandleAngle, 0, 0);        
        Debug.Log($"Gate {TargetGateProperty.TargetGateName} is open!");
    }
    private void ActivateHandleFalse()
    {
        HandleRotation.localEulerAngles = new(deActiveHandleAngle, 0, 0);
        Debug.Log($"Gate {TargetGateProperty.TargetGateName} is close!");
    }
}