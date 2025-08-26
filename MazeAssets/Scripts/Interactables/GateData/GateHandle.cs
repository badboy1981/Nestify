using UnityEngine;
using System.Collections;

public class GateHandle : MonoBehaviour
{
    public Transform targetObject;
    public float rotationDuration = 1f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private Coroutine currentRotationCoroutine;
    private bool isRotatingToTarget = false;

    void Start()
    {
        initialRotation = Quaternion.Euler(-50f, -90f, 90f);
        targetRotation = Quaternion.Euler(-140, -90f, 90f);
        targetObject.rotation = initialRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);
        }

        isRotatingToTarget = true;
        currentRotationCoroutine = StartCoroutine(RotateToTarget(targetRotation, () =>
        {
            isRotatingToTarget = false;
        }));
    }

    private void OnTriggerExit(Collider other)
    {
        if (isRotatingToTarget)
        {
            return;
        }

        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);
        }

        currentRotationCoroutine = StartCoroutine(RotateToTarget(initialRotation));
    }

    private IEnumerator RotateToTarget(Quaternion target, System.Action onComplete = null)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = targetObject.rotation;

        while (elapsedTime < rotationDuration)
        {
            targetObject.rotation = Quaternion.Slerp(startRotation, target, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetObject.rotation = target;
        currentRotationCoroutine = null;
        onComplete?.Invoke();
    }
}