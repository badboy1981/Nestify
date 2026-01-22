using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveAndLoad
{
    [CreateAssetMenu(fileName = "SaveGame", menuName = "Game Data/Save Game")]
    public class SaveScript : ScriptableObject
    {
        private readonly Vector3 _Position;
        private readonly Quaternion _Angle;
        private readonly List<string> _KeyList;
        private readonly List<string> _CoinList;
        public SaveScript() { }
        public SaveScript(Vector3 Position, Quaternion Angle, List<string> KeyList, List<string> CoinList)
        {
            _Position = Position;
            _Angle = Angle;
            _KeyList = KeyList;
            _CoinList = CoinList;
        }
        private readonly string JsonPath = "/Script/SaveScript/PlayerData.json";

        private string SlotName()
        {
            return "";
        }
        private SaveSlot CreateSaveObject()
        {
            return new()
            {
                SlotName = "Slot01",
                playerData = new()
                {
                    Position = _Position,
                    Angle = _Angle,
                    KeyLists = String.Join(",", _KeyList),
                    CoinLists = String.Join(",", _CoinList)
                }
            };
        }
        public void Save()
        {
            SaveSlot Pdata = CreateSaveObject();
            using var PlayerData = new StreamWriter(Application.dataPath + JsonPath + "_" + Pdata.SlotName + ".json");
            PlayerData.Write(JsonUtility.ToJson(Pdata));

        }
        public void SaveTest2(Vector3 Position, Quaternion Angle, List<string> KeyList, List<string> CoinList)
        {
            var Pdata = new PlayerData()
            {
                Position = Position,
                Angle = Angle,
                KeyLists = String.Join(",", KeyList),
                CoinLists = String.Join(",", CoinList)
            };
            using var PlayerData = new StreamWriter(Application.dataPath + JsonPath);
            PlayerData.Write(JsonUtility.ToJson(Pdata, true));
        }
        public void SaveTest()
        {
            SaveSlot saveSlot = CreateSaveObject();
            SaveSlots saveSlots = new();

            saveSlots.slots.Add(saveSlot);
            saveSlots.slots.Add(saveSlot);

            using var PlayerData = new StreamWriter(Application.dataPath + JsonPath + "_" + saveSlot.SlotName + ".json");
            PlayerData.Write(JsonUtility.ToJson(saveSlot));
        }
    }

    [Serializable]
    public class PlayerData
    {
        public Vector3 Position;
        public Quaternion Angle;
        public string KeyLists;
        public string CoinLists;

        public PlayerData()
        {
            this.Position = Vector3.zero;
        }
    }
    public class SaveSlot
    {
        public string SlotName;
        public PlayerData playerData;
    }
    public class SaveSlots
    {
        public List<SaveSlot> slots;
    }
}