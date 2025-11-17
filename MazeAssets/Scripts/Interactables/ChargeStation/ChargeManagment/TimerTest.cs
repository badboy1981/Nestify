using UnityEngine;

public class TimerTest : MonoBehaviour
{
    [SerializeField] MazeCore.MazeTimer timer;
    private Coroutine TimerCoroutine;
    public void StartTest()
    {
        if (timer.timer <= timer.minValue)
        {
            timer.timer = timer.duration;
            timer.currentValue = timer.minValue;
        }
        TimerCoroutine = StartCoroutine(timer.TimerCoroutine());
    }
    public void StopTest()
    {
        if (TimerCoroutine != null)
        {
            StopCoroutine(TimerCoroutine);
        }
        TimerCoroutine = null;
    }
}