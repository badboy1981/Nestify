using System.Collections.Generic;
using UnityEngine;

namespace CellMap
{
    [CreateAssetMenu(fileName = "MazeCellPropertyList", menuName = "Maze Map/Maze Cell Property List")]
    public class MazeCellPropertyList : ScriptableObject
    {
        public List<MazeCell> CellPropertyList;
    }
}