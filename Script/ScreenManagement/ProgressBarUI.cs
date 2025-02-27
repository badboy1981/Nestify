using UnityEngine;
using UnityEditor;

namespace MazeScreenManagement
{
    [CustomEditor(typeof(ProgressBar))]
    public class ProgressBarUI : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ProgressBar progressBar = (ProgressBar)target;
            if (GUILayout.Button("Drow Element!"))
            {
                progressBar.Excute();
            }
        }
    }
}