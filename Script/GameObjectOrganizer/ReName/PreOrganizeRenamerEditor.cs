#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PreOrganizeRenamer))]
public class PreOrganizeRenamerEditor : Editor
{
    private SerializedProperty renamePatternProp;

    private void OnEnable()
    {
        renamePatternProp = serializedObject.FindProperty("renamePattern");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(renamePatternProp);

        if (renamePatternProp.objectReferenceValue != null)
        {
            RenamePattern pattern = (RenamePattern)renamePatternProp.objectReferenceValue;

            EditorGUILayout.HelpBox(
                $"Counter Settings:\n" +
                $"Start Number: {pattern.globalStartNumber}\n" +
                $"Increment: {pattern.globalIncrement}\n\n" +
                $"Naming Format:\n" +
                $"{pattern.globalPrefix}[TargetPattern]{pattern.globalSuffix}",
                MessageType.Info);
        }

        EditorGUILayout.Space(15);
        if (GUILayout.Button("APPLY RENAMING WITH GLOBAL COUNTER", GUILayout.Height(40)))
        {
            ((PreOrganizeRenamer)target).ApplyRenamePatterns();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif