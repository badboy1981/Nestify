#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
public class ProjectStructureBuilder
{
    private static readonly string rootPath = "Assets/MazeRoot";
    private static readonly string structureFile = "Assets/MazeRootTools/structure.json";

    [MenuItem("Tools/Build MazeRoot Structure")]
    public static void BuildStructure()
    {
        if (!File.Exists(structureFile))
        {
            Debug.LogError($"❌ Structure file not found: {structureFile}");
            return;
        }
        
        var json = File.ReadAllText(structureFile);
        Debug.Log(json);
        var structure = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

        CreateFolder(rootPath);
        CreateSubFolders(rootPath, structure);

        Debug.Log("✅ MazeRoot folder structure created successfully.");
    }

    private static void CreateFolder(string path)
    {
        if (!AssetDatabase.IsValidFolder(path))
        {
            string parent = Path.GetDirectoryName(path).Replace("\\", "/");
            string folderName = Path.GetFileName(path);
            AssetDatabase.CreateFolder(parent, folderName);
        }
    }
    private static void CreateSubFolders(string currentPath, Dictionary<string, object> structure)
    {
        foreach (var entry in structure)
        {
            string subPath = Path.Combine(currentPath, entry.Key).Replace("\\", "/");

            // فقط اگر مقدار یک دیکشنری یا لیست باشه، یعنی پوشه‌ست
            if (entry.Value is Dictionary<string, object> nestedDict)
            {
                CreateFolder(subPath);
                CreateSubFolders(subPath, nestedDict);
            }
            else if (entry.Value is List<object> nestedList)
            {
                CreateFolder(subPath);
                foreach (var item in nestedList)
                {
                    if (item is string folderName)
                    {
                        string leafPath = Path.Combine(subPath, folderName).Replace("\\", "/");
                        CreateFolder(leafPath);
                    }
                }
            }
            else
            {
                // اگر مقدار فایل باشه (مثل GameManager.cs)، نادیده گرفته می‌شه
                continue;
            }
        }
        AssetDatabase.Refresh();
    }
}
#endif
