#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameObjectOrganizer))]
public class GameObjectOrganizerEditor : Editor
{
    private SerializedProperty organizerDataProp;
    private SerializedProperty groupsParentProp;
    private SerializedProperty runOnStartProp;

    private void OnEnable()
    {
        organizerDataProp = serializedObject.FindProperty("organizerData");
        groupsParentProp = serializedObject.FindProperty("groupsParent");
        runOnStartProp = serializedObject.FindProperty("runOnStart");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Configuration", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(organizerDataProp);
        EditorGUILayout.PropertyField(runOnStartProp);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Runtime Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(groupsParentProp);

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space(15);
        if (GUILayout.Button("Organize Objects Now", GUILayout.Height(30)))
        {
            OrganizeObjects();
        }
    }

    private void OrganizeObjects()
    {
        GameObjectOrganizer organizer = (GameObjectOrganizer)target;

        if (organizer.organizerData == null)
        {
            EditorUtility.DisplayDialog("Error", "Please assign Organizer Data first!", "OK");
            return;
        }

        Undo.RegisterCompleteObjectUndo(organizer.gameObject, "Organize Objects");
        if (organizer.groupsParent != null)
        {
            Undo.RecordObject(organizer.groupsParent, "Organize Objects");
        }

        organizer.OrganizeObjects();

        EditorUtility.SetDirty(organizer);
        if (organizer.groupsParent != null)
        {
            EditorUtility.SetDirty(organizer.groupsParent);
        }
    }
}
#endif