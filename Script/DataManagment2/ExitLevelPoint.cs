using SaveSystem.Data;
using SaveSystem.Test;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveSystem.Data2.PlayableSceneList;

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

                SlotUnlockedLevelList Un = new();
                UnlockLevel UnLevel = new();
                //{
                //    SlotID=SaveData.SlotID,
                //    Level=PlayableSceneList.levelNumber(SaveData.SceneName)
                //};
                if (File.Exists(AppConstant.UnlockedLevelPathName))
                {
                    Un = JsonUtility.FromJson<SlotUnlockedLevelList>(JsonFileRW.Read(AppConstant.UnlockedLevelPathName));

                    UnLevel = Un.unlockLevel.FirstOrDefault(x => x.SlotID == SaveData.SlotID);
                    if (UnLevel != null)
                    { UnLevel.Level = PlayableSceneList.levelNumber(SaveData.SceneName); }
                }
                UnLevel.SlotID = SaveData.SlotID;
                UnLevel.Level = PlayableSceneList.levelNumber(SaveData.SceneName);

                JsonFileRW.Write(AppConstant.UnlockedLevelPathName, JsonUtility.ToJson(Un));



                SaveData.CoinBank += SaveData.CoinCounter;
                SaveData.CoinCounter = 0;
                SceneManager.LoadSceneAsync(PlayableSceneList.NextScene(SaveData.SceneName));
            }
            else
            {

            }
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