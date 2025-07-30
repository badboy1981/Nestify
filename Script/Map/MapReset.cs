using CellMap;
using UnityEngine;

public class MapReset : MonoBehaviour
{
    [SerializeField] MazeMapSB mazeMap;
    private void Start()
    {
        mazeMap.MazeCellProperty.CellID="D0";
        mazeMap.MazeCellProperty.CellPosition = new Vector3(0, 0.5f, -8);
    }
}
