using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] SaveLevelDataSObject SaveLevelDataSObject;

        public void StartGame()
        {

        }
        public void NewGame()
        {
            SaveLevelDataSObject.SlotID = SelectSlot.Select();
            SaveLevelDataSObject.SceneName = "B_Maze(2)";
            SaveLevelDataSObject.CoinCounter = 0;
            SaveLevelDataSObject.KeyList.Clear();
            SceneManager.LoadSceneAsync("B_Maze(2)");
        }
        public void ContinueGame()
        {

        }
        private void CreateNewSaveSlot()
        {

        }
        private void Test()
        {
            DataTest.ScriptableObjectDataTEST ts = new();
            ts.CoinCounter = 0;
        }
    }
}