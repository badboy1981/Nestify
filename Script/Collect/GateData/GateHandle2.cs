using Collectable.Gate;
using UnityEngine;
using System.Collections;

public class GateHandle2 : MonoBehaviour
{
    [SerializeField] GateProperty TargetGateProperty;
    [SerializeField] Transform HandleRotation;
    [SerializeField] Transform TargetGate;
    [SerializeField] float deActiveHandleAngle;
    [SerializeField] float StartHandleAngle;

    [SerializeField] float WaitTime;

    [SerializeField] bool handleRotated = false;
    //[SerializeField] bool GateOpen = false;
    private Quaternion originalRotation;
    //private Vector3 orginalPosition;

    private void Start()
    {
        StopAllCoroutines();
        //orginalPosition = TargetGate.position;
        //originalRotation = HandleRotation.rotation;
        TargetGateProperty.ActiveGateHandleState = false;
        deActiveHandleAngle = -90f;
        StartHandleAngle = -45f;
        WaitTime = 3f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (TargetGateProperty.ActiveGateHandleState && !handleRotated)
        {    
            Quaternion targetRotation = Quaternion.AngleAxis(90f, HandleRotation.transform.right) * HandleRotation.rotation;
            StartCoroutine(RotateStart(HandleRotation, targetRotation));
            handleRotated = true;
        }
        else if (!TargetGateProperty.ActiveGateHandleState)
        {
            Debug.Log("Need to find lost key!");
        }
        else
        {
            Debug.Log("Already rotated");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(RotateReset(HandleRotation, WaitTime));
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
        HandleRotation.localEulerAngles = new(StartHandleAngle, 0, 0);
        originalRotation = HandleRotation.rotation;
        //Debug.Log($"Gate {TargetGateProperty.TargetGateName} is open!");
    }
    private void ActivateHandleFalse()
    {
        HandleRotation.localEulerAngles = new(deActiveHandleAngle, 0, 0);
        //Debug.Log($"Gate {TargetGateProperty.TargetGateName} is close!");
    }
    private IEnumerator RotateTo(Transform target, Quaternion targetRot)
    {
        float duration = 1f;
        float time = 0f;
        Quaternion startRot = target.rotation;

        while (time < duration)
        {
            time += Time.deltaTime;
            target.rotation = Quaternion.Slerp(startRot, targetRot, time / duration);
            yield return null;
        }

        target.rotation = targetRot;
    }
    //private IEnumerator RotateStart(Transform target, Quaternion targetRot, float waitTime)
    private IEnumerator RotateStart(Transform target, Quaternion targetRot)
    {
        yield return RotateTo(target, targetRot);
        //handleRotated = true;
        //if (!GateOpen)
        //{
        //    StartCoroutine(MoveGate(1f, 6f, 1.5f));
        //    //StartCoroutine(MoveGateTo(orginalPosition, 1.5f));
        //    GateOpen = true;
        //}
    }
    private IEnumerator RotateReset(Transform target, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        yield return RotateTo(target, originalRotation);
        handleRotated = false;
        //StartCoroutine(MoveGate(-1f, 6f, 1.5f));
        ////StartCoroutine(MoveGateTo(orginalPosition, 1.5f));
        //GateOpen = false;
    }
    private IEnumerator MoveGate(float direction, float distance, float duration)
    {
        Vector3 startPos = TargetGate.position;
        Vector3 endPos = startPos + TargetGate.forward * direction * distance;

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            TargetGate.position = Vector3.Lerp(startPos, endPos, time / duration);
            yield return null;
        }
        TargetGate.position = endPos;
    }
    private IEnumerator MoveGateTo(Vector3 targetPos, float duration)
    {
        Vector3 startPos = TargetGate.position;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            TargetGate.position = Vector3.Lerp(startPos, targetPos, time / duration);
            yield return null;
        }
        TargetGate.position = targetPos;
    }
}