using UnityEngine;

namespace CellMap
{
    public class CellParentProperty : MonoBehaviour
    {
        public MazeMapSB _MazeMap;
        [SerializeField] MazeCellPropertyList CellPropertyList;
        public virtual void OnVoltEnter(GameObject Volt)
        {
            if (Volt.CompareTag("Player"))
            {
                _MazeMap.MazeCellProperty = CellPropertyList.CellPropertyList.Find(g => g.CellID == name);
                //Debug.Log($"{Volt.name} entered cell {name}");
            }
        }
    }
}