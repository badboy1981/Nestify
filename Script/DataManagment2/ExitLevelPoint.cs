using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem.Data2
{
    public class ExitLevelPoint : MonoBehaviour
    {
        [SerializeField] SaveLevelDataSObject SaveData;
        [SerializeField] SaveSlotTotalDataSObject SlotTotalData;

        private void OnTriggerEnter(Collider other)
        {
            //if (SaveData.CoinCounter >= 100)
            if (true)
            {
                SaveJsonGameData(SlotData());
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
        private SlotLastGameLevel LastLevel()
        {
            return new SlotLastGameLevel
            {
                Slot1 = 2,
                Slot2 = 3,
                Slot3 = 3,
                Slot4 = 3,
                Slot5 = 3,
                Slot6 = 3
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
        private void SaveJsonSlotData(SlotLastGameLevel SlotLastGame)
        {
            var SlotData = new StreamWriter(AppConstant.BasePath + "UnlockedLevel" + AppConstant.JsonExtension);
            SlotData.Write(JsonUtility.ToJson(SlotLastGame));
        }
    }
}