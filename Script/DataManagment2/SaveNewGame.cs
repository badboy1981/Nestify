using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SaveSystem
{
    public class SaveNewGame : MonoBehaviour
    {
        [SerializeField] ObjectActiveSlot slot;
        //[SerializeField] GamkeObject SavePrefab;
        [SerializeField] Test.SavePlayerData NewPlayerData;   

        private void SaveActiveSlotNameSlot()
        {
            slot.ActiveSlot = new Test.GenerateSlotName().SlotName();
            NewPlayerData.SlotInfo.SlotIndex = 0;
            NewPlayerData.SlotInfo.SlotName = new Test.GenerateSlotName().SlotName();
            NewPlayerData.playerData.ScenceName = "B_Maze(2)";
            NewPlayerData.playerData.CoinCounter = 0;
            NewPlayerData.playerData.KeyLists.Clear();
        }
    }
}