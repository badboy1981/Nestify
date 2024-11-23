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
    public class PlayerData
    {
        public string CurrentSceneName;
        public int LifeCounter;
        public int CoinBank;
        public int CoinCounter;
        public List<string> KeyList;
    }

    [Serializable]
    public class SlotUnlockedLevelList
    {
        public List<UnlockLevel> unlockLevel;

        public SlotUnlockedLevelList()
        {
            unlockLevel = new List<UnlockLevel>();
        }

        // متدی برای یافتن UnlockLevel بر اساس SlotID
        public UnlockLevel FindUnlockLevel(string slotID)
        {
            return unlockLevel.Find(x => x.SlotID == slotID);
        }

        // متدی برای افزودن یا به‌روزرسانی UnlockLevel
        public void AddOrUpdateUnlockLevel(string slotID, int level)
        {
            var existingUnlockLevel = FindUnlockLevel(slotID);
            if (existingUnlockLevel != null)
            {
                existingUnlockLevel.Level = level;
            }
            else
            {
                unlockLevel.Add(new UnlockLevel { SlotID = slotID, Level = level });
            }

        }
    }


    [Serializable]
    public class UnlockLevel
    {
        public string SlotID;
        public int Level;
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

        private readonly static Dictionary<string, int> LevelNumber = new()
        {
            { sceneList[0],1},
            { sceneList[1],2},
            { sceneList[2],3},
            { sceneList[3],4},
            { sceneList[4],5},
            { sceneList[5],6},
            { sceneList[6],7},
            { sceneList[7],8},
            { sceneList[8],9},
            { sceneList[9],10},
            { sceneList[10],11},
            { sceneList[11],12},
        };

        public static int levelNumber(string SceneName)
        {
            return LevelNumber[SceneName];
        }
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
        public enum SceneListEnum
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
        public static int SlotSceneID(string SlotID)
        {
            return (int)SceneListEnum.B_Maze;
        }
    }
}