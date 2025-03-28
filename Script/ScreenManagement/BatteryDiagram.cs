using UnityEngine;
using UnityEngine.UI;

namespace MazeScreenManagement
{
    public class BatteryDiagram : MonoBehaviour
    {
        [Header("A Method")]
        [SerializeField] RectTransform _BatteryDiagram;
        [SerializeField] float maxCharge = 500f;
        [SerializeField] float drainTime = 30f;
        [SerializeField] Slider batterySlider;
        [SerializeField] float Charge;
        [SerializeField] Vector2 BatteryDim;
        [SerializeField] float currentCharge;
        [SerializeField] float progress;

        [Header("B Method")]
        [SerializeField] float c;

        private void Start()
        {
            BStart();
        }
        private void Update()
        {
            BUpdate();
        }
        private void BStart()
        {
            _BatteryDiagram = GetComponent<RectTransform>();
            currentCharge = maxCharge;
        }
        private void BUpdate()
        {
            currentCharge -= maxCharge * Time.deltaTime / drainTime;
            progress = 1 - (currentCharge / maxCharge);
            currentCharge = Mathf.SmoothStep(maxCharge, 0f, progress);
            _BatteryDiagram.sizeDelta = new(100, currentCharge);
        }
        private void AStart()
        {
            _BatteryDiagram = GetComponent<RectTransform>();
            currentCharge = maxCharge;
            BatteryDim = new(100f, maxCharge);
        }
        private void AUpdate()
        {
            if (currentCharge > 0)
            {
                currentCharge -= maxCharge * Time.deltaTime / drainTime;
                progress = 1 - (currentCharge / maxCharge);
                currentCharge = Mathf.SmoothStep(maxCharge, 0f, progress);

                //if (batterySlider != null)
                //{
                //    batterySlider.value = currentCharge / maxCharge;
                //}
                BatteryDim.y = currentCharge / maxCharge;
                Charge = BatteryDim.y;
                _BatteryDiagram.sizeDelta = BatteryDim;
                Debug.Log("شارژ باقی‌مانده: " + currentCharge.ToString("F1"));
            }
        }
    }
}