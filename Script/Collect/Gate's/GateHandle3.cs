using System.Collections;
using Collectable.Gate;
using UnityEngine;

public class GateHandle3 : MonoBehaviour
{
    [SerializeField] GateProperty TargetGateProperty;
    [SerializeField] Transform HandleRotation;
    [SerializeField] Transform TargetGate;
    [SerializeField] Animator HandelAnimator;
    [SerializeField] Animator GateAnimator;

    private void Start()
    {
        TargetGateProperty.ActiveGateHandleState = false;
        TargetGateProperty.gateIsBusy = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !HandelAnimator.IsInTransition(0) && !TargetGateProperty.gateIsBusy)
        {
            TargetGateProperty.gateIsBusy = true;
            HandelAnimator.SetBool("OpenGate", true);
            GateAnimator.SetBool("OpenGate", true);
            StartCoroutine(AnimationStop());
        }
    }

    private IEnumerator AnimationStop()
    {
        yield return new WaitForSeconds(TargetGateProperty.AnimationWaitTime);
        HandelAnimator.SetBool("OpenGate", false);
        GateAnimator.SetBool("OpenGate", false);
        TargetGateProperty.gateIsBusy = false;
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
        TargetGateProperty.OnGatelocked -= ActivateHandleFalse;
    }
    private void ActivateHandleTure()
    {
        HandelAnimator.SetBool("ActiveHandle", true);
        Debug.Log($"Gate {TargetGateProperty.TargetGateName} is open!");
    }
    private void ActivateHandleFalse()
    {
        HandelAnimator.SetBool("ActiveHandle", false);
        Debug.Log($"Gate {TargetGateProperty.TargetGateName} is close!");
    }
}