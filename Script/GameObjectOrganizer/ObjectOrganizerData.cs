using UnityEngine;

[CreateAssetMenu(fileName = "OrganizerData", menuName = "Game Tools/Object Organizer Data")]
public class ObjectOrganizerData : ScriptableObject
{
    [System.Serializable]
    public class PrefabReplacement
    {
        public string keyword;
        public GameObject prefab;
    }

    [Header("Organization Settings")]
    [Tooltip("Keywords for grouping objects (format: A_Keyword_001)")]
    public string[] keywords;

    [Header("Prefab Replacements")]
    [Tooltip("Prefabs to replace objects with")]
    public PrefabReplacement[] prefabReplacements;
}