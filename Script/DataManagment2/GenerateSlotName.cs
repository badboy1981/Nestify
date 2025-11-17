using System;
using System.Collections.Generic;

namespace SaveSystem.Test
{
    public class GenerateSlotName
    {
        private readonly string[] SlotList = 
            {
            "Slot_1", 
            "Slot_2", 
            "Slot_3", 
            "Slot_4", 
            "Slot_5", 
            "Slot_6", 
            "Slot_7", 
            "Slot_8", 
            "Slot_9",
            "Slot_10" 
        };
        public string SlotName()
        {
            return A_GenerateAlotName.SlotName;
        }
        private ObjectSlotName A_GenerateAlotName
        {
            get
            {
                return new ObjectSlotName
                {
                    SlotName = SlotList[0]
                };
            }
        }
        private readonly Dictionary<string, bool> SlotLists = new()
        {
            { "Slot_1",false},
            { "Slot_2",false},
            { "Slot_3",false},
            { "Slot_4",false},
            { "Slot_5",false},
            { "Slot_6",false},
            { "Slot_7",false},
            { "Slot_8",false},
            { "Slot_9",false},
            { "Slot_10",false}
        };
    }

    public static class SlotLists
    {
        public readonly static string[] SlotList =
            {
            "Slot_1",
            "Slot_2",
            "Slot_3",
            "Slot_4",
            "Slot_5",
            "Slot_6",
            "Slot_7",
            "Slot_8",
            "Slot_9",
            "Slot_10" };
    }

    [Serializable]
    public class Slot
    {
        public int SlotIndex;
        public string SlotName;
    }

    [Serializable]
    public class PlayerData
    {
        public string ScenceName;
        public int CoinCounter;
        public List<string> KeyLists;
    }
    [Serializable]
    public class ObjectSlotName
    {
        public string SlotName;
    }

    [Serializable]
    public class SavePlayerData
    {
        public Slot SlotInfo;
        public PlayerData playerData;
    }
}

namespace SaveSystem.Test
{
    public class CreatePlayerData
    {
        private int slotIndex;
        private string scenceName;
        private int coinCounter;
        private List<string> keysList;

        //public CreatePlayerData(string ScenceName, List<string> KeysList, int CoinCounter, int SlotIndex)
        //{
        //    scenceName = ScenceName;
        //    keysList = KeysList;
        //    coinCounter = CoinCounter;
        //    slotIndex = SlotIndex;
        //}
        public CreatePlayerData(SavePlayerData PlayerData)
        {
            slotIndex = PlayerData.SlotInfo.SlotIndex;
            scenceName = PlayerData.playerData.ScenceName;
            coinCounter = PlayerData.playerData.CoinCounter;
            keysList = PlayerData.playerData.KeyLists;
        }
        public SavePlayerData Create()
        {
            return new SavePlayerData
            {
                SlotInfo = new()
                {
                    SlotIndex = slotIndex,
                    SlotName = SlotLists.SlotList[slotIndex],
                },
                playerData = new()
                {
                    ScenceName = scenceName,
                    CoinCounter = coinCounter,
                    KeyLists = keysList
                }
            };
        }
    }
}