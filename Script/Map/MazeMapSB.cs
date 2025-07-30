using UnityEngine;

namespace CellMap
{
    [CreateAssetMenu(fileName = "MazeMap", menuName = "Maze Map/Maze Map")]
    public class MazeMapSB : ScriptableObject
    {
        public MazeCell MazeCellProperty;
    }
}