using UnityEngine;

namespace CellMap
{
    public class CellPublicChild : CellParentProperty
    {
        private void OnTriggerEnter(Collider other)
        {
            base.OnVoltEnter(other.gameObject);
            //_MazeMap.MazeCellProperty.CellPosition.y = 0.5f; // Ensure the y position is set to 0.5f
        }
    }
}