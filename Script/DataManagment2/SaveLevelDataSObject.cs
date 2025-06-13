using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "SaveData", menuName = "Save System/Save Data")]

    public class SaveLevelDataSObject : ScriptableObject
    {
        public string SlotID;
        public string SceneName;
        public int LifeCounter;
        public int CoinBank;
        public int CoinCounter;
        public List<string> KeyList;
        public List<string> CollectedGateActivatorListKey;
    }
}