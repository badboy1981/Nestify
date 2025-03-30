using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ValueDecreaser : MonoBehaviour
{
    private RectTransform _BatteryDiagram;
    [SerializeField] float startValue = 500f;
    [SerializeField] float duration = 30f;
    [SerializeField] float currentValue;

    private void Start()
    {
        _BatteryDiagram = GetComponent<RectTransform>();
        currentValue = startValue;
        StartCoroutine(DecreaseValueOverTime());
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
}