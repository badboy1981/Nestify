using CellMap;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TelePort
{
    public class ManagePortal : MonoBehaviour
    {
        [Header("Portal Property")]        
        [SerializeField] MazeMapSB CellProperty;
        [SerializeField] PointProperty TeleportPositionData;

        [Header("Bloom Effect Settings")]
        [SerializeField] BloomSettings animatedBloom;
        [SerializeField] Volume volume;
        [SerializeField] Bloom bloom;

        [Header("Glow Animation")]
        [SerializeField] AnimationCurve thresholdCurve;
        [SerializeField] AnimationCurve intensityCurve;
        [SerializeField] float duration = 1f;
        [SerializeField] float distance = 1f;

    
        public void CreatePortal()
        {
            if (!gameObject.activeSelf)
            {
                transform.position = CellProperty.MazeCellProperty.CellPosition;
                gameObject.SetActive(true);
                volume = gameObject.GetComponent<Volume>();
                SetBloomCurves(PortalBloomState.LowGlow);
                TriggerBloomEffect();
            }
            else
            {
                Debug.Log("Warning: Portal already deployed. Duplicate creation blocked.");
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            SetBloomCurves(PortalBloomState.FlashOut);
            TriggerBloomEffect();
            transform.position = TeleportPositionData.Position;
            TeleportPositionData.Position.x += distance;
            other.transform.position = TeleportPositionData.Position;
        }
        private void OnTriggerExit(Collider other)
        {
            SetBloomCurves(PortalBloomState.Off);
            TriggerBloomEffect();
            gameObject.SetActive(false);
        }
        public enum PortalBloomState
        {
            LowGlow,
            FlashOut,
            Off
        }
        private void SetBloomCurves(PortalBloomState state)
        {
            switch (state)
            {
                case PortalBloomState.LowGlow:
                    intensityCurve = new AnimationCurve(
                        new Keyframe(0f, 0f),
                        new Keyframe(0.5f, 10f),
                        new Keyframe(1f, 1f)                        
                    );
                    thresholdCurve = new AnimationCurve(
                        new Keyframe(0f, 1.2f),
                        new Keyframe(1f, 1f)
                    );
                    break;

                case PortalBloomState.FlashOut:
                    intensityCurve = new AnimationCurve(
                        new Keyframe(0f, 30f),
                        new Keyframe(1f, 10f)
                    );
                    thresholdCurve = new AnimationCurve(
                        new Keyframe(0f, 1f),
                        new Keyframe(1f, 0f)
                    );
                    break;

                case PortalBloomState.Off:
                    intensityCurve = new AnimationCurve(
                        new Keyframe(0f, 250f),
                        new Keyframe(1f, 0f)
                    );
                    thresholdCurve = new AnimationCurve(
                        new Keyframe(0f, 0.3f),
                        new Keyframe(1f, 0f)
                    );
                    break;
            }
        }
        public void TriggerBloomEffect()
        {
            if (volume.profile.TryGet(out bloom)) { StartCoroutine(AnimateBloom()); }
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

            bloom.threshold.value = thresholdCurve.Evaluate(1f);
            bloom.intensity.value = intensityCurve.Evaluate(1f);
        }
    }
}
//🛰️ ⚡ VoltBeacon & Portal System Alerts ⚡ 🛰️
//- VoltBeacon Initialized. Anchoring complete.
//- Warning: VoltBeacon already active. Duplicate registration blocked.
//- Portal established. Return channel to VoltBeacon activated.
//- Warning: Portal already deployed. Duplicate creation blocked.
//- Error: Portal range exceeded. Return function disabled.
