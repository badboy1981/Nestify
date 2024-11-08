using System;
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
        public string SlotID;
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
        public int LifeCounter;
        public int CoinBank;
        public int CoinCounter;        
        public List<string> KeyList;
    }

    [Serializable]
    public class SlotLastGameLevel
    {
        public int Slot1 = 1;
        public int Slot2 = 1;
        public int Slot3 = 1;
        public int Slot4 = 1;
        public int Slot5 = 1;
        public int Slot6 = 1;
    }

    public static class PlayableSceneList
    {
        private static string[] sceneList =
            {
            "B_Maze",
            "C_Maze",
            "D_Maze",
            "E_Maze",
            "F_Maze",
            "G_Maze",
            "H_Maze",
            "I_Maze",
            "J_Maze",
            "K_Maze",
            "L_Maze",
            "M_Maze"
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
        private enum SceneListEnum
        {
            B_Maze = 1,
            C_Maze = 2,
            D_Maze = 3,
            E_Maze = 4,
            F_Maze = 5,
            G_Maze = 6,
            H_Maze = 7,
            I_Maze = 8,
            J_Maze = 9,
            K_Maze = 10,
            L_Maze = 11,
            M_Maze = 12
        }
        public static int SceneID(string ID)
        {
            return (int)SceneListEnum.B_Maze;
        }
    }
}