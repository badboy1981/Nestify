using CellMap;
using UnityEngine;

public class TestCellPosition : MonoBehaviour
{
    [SerializeField] MazeMapSB _MazeMap;
    private void Update()
    {
        transform.position = _MazeMap.MazeCellProperty.CellPosition;
    }
}