using UnityEngine;
using System.Collections;

public class RotatingObject : MonoBehaviour
{
    public Transform targetObject;
    public float rotationDuration = 1f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private Coroutine currentRotationCoroutine;
    private bool isRotatingToTarget = false; // پرچم تشخیص وضعیت چرخش

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

        isRotatingToTarget = true; // فعال کردن پرچم هنگام شروع چرخش به هدف
        currentRotationCoroutine = StartCoroutine(RotateToTarget(targetRotation, () => {
            isRotatingToTarget = false; // غیرفعال کردن پرچم پس از اتمام چرخش
        }));
    }

    private void OnTriggerExit(Collider other)
    {
        // اگر چرخش به هدف کامل نشده، هیچ کاری نکن
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
        onComplete?.Invoke(); // اجرای کالبک پس از اتمام
    }
}