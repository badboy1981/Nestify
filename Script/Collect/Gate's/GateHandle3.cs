using System.Collections;
using Collectable.Gate;
using UnityEngine;

public class GateHandle3 : MonoBehaviour
{
    [Header("Animate Objects")]
    [SerializeField] Animator GateFence;
    [Header("Gate Property")]
    [SerializeField] GateProperty TargetGateProperty;
    [Header("Target Gate")]
    [SerializeField] Animator TargetGateAnimator;
    [Header("Handle")]
    [SerializeField] Animator HandleAnimator;

    private void Start()
    {
        GateFence = GameObject.Find($"Gate{name[0]}").GetComponentInChildren<Animator>();
        //TargetGateProperty = GameObject.Find($"GatePropertyReset").GetComponent<GateProperty>();
        TargetGateAnimator = GateFence.GetComponent<Animator>();
        HandleAnimator = GetComponentInChildren<Animator>();

        TargetGateProperty.ActiveGateHandleState = false;
        TargetGateProperty.gateIsBusy = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player") && !HandelAnimator.IsInTransition(0) && !TargetGateProperty.gateIsBusy && TargetGateProperty.ActiveGateHandleState)
        if (other.CompareTag("Player") && !TargetGateProperty.gateIsBusy && TargetGateProperty.ActiveGateHandleState)
        {
            TargetGateProperty.gateIsBusy = true;
            HandleAnimator.SetBool("OpenGate", true);
            TargetGateAnimator.SetBool("OpenGate", true);
            StartCoroutine(AnimationStop());
        }
    }
    private IEnumerator AnimationStop()
    {
        yield return new WaitForSeconds(TargetGateProperty.AnimationWaitTime);
        HandleAnimator.SetBool("OpenGate", false);
        TargetGateAnimator.SetBool("OpenGate", false);
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
        HandleAnimator.SetBool("ActiveHandle", true);
        //Debug.Log($"Gate {TargetGateProperty.TargetGateName} is open!");
    }
    private void ActivateHandleFalse()
    {
        HandleAnimator.SetBool("ActiveHandle", false);
        //Debug.Log($"Gate {TargetGateProperty.TargetGateName} is close!");
    }
}