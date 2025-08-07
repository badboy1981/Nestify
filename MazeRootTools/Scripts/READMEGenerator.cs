#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
//using Newtonsoft.Json;

public class READMEGenerator
{
    private static readonly string rootPath = "Assets/MazeRoot";
    private static readonly string langPath = "Assets/MazeRootTools/Localization";

    [MenuItem("Tools/Generate README Files")]
    public static void GenerateReadmes()
    {
        string language = "fa"; // یا "en" یا هر زبان دیگه
        string langFile = Path.Combine(langPath, $"{language}.json");

        if (!File.Exists(langFile))
        {
            Debug.LogError($"❌ Language file not found: {langFile}");
            return;
        }

        //var descriptions = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(langFile));
        var descriptions = JsonUtility.FromJson<Dictionary<string, string>>(File.ReadAllText(langFile));

        foreach (var entry in descriptions)
        {
            string folderPath = Path.Combine(rootPath, entry.Key);
            string readmePath = Path.Combine(folderPath, "README.md");

            if (Directory.Exists(folderPath))
            {
                File.WriteAllText(readmePath, $"# {entry.Key}{entry.Value} ");
                Debug.Log($"✅ README.md created for {entry.Key}");
            }
            else
            {
                Debug.LogWarning($"⚠️ Folder not found: {folderPath}");
            }
        }

        AssetDatabase.Refresh();
    }
}
#endif
