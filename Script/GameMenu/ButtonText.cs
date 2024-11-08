using SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace GameMenu
{
    public class ButtonText : MonoBehaviour
    {
        [SerializeField] Button _Button;
        [SerializeField] Texture Start;
        [SerializeField] Texture Continue;
        RawImage _RawImage;

        private void Awake()
        {
            string JsonFilePath = AppConstant.BasePath + _Button.name + AppConstant.JsonExtension;
            //gameObject.AddComponent<RawImage>();
            _RawImage = GetComponent<RawImage>();

            if (File.Exists(JsonFilePath))
            {
                _RawImage.texture = Continue;
            }
            else
            {
                _RawImage.texture = Start;
            }
        }
    }
}