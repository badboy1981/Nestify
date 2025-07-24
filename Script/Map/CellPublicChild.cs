using UnityEngine;

namespace CellMap
{
    public class CellPublicChild : CellParentProperty
    {
        private void OnTriggerEnter(Collider other)
        {
            base.OnVoltEnter(other.gameObject);
        }
    }
}