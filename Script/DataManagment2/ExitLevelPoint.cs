using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem.Data2
{
    public class ExitLevelPoint : MonoBehaviour
    {
        [SerializeField] SaveLevelDataSObject SaveData;
        //[SerializeField] SaveSlotTotalDataSObject SlotTotalData;

        private void OnTriggerEnter(Collider other)
        {
            //if (SaveData.CoinCounter >= 100)
            if (true)
            {
                SaveJsonGameData(SlotData());
                SaveUnlockLevel();

                SceneManager.LoadSceneAsync(PlayableSceneList.NextScene(SaveData.SceneName));
            }
            else
            {
            }
        }
        private void SaveUnlockLevel()
        {
            SlotUnlockedLevelList UnLockLevelList = new();
            if (File.Exists(AppConstant.UnlockedLevelPathName))
            {
                UnLockLevelList = JsonUtility.FromJson<SlotUnlockedLevelList>(JsonFileRW.Read(AppConstant.UnlockedLevelPathName));
            }
            UnLockLevelList.AddOrUpdateUnlockLevel(SaveData.SlotID, PlayableSceneList.levelNumber(SaveData.SceneName));
            JsonFileRW.Write(AppConstant.UnlockedLevelPathName, JsonUtility.ToJson(UnLockLevelList, true));
        }
        private Slot SlotData()
        {
            return new Slot
            {
                SlotID = SaveData.SlotID,
                playerData = new PlayerData
                {
                    //CurrentSceneName = SaveData.SceneName,
                    CurrentSceneName = PlayableSceneList.NextScene(SaveData.SceneName),
                    LifeCounter = SaveData.LifeCounter,
                    CoinBank = SaveData.CoinBank + SaveData.CoinCounter,
                    CoinCounter = 0,
                    KeyList = SaveData.KeyList
                }
            };
        }
        private Slots slots()
        {
            return new Slots
            {
                slots = new List<Slot>
                {
                    new Slot
                    {
                        SlotID = SaveData.SlotID,
                        playerData = new PlayerData
                        {
                            CurrentSceneName = SaveData.SceneName,
                            CoinCounter = SaveData.CoinCounter,
                            LifeCounter = SaveData.LifeCounter,
                            KeyList = SaveData.KeyList,
                        }
                    }
                }
            };
        }

        private void SaveJsonGameData(Slot slots)
        {
            using var _JsonStr = new StreamWriter(AppConstant.BasePath + SaveData.SlotID + AppConstant.JsonExtension);
            _JsonStr.Write(JsonUtility.ToJson(slots, true));
        }
        private void SaveJsonSlotData(SlotUnlockedLevelList SlotLastGame)
        {
            using var SlotData = new StreamWriter(AppConstant.BasePath + AppConstant.UnlockedLevel + AppConstant.JsonExtension);
            SlotData.Write(JsonUtility.ToJson(SlotLastGame, true));
        }
        private void Test()
        {

        }
        private void Test2()
        {
            switch (SaveData.SlotID)
            {
                case "":
                    break;
            }
        }
    }
}