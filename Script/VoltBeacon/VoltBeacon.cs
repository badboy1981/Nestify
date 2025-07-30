using UnityEngine;

namespace TelePort
{
    public class VoltBeacon : MonoBehaviour
    {
        [SerializeField] PointProperty TeleportPositionData;
        [SerializeField] PortalGlow portalGlow;
        [SerializeField] Vector3 markPosition;
        [SerializeField] float effectRadius = 5f;


        void Start()
        {
            markPosition = transform.position;
        }

        public void ReturnVoltToMark(GameObject volt)
        {
            volt.transform.position = markPosition;
            Debug.Log("⚡ Volt returned to beacon position!");

            //portalGlow.TriggerBloomEffect(); // فعال‌سازی افکت Bloom
        }
        public void TryReturnVolt(GameObject volt)
        {
            float distance = Vector3.Distance(volt.transform.position, transform.position);

            if (distance <= effectRadius)
            {
                volt.transform.position = markPosition;
                Debug.Log("✅ Volt returned to beacon");

                portalGlow.TriggerBloomEffectAnimation(); // فعال‌سازی افکت Bloom
            }
            else
            {
                Debug.Log("❌ Volt is too far from beacon");
                // می‌تونی پیغام HUD یا صدای هشدار بذاری
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 1, 0.2f); // آبی ملایم
            Gizmos.DrawSphere(transform.position, effectRadius);
        }
    }
}