using UnityEngine;

public static class AppConstant
{
    public static string BasePath { get { return $"{Application.persistentDataPath}/"; } }
    public static string JsonExtension { get { return ".json"; } }
    public static string JsonFilePath(string FileName)
    {
        return BasePath + FileName + JsonExtension;
    }
    public static string UnlockedLevel { get { return "UnlockedLevel"; } }
    public static string UnlockedLevelPathName { get { return BasePath + UnlockedLevel + JsonExtension; } }


    //public static Vector2 SafeArea = new(Screen.safeArea.width, Screen.safeArea.height);
    public static Vector2 SafeArea { get; private set; }

    [RuntimeInitializeOnLoadMethod]
    private static void InitializeSafeArea()
    {
        SafeArea = new Vector2(Screen.safeArea.width, Screen.safeArea.height);
    }
}