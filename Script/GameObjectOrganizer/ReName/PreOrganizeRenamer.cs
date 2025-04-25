using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using System.Text.RegularExpressions;
#endif

[ExecuteInEditMode]
public class PreOrganizeRenamer : MonoBehaviour
{
    public RenamePattern renamePattern;

#if UNITY_EDITOR
    public void ApplyRenamePatterns()
    {
        if (renamePattern == null || renamePattern.rules.Count == 0)
        {
            Debug.LogWarning("No rename pattern assigned!", this);
            return;
        }

        List<Transform> allChildren = new List<Transform>();
        GetAllChildren(transform, allChildren);

        Undo.RecordObjects(allChildren.ToArray(), "Bulk Rename Objects");

        // دیکشنری برای نگهداری شمارنده هر دسته
        Dictionary<string, int> categoryCounters = new Dictionary<string, int>();
        int renamedCount = 0;

        foreach (Transform child in allChildren)
        {
            foreach (var rule in renamePattern.rules)
            {
                if (TryRename(child, rule, ref categoryCounters, renamePattern.globalStartNumber, renamePattern.globalIncrement))
                {
                    renamedCount++;
                    EditorUtility.SetDirty(child.gameObject);
                    break;
                }
            }
        }

        Debug.Log($"Renamed {renamedCount} objects");
    }

    private bool TryRename(Transform child, RenamePattern.RenameRule rule, ref Dictionary<string, int> counters, int startNumber, int increment)
    {
        string currentName = child.name;

        // تبدیل الگو به regex
        string regexPattern = "^" +
            Regex.Escape(rule.sourcePattern)
                .Replace("\\*", ".*?")    // * برای چند کاراکتر
                .Replace("\\?", ".")      // ? برای تک کاراکتر
            + "$";

        var match = Regex.Match(currentName, regexPattern);
        if (!match.Success) return false;

        string categoryKey = rule.sourcePattern;
        if (!counters.ContainsKey(categoryKey))
        {
            counters[categoryKey] = startNumber;
        }

        string newName = rule.targetPattern
            .Replace("{0}", counters[categoryKey].ToString())
            .Replace("{1}", match.Groups.Count > 1 ? match.Groups[1].Value : "");

        child.name = renamePattern.globalPrefix + newName + renamePattern.globalSuffix;
        counters[categoryKey] += increment;
        return true;
    }
    //private bool TryRename(Transform child, RenamePattern.RenameRule rule, ref Dictionary<string, int> counters, int startNumber, int increment)
    //{
    //    string currentName = child.name;
    //    string pattern = "^" + Regex.Escape(rule.sourcePattern).Replace("\\*", "(.*?)") + "$";
    //    var match = Regex.Match(currentName, pattern);
    //    if (!match.Success) return false;

    //    // شناسایی دسته بر اساس الگوی منبع
    //    string categoryKey = rule.sourcePattern;

    //    // مقداردهی اولیه شمارنده اگر وجود نداشته باشد
    //    if (!counters.ContainsKey(categoryKey))
    //    {
    //        counters[categoryKey] = startNumber;
    //    }

    //    // ساخت نام نهایی
    //    string newName = rule.targetPattern
    //        .Replace("{0}", counters[categoryKey].ToString())
    //        .Replace("{1}", match.Groups.Count > 1 ? match.Groups[1].Value : "");

    //    child.name = renamePattern.globalPrefix + newName + renamePattern.globalSuffix;

    //    // افزایش شمارنده برای این دسته
    //    counters[categoryKey] += increment;
    //    return true;
    //}

    private void GetAllChildren(Transform parent, List<Transform> result)
    {
        foreach (Transform child in parent)
        {
            result.Add(child);
            GetAllChildren(child, result);
        }
    }
#endif
}