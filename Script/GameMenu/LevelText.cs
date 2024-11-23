using NUnit.Framework.Constraints;
using SaveSystem;
using SaveSystem.Data2;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;


namespace GameMenu
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField] SaveSlotTotalDataSObject SlotData;
        //[SerializeField] Button SlotButton;
        TextMeshProUGUI UnlockedLevel;
   
        private void Start()
        {
            string SlotName = GetComponentInParent<Image>().GetComponentInParent<Button>().name;

            UnlockedLevel = GetComponent<TextMeshProUGUI>();
            //if (SlotData.UnlockLevels.Count > 0)
            //if (SlotData.SlotList.Count > 0)

            if (SlotExist(SlotName))
            //if(true)
            {
                //string lev = SlotData.SlotList.Find(x => x.unlockLevel.);
                UnlockedLevel.text = $"Level: {SlotData.UnlockLevels.Find(x => x.SlotID == SlotName).Level}";

            }
            else
            {
                UnlockedLevel.text = $"Level: 1";
            }
        }
        private bool SlotExist(string SlotName)
        {
            //var un = GetComponent<UnlockLevel>();
            UnlockLevel un = new();
            un = SlotData.UnlockLevels.FirstOrDefault(x => x.SlotID == SlotName);

            if (un != null) return true;
            return false;
        }
    }
}