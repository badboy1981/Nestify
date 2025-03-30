using UnityEngine;

namespace Collectable
{
    public class Battery : Collectable
    {
        [SerializeField] BatteryDiagram _BatteryDiagram;
        [SerializeField] float IncreaseValue = 20f;
        public override void OnTriggerEnter(Collider other)
        {
            //base.OnTriggerEnter(other);
            _BatteryDiagram.IncreaseValue(IncreaseValue);
        }
    }
}