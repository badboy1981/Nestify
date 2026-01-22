using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GateSystem2
{
    public class UpdateKeyUI : MonoBehaviour
    {
        [SerializeField] GateManagment gateManagment;
        [SerializeField] List<Image> KeyImages;
        [SerializeField] Sprite collectedSprite;
        [SerializeField] Sprite notCollectedSprite;

        private void Start()
        {
            foreach (var key in KeyImages)
            {
                key.sprite = notCollectedSprite;
            }
        }
        void OnEnable() { gateManagment.gateEvent.OnKeyCollectedEvent += UpdateUI; }
        void OnDisable() { gateManagment.gateEvent.OnKeyCollectedEvent -= UpdateUI; }
        void UpdateUI(string keyTag)
        {
            //KeyImages[keysCollected - 1].sprite = collectedSprite;
            //var fg = KeyImages.Find(s => s.CompareTag(keyTag)).sprite;
            //KeyImages[0].CompareTag(keyTag);
            KeyImages.Find(t => t.CompareTag(keyTag)).sprite = collectedSprite;
            //foreach (var key in KeyImages)
            //{
            //    if (key.CompareTag(keyTag))
            //    {
            //        key.sprite = collectedSprite;
            //    }
            //}
        }
    }
}