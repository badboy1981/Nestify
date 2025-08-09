#if  UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoundPosition))]
public class RoundPositionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoundPosition rounder = (RoundPosition)target;

        if (GUILayout.Button("🔘 Round Children Positions"))
        {
            rounder.RoundChildrenPositions();
        }
    }
}
#endif