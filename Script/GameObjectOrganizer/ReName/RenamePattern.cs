using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RenamePattern", menuName = "Game Tools/Rename Pattern")]
public class RenamePattern : ScriptableObject
{
    [Header("Global Settings")]
    public string globalPrefix = "";
    public string globalSuffix = "";

    [Header("Counter Settings")]
    public int globalStartNumber = 1;
    public int globalIncrement = 1;

    [System.Serializable]
    public class RenameRule
    {
        //[Header("Source Pattern")]
        public string sourcePattern = "Old_*";

        //[Header("Target Pattern")]
        public string targetPattern = "New_{0}";

        //[Header("Example")]
        //public string exampleBefore = "Old_Sword";
        //public string exampleAfter = "PRE_New_1_SUF";
    }

    [Header("Rename Rules")]
    public List<RenameRule> rules = new List<RenameRule>();
}