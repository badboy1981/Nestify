using SaveSystem;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
//using UnityEngine.Windows;
using System.IO;
using TMPro;

namespace GameMenu
{
    public class ButtonText : MonoBehaviour
    {
        [SerializeField] Button _Button;
        //[SerializeField] Texture Start;
        //[SerializeField] Texture Continue;
        [SerializeField] TextMeshProUGUI BtText;
        //RawImage _RawImage;
        Image _RawImage;

        private void Awake()
        {
            string JsonFilePath = AppConstant.BasePath + _Button.name + AppConstant.JsonExtension;
            //gameObject.AddComponent<RawImage>();
            _RawImage = GetComponent<Image>();

            if (File.Exists(JsonFilePath))
            {
                BtText.text = "Continue";
                //_RawImage.sprite = Continue;
            }
            else
            {
                BtText.text = "Start";
                //_RawImage.texture = Start;
            }
        }
    }
}