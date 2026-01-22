#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GateSystem3
{
    [CustomEditor(typeof(ReadWriteElementPosition))]
    public class PuzzleElementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ReadWriteElementPosition element = (ReadWriteElementPosition)target;

            if (GUILayout.Button("Get Gate Puzzle Transform List!"))
            {
                element.GateTransformPuzzleList();
            }
            EditorGUILayout.Space(20);
            if (GUILayout.Button("DeleteList"))
            {
                element.ClearAllList();
            }
            EditorGUILayout.Space(20);
            if (GUILayout.Button("READ: Read from JSON and fill to Element!"))
            {
                element.ReadJsonPuzzleData();
            }
            GUILayout.Space(20);
            if (GUILayout.Button("WRITE: Read From element and write JSON file!"))
            {
                element.WriteJsonPazzleData();
            }           
        }
    }
}
#endif