using UnityEngine;
using UnityEngine.SceneManagement;

namespace GateSystem3
{
    public static class FileProperty
    {
        public static string FilePath()
        {
            string Pt = "/MazeAssets/Scripts/Interactables/Puzzle/GatePuzzle/ComponentPosition/Data/";
            return $"{Application.dataPath}{Pt}";
        }
        public static string FileName(string gPuzzleName)
        {
            return $"{SceneManager.GetActiveScene().name}_{gPuzzleName}.json";
        }
    }
}
