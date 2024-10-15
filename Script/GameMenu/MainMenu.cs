using SaveSystem;
using System;
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
        public void NewGame(string SlotID)
        {
            SaveLevelDataSObject.SlotID = SlotID;
            SaveLevelDataSObject.SceneName = SaveSystem.Data2.PlayableSceneList.StartScene();
            SaveLevelDataSObject.CoinCounter = 0;
            SaveLevelDataSObject.KeyList.Clear();
            SceneManager.LoadSceneAsync(SaveSystem.Data2.PlayableSceneList.StartScene());
        }
        public void SelectSaveSlot(string SlotName)
        {
            Debug.Log($"Slot Name: {SlotName}");
        }
        public void ExitGame()
        {
            Application.Quit();
        }
        public void ContinueGame()
        {

        }
    }
    public static class SlotList
    {
        public static Dictionary<int, string> SlotDic = new()
        {
            { 0,"Slot1" },
            { 0,"Slot2" },
            { 0,"Slot3" },
            { 0,"Slot4" },
            { 0,"Slot5" },
            { 0,"Slot6" },
        };
        public static void test()
        {
            string fg = SlotDic[0];
        }
    }
    public enum SlotS
    {
        Slot1, Slot2, Slot3, Slot4, Slot5, Slot6
    }
}