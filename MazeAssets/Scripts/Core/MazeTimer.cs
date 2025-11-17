using System;
using System.Collections;
using UnityEngine;

namespace MazeCore
{
    [Serializable]
    public class MazeTimer
    {
        public float duration; // total time to deplete from full to empty
        public float timer; // current time left
        public float currentValue; // current charge level
        public float maxValue; // Start value
        public float minValue; // End Value
        private float t; // normalized time (0 to 1)

        public float CalCurrentValue()
        {
            if (timer > minValue)
            {
                timer -= Time.deltaTime;
                timer = Mathf.Clamp(timer, minValue, duration);
                t = Mathf.Clamp01(timer / duration);
                currentValue = Mathf.Lerp(minValue, maxValue, t);
            }
            return currentValue;
        }
        public IEnumerator TimerCoroutine()
        {
            while (timer > minValue)
            {
                CalCurrentValue();
                yield return null;
            }
        }
        public void SelectMetod()
        {
            if (maxValue > minValue)
            {

            }
            else
            {

            }
        }
    }
}