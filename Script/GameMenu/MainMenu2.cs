using SaveSystem;
using SaveSystem.Data2;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMenu
{
    public class MainMenu2 : MonoBehaviour
    {
        [SerializeField] SaveLevelDataSObject saveLevelData;
        [SerializeField] SaveSlotTotalDataSObject SlotData;

        private void Awake()
        {
            SlotData.UnlockLevels.Clear();
            if (File.Exists(AppConstant.UnlockedLevelPathName))
            {
                var UnlockLevelData = JsonUtility.FromJson<SlotUnlockedLevelList>(JsonFileRW.Read(AppConstant.UnlockedLevelPathName));

                foreach (var Item in UnlockLevelData.unlockLevel)
                {
                    SlotData.UnlockLevels.Add(Item);
                }
            }
        }
        public void NewGame(string SlotID)
        {
            saveLevelData.KeyList.Clear();
            string JsonFilePath = AppConstant.BasePath + SlotID + AppConstant.JsonExtension;
            if (File.Exists(JsonFilePath))
            {
                //Continu The Game
                var ActiveGameData = JsonUtility.FromJson<Slot>(JsonFileRW.Read(JsonFilePath));
                saveLevelData.SlotID = ActiveGameData.SlotID;
                saveLevelData.SceneName = ActiveGameData.playerData.CurrentSceneName;
                saveLevelData.LifeCounter = ActiveGameData.playerData.LifeCounter;
                saveLevelData.CoinBank = ActiveGameData.playerData.CoinBank;
                saveLevelData.CoinCounter = ActiveGameData.playerData.CoinCounter;
                SceneManager.LoadSceneAsync(ActiveGameData.playerData.CurrentSceneName, LoadSceneMode.Single);
            }
            else
            {
                //Start New Game
                saveLevelData.SlotID = SlotID;
                saveLevelData.SceneName = PlayableSceneList.StartScene();
                saveLevelData.LifeCounter = 3;
                saveLevelData.CoinBank = 0;
                saveLevelData.CoinCounter = 0;
                SceneManager.LoadSceneAsync(PlayableSceneList.StartScene(), LoadSceneMode.Single);
            }
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}