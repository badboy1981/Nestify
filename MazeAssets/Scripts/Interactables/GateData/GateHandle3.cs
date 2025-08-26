using System.Collections;
using Collectable.Gate;
using UnityEngine;

internal class GateHandle3 : Interactive
{
    [Header("Gate Property")]
    [SerializeField] GatePropertyGroup gatePropertyGroup;
    [SerializeField] GateProperty TargetGateProperty;
    [Header("Animate Objects")]
    [SerializeField] Animator GateFence;
    [Header("Target Gate")]
    [SerializeField] Animator TargetGateAnimator;
    [Header("Handle")]
    [SerializeField] Animator HandleAnimator;

    private void Start()
    {
        GateFence = GameObject.Find($"{name[0]}Gate").GetComponentInChildren<Animator>();

        TargetGateAnimator = GateFence.GetComponent<Animator>();
        HandleAnimator = GetComponentInChildren<Animator>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !TargetGateProperty.gateIsBusy && TargetGateProperty.ActiveGateHandleState)
        {
            TargetGateProperty.gateIsBusy = true;
            HandleAnimator.SetBool("OpenGate", true);
            TargetGateAnimator.SetBool("OpenGate", true);
            PlaySound("OpenGateSound");
            StartCoroutine(AnimationStop());
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Player") && TargetGateProperty.gateIsBusy)
        //{
        //    TargetGateProperty.gateIsBusy = false;
        //    HandleAnimator.SetBool("OpenGate", false);
        //    TargetGateAnimator.SetBool("OpenGate", false);
        //}
    }
    private IEnumerator AnimationStop()
    {
        yield return new WaitForSeconds(TargetGateProperty.AnimationWaitTime);
        PlaySound("OpenGateSound");
        HandleAnimator.SetBool("OpenGate", false);
        TargetGateAnimator.SetBool("OpenGate", false);
        TargetGateProperty.gateIsBusy = false;
    }
    private void OnEnable()
    {
        TargetGateProperty = FindGateProperty.GetGateProperty(gatePropertyGroup, name);
        //Debug.Log($"OnEnable: {TargetGateProperty.name}");
        if (TargetGateProperty != null)
        {
            TargetGateProperty.OnGateUnlocked += ActivateHandleTure;
            TargetGateProperty.OnGatelocked += ActivateHandleFalse;
        }
    }
    private void OnDisable()
    {
        //Debug.Log($"OnDisable: {TargetGateProperty.name}");
        TargetGateProperty.OnGateUnlocked -= ActivateHandleTure;
        TargetGateProperty.OnGatelocked -= ActivateHandleFalse;
    }
    private void ActivateHandleTure()
    {
        HandleAnimator.SetBool("ActiveHandle", true);
        //Debug.Log($"Handle {name} is Active!");
    }
    private void ActivateHandleFalse()
    {
        HandleAnimator.SetBool("ActiveHandle", false);
        //Debug.Log($"Handle {name} is DeActive!");
    }
}