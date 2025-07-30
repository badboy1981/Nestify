using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TelePort
{
    public class PortalGlow : MonoBehaviour
    {
        [Header("Bloom Effect Settings")]
        [SerializeField] BloomSettings animatedBloom;
        [SerializeField] Volume volume;
        [SerializeField] AnimationCurve intensityCurve;
        [SerializeField] AnimationCurve thresholdCurve;
        [SerializeField] float duration = 1f;

        [Header("Volt Beacon")]
        [SerializeField] PointProperty TeleportPositionData;
        [SerializeField] Transform VoltPosition;
        [SerializeField] Transform VoltBeaconPosition;
        [SerializeField] Vector3 MarkedPoint;


        [SerializeField] Bloom bloom;

        void Start()
        {
            //volume.profile.TryGet(out bloom);
        }
        private void OnTriggerEnter(Collider other)
        {
            other.transform.position = VoltBeaconPosition.position;
        }
        public void MarkVoltPosition()
        {
            //VoltPosition = GameObject.Find("Drone").GetComponent<Transform>();
            //TeleportPositionData.Position = GameObject.Find("Drone").GetComponent<Transform>();
            VoltBeaconPosition = GameObject.Find("VoltBeacon").GetComponent<Transform>();
            //MarkedPoint = VoltPosition.position;

            VoltBeaconPosition.position = MarkedPoint;
        }
        public void TriggerBloomEffectAnimation()
        {
            gameObject.SetActive(true);
            transform.position = MarkedPoint;
            if (volume.profile.TryGet(out bloom))
            {
                bloom.intensity.value = animatedBloom.intensity;
                bloom.threshold.value = animatedBloom.threshold;
            }
            else
            {
                Debug.LogWarning("Bloom effect not found in the volume profile.");
            }
        }
        public void TriggerBloomEffect()
        {
            gameObject.SetActive(true);
            volume.profile.TryGet(out bloom);
            StartCoroutine(AnimateBloom());
        }
        private System.Collections.IEnumerator AnimateBloom()
        {
            float time = 0f;
            while (time < duration)
            {
                float t = Mathf.Clamp01(time / duration);
                bloom.intensity.value = intensityCurve.Evaluate(t);
                bloom.threshold.value = thresholdCurve.Evaluate(t);
                time += Time.deltaTime;
                yield return null;
            }

            bloom.intensity.value = intensityCurve.Evaluate(1f);
            bloom.threshold.value = thresholdCurve.Evaluate(1f);
        }
    }
}