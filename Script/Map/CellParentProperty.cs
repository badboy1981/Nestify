using UnityEngine;

namespace CellMap
{
    public class CellParentProperty : MonoBehaviour
    {
        public virtual void OnVoltEnter(GameObject Volt)
        {
            Debug.Log($"{Volt.name} entered cell {name}");            
        }
    }
}