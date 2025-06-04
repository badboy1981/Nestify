using UnityEngine;
using System.Collections;

public class BatteryDiagram : MonoBehaviour
{
    private RectTransform _BatteryDiagram;
    [SerializeField] float startValue = 500;
    [SerializeField] float duration = 30f;
    public float currentValue;
    private Coroutine decreaseCoroutine;
    private float elapsedTime;

    private void Start()
    {
        _BatteryDiagram = GetComponent<RectTransform>();
        currentValue = startValue;
        decreaseCoroutine = StartCoroutine(DecreaseValueOverTime());
    }

    private IEnumerator DecreaseValueOverTime()
    {
        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            //currentValue = Mathf.SmoothStep(startValue, 0f, elapsedTime / duration);
            currentValue = Mathf.FloorToInt(Mathf.SmoothStep(startValue, 0f, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            _BatteryDiagram.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentValue);
            yield return null;
        }
        currentValue = 0;
    }

    public void IncreaseValue(float IncreaseValue)
    {
        float newValue = Mathf.Min(currentValue + IncreaseValue, startValue);
        float valueDifference = newValue - currentValue;
        //float remainingTime = duration - elapsedTime;
        float newElapsedTime = elapsedTime - (valueDifference / startValue) * duration;


        currentValue = newValue;
        elapsedTime = Mathf.Max(0, newElapsedTime);
        _BatteryDiagram.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentValue);
    }
}