using System;
using System.Collections.Generic;

namespace SaveSystem.Data
{

    [Serializable]
    public class SlotIDs
    {
        public int SlotID;
    }

    [Serializable]
    public class ScenceName
    {
        public string Name;
    }

    [Serializable]
    public class PlayerData
    {
        public int CoinCounter;
        public List<string> KeyLists;
    }

    [Serializable]
    public class SaveData
    {
        public SlotIDs SlotID;
        public ScenceName scenceName;
        public PlayerData playerData;
    }
}