using UnityEngine;
using System.Collections;
using SaveSystem;

public class BatteryDiagram : MonoBehaviour
{
    [SerializeField] float startValue;
    [SerializeField] float duration = 30f;
    [SerializeField] SaveLevelDataSObject PlayerData;
    private RectTransform _BatteryDiagram;
    //private Coroutine decreaseCoroutine;
    public float currentValue;
    private float elapsedTime;

    private void Start()
    {
        _BatteryDiagram = GetComponent<RectTransform>();
        //PlayerData.ChargeCapacity = 100f;
        //startValue = PlayerData.ChargeCapacityMax;
        //PlayerData.ChargeStatus = startValue;
        //currentValue = PlayerData.ChargeStatus;
        //decreaseCoroutine = 
        StartCoroutine(DecreaseValueOverTime());
    }

    private IEnumerator DecreaseValueOverTime()
    {
        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            //currentValue = Mathf.SmoothStep(startValue, 0f, elapsedTime / duration);
            //currentValue = Mathf.FloorToInt(Mathf.SmoothStep(startValue, 0f, elapsedTime / duration));
            currentValue = Mathf.FloorToInt(Mathf.SmoothStep(startValue, 0f, elapsedTime / duration));
            //PlayerData.ChargeStatus = currentValue;
            elapsedTime += Time.deltaTime;
            //_BatteryDiagram.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, PlayerData.ChargeStatus);
            //PlayerData.ChargeStatus = currentValue;
            yield return null;
        }
        //currentValue = 0;
    }

    //public void IncreaseValue(float IncreaseValue)
    //{
    //    float newValue = Mathf.Min(currentValue + IncreaseValue, startValue);
    //    float valueDifference = newValue - currentValue;
    //    //float remainingTime = duration - elapsedTime;
    //    float newElapsedTime = elapsedTime - (valueDifference / startValue) * duration;


    //    currentValue = newValue;
    //    elapsedTime = Mathf.Max(0, newElapsedTime);
    //    _BatteryDiagram.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentValue);
    //}
}