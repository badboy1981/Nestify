using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameObjectOrganizer : MonoBehaviour
{
    [Header("Configuration")]
    public ObjectOrganizerData organizerData;

    [Header("Runtime Settings")]
    [Tooltip("Parent transform for organized groups")]
    public Transform groupsParent;

    [Tooltip("Should the organizer run automatically on Start?")]
    public bool runOnStart = true;

    private Dictionary<string, GameObject> replacementDictionary;

    #region Initialization
    private void Start()
    {
        if (runOnStart && Application.isPlaying)
        {
            OrganizeObjects();
        }
    }

    private void Initialize()
    {
        if (organizerData == null)
        {
            Debug.LogError("Organizer Data is not assigned!", this);
            return;
        }

        CreateGroupsParentIfNeeded();
        BuildReplacementDictionary();
    }

    private void CreateGroupsParentIfNeeded()
    {
        if (groupsParent == null)
        {
            groupsParent = new GameObject("ObjectGroups").transform;
            groupsParent.SetParent(transform);
            groupsParent.localPosition = Vector3.zero;
            groupsParent.localRotation = Quaternion.identity;

#if UNITY_EDITOR
            UnityEditor.Undo.RegisterCreatedObjectUndo(groupsParent.gameObject, "Create Groups Parent");
#endif
        }
    }

    private void BuildReplacementDictionary()
    {
        replacementDictionary = new Dictionary<string, GameObject>();

        if (organizerData.prefabReplacements != null)
        {
            foreach (var replacement in organizerData.prefabReplacements)
            {
                if (replacement.prefab != null && !string.IsNullOrEmpty(replacement.keyword))
                {
                    string key = replacement.keyword.ToLower();
                    if (!replacementDictionary.ContainsKey(key))
                    {
                        replacementDictionary.Add(key, replacement.prefab);
                    }
                }
            }
        }
    }
    #endregion

    #region Public Methods
    public void OrganizeObjects()
    {
        Initialize();

        if (organizerData == null || organizerData.keywords == null)
            return;

        foreach (string keyword in organizerData.keywords)
        {
            if (string.IsNullOrEmpty(keyword))
                continue;

            ProcessKeyword(keyword.ToLower());
        }
    }

    public void SetOrganizerData(ObjectOrganizerData newData)
    {
        organizerData = newData;
        Initialize();
    }
    #endregion

    #region Processing Methods
    private void ProcessKeyword(string keyword)
    {
        Transform keywordGroup = CreateOrFindKeywordGroup(keyword);
        ProcessMatchingChildren(keyword, keywordGroup);
    }

    private Transform CreateOrFindKeywordGroup(string keyword)
    {
        string groupName = $"{keyword}_Group";
        Transform group = groupsParent.Find(groupName);

        if (group == null)
        {
            GameObject newGroup = new GameObject(groupName);
            group = newGroup.transform;
            group.SetParent(groupsParent);
            group.localPosition = Vector3.zero;
            group.localRotation = Quaternion.identity;

#if UNITY_EDITOR
            UnityEditor.Undo.RegisterCreatedObjectUndo(newGroup, "Create Keyword Group");
#endif
        }

        return group;
    }

    private void ProcessMatchingChildren(string keyword, Transform targetParent)
    {
        List<Transform> childrenToProcess = new List<Transform>();

        foreach (Transform child in transform)
        {
            if (child != targetParent && child != groupsParent &&
                child.name.ToLower().Contains($"_{keyword}_"))
            {
                childrenToProcess.Add(child);
            }
        }

        foreach (Transform child in childrenToProcess)
        {
            if (replacementDictionary.TryGetValue(keyword, out GameObject prefab))
            {
                ReplaceWithPrefab(child, prefab, targetParent);
            }
            else
            {
                MoveToGroup(child, targetParent);
            }
        }
    }
    #endregion

    #region Object Operations
    private void ReplaceWithPrefab(Transform original, GameObject prefab, Transform newParent)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            GameObject newObj = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            SetupNewObject(original, newObj.transform, newParent);
            UnityEditor.Undo.DestroyObjectImmediate(original.gameObject);
            return;
        }
#endif

        GameObject runtimeObj = Instantiate(prefab, newParent);
        SetupNewObject(original, runtimeObj.transform, newParent);
        Destroy(original.gameObject);
    }

    private void MoveToGroup(Transform obj, Transform newParent)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            UnityEditor.Undo.RecordObject(obj, "Move Object to Group");
        }
#endif

        obj.SetParent(newParent);
    }

    private void SetupNewObject(Transform source, Transform target, Transform parent)
    {
        target.SetParent(parent);
        target.position = source.position;
        target.rotation = source.rotation;
        target.localScale = source.localScale;
        target.name = source.name;

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            UnityEditor.Undo.RegisterCreatedObjectUndo(target.gameObject, "Create Replacement Object");
        }
#endif
    }
    #endregion
}