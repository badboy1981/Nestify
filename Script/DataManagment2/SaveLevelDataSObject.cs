using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "SaveData", menuName = "Save System/Save Data")]

    public class SaveLevelDataSObject : ScriptableObject
    {
        public int SlotID;
        public string SceneName;
        public int CoinCounter;
        public List<string> KeyList;
    }
}