using NUnit.Framework.Constraints;
using SaveSystem;
using SaveSystem.Data2;
using System.Collections.Generic;
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
        private void Awake()
        {
            string SlotName = GetComponentInParent<Image>().GetComponentInParent<Button>().name;
            
            UnlockedLevel = GetComponent<TextMeshProUGUI>();
            if (SlotData.UnlockLevels.Count > 0)
            {
                UnlockedLevel.text = $"Level: {SlotData.UnlockLevels.Find(x => x.SlotID == SlotName).Level}";
            }
            else
            {
                UnlockedLevel.text = $"Level: {1}";
            }
        }
    }
}