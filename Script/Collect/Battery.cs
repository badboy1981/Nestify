using UnityEngine;

namespace Collectable
{
    public class Battery : Collectable
    {
        [SerializeField] BatteryDiagram _BatteryDiagram;
        [SerializeField] float IncreaseValue = 20f;
        private void Start()
        {
            _BatteryDiagram = GameObject.Find("BatteryDiagram").GetComponent<BatteryDiagram>();
        }
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            _BatteryDiagram.IncreaseValue(IncreaseValue);
        }
    }
}