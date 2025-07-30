//using CellMap;
//using UnityEngine;

//public class CreateMazeMapList : MonoBehaviour
//{
//    [SerializeField] MazeCellPropertyList Mcpl;

//    [ContextMenu("Fill MazeCellPropertyList.")]
//    private void GetChild()
//    {
//        Mcpl.CellPropertyList.Clear();
//        foreach (Transform child in transform)
//        {
//            var cp = new MazeCell()
//            {
//                CellID = child.name,
//                CellPosition = CellUtils.RoundVector(child.position)
//            };
//            cp.CellPosition.y = 0.5f;
//            Mcpl.CellPropertyList.Add(cp);
//        }
//    }
//}