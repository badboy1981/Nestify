using UnityEngine;
using System.Collections;

public class BatteryDiagram2 : MonoBehaviour
{
    private RectTransform _BatteryDiagram;
    [SerializeField] float startValue = 500f;
    [SerializeField] float duration = 30f;
    public float currentValue;
    private Coroutine decreaseCoroutine;

    private void Start()
    {
        _BatteryDiagram = GetComponent<RectTransform>();
        currentValue = startValue;
        decreaseCoroutine = StartCoroutine(DecreaseValueOverTime());
    }

    private IEnumerator DecreaseValueOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            currentValue = Mathf.SmoothStep(startValue, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            _BatteryDiagram.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentValue);
            yield return null;
        }
        currentValue = 0f;
    }

    public void IncreaseValue()
    {
        if (decreaseCoroutine != null)
        {
            StopCoroutine(decreaseCoroutine);
        }
        currentValue = Mathf.Min(currentValue + 20f, startValue);
        startValue = currentValue;
        decreaseCoroutine = StartCoroutine(DecreaseValueOverTime());
    }

    public void ResetCoroutine()
    {
        if (decreaseCoroutine != null)
        {
            StopCoroutine(decreaseCoroutine);
        }
        currentValue = startValue;
        decreaseCoroutine = StartCoroutine(DecreaseValueOverTime());
    }

    public void IncreaseDuration(float additionalTime)
    {
        duration += additionalTime;
        ResetCoroutine();
    }
}