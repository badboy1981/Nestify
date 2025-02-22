using System.IO;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMenu
{
    public class ButtonImage : MonoBehaviour
    {
        private SaveSlotTotalDataSObject SlotData;
        private ButtonImageList _ImageList;
        private Image _Image;

        private void Awake()
        {
            _ImageList = GetComponentInParent<ButtonPanel>().ImageList;
            SlotData = GetComponentInParent<ButtonPanel>().SlotData;
            _Image = GetComponent<Image>();

            string JsonFilePath = AppConstant.BasePath + name + AppConstant.JsonExtension;
            TMP_Text tm = GetComponentInChildren<Image>().GetComponentInChildren<TMP_Text>();

            if (File.Exists(JsonFilePath))
            {
                _Image.sprite = _ImageList.ContinueSprite;

                tm.text = $"{SlotData.UnlockLevels.Find(x => x.SlotID == name).Level}";
            }
            else
            {
                _Image.sprite = _ImageList.NewGameSprite;
                tm.text = "0";
            }
        }
    }
}