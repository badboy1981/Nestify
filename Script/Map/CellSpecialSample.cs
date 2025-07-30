using UnityEngine;

namespace CellMap
{
    public class CellSpecialSample : CellParentProperty
    {
        private string lastCellID = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && name != lastCellID)
            {
                lastCellID = name;
                base.OnVoltEnter(other.gameObject);
                _MazeMap.MazeCellProperty.CellPosition.y = 1.5f; // Ensure the y position is set to 1.5f
            }
        }
    }
}