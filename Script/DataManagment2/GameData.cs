using System;
using System.Collections;
using System.Collections.Generic;

namespace SaveSystem.Data2
{
    [Serializable]
    public class Slots
    {
        public List<Slot> slots;
    }

    [Serializable]
    public class Slot
    {
        public string Name;
        public PlayerData playerData;
    }

    [Serializable]
    public class UnlockSceneList
    {
        public List<string> SceneList;
    }

    [Serializable]
    public class PlayerData
    {
        public string CurrentSceneName;
        public int CoinCounter;
        public int LifeCounter;
        public List<string> KeyList;
    }
    public static class PlayableSceneList
    {
        private static string[] sceneList =
            {
            "B_Maze(2)",
            "C_Maze(6)",
            "D_Maze(9)",
            "E_Maze(3)",
            "F_Maze(10)",
            "G_Maze(11)",
            "H_Maze(1)",
            "I_Maze(5)",
            "J_Maze(7)",
            "K_Maze(4)",
            "L_Maze(8)",
            "M_Maze(12)"
            };
        public static string StartScene()
        {
            return sceneList[0];
        }
        public static string CurrentScene(string CurrentSceneName)
        {
            //var index = Array.FindIndex(sceneList, row => row.Contains(SceneName));
            return sceneList[Array.FindIndex(sceneList, row => row.Contains(CurrentSceneName))];
        }
        public static string NextScene(string CurrentSceneName)
        {
            return sceneList[Array.FindIndex(sceneList, row => row.Contains(CurrentSceneName)) + 1];
        }
    }
}