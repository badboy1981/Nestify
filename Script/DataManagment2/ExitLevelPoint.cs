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
        private void OnTriggerEnter(Collider other)
        {
            //if (SaveData.CoinCounter >= 100)
            if (true)
            {
                SaveJsonStream(SlotData());
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

        private void SaveJsonStream(Slot slots)
        {
            using (var _JsonStr = new StreamWriter(AppConstant.BasePath + SaveData.SlotID + AppConstant.JsonExtension))
            {
                _JsonStr.Write(JsonUtility.ToJson(slots, true));
            }
        }
    }
}