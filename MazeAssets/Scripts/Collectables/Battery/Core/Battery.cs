using UnityEngine;

namespace Collectable
{
    internal class Battery : Collectable
    {
        [SerializeField] BatteryDiagram _BatteryDiagram;
        [SerializeField] float IncreaseValue = 20f;

        protected override void OnTriggerEnter(Collider other)
        {
            _BatteryDiagram = GameObject.Find("BatteryDiagram").GetComponent<BatteryDiagram>();
            base.OnTriggerEnter(other);
            _BatteryDiagram.IncreaseValue(IncreaseValue);
        }
    }
}