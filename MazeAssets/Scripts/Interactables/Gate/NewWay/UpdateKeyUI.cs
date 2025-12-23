using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GateSystem
{
    public class UpdateKeyUI : MonoBehaviour
    {
        [SerializeField] GateSystemManager manager;
        [SerializeField] List<Image> KeyImages;
        [SerializeField] Sprite collectedSprite;
        [SerializeField] Sprite notCollectedSprite;
        void OnEnable() { manager.OnKeyCollected += UpdateUI; }
        void OnDisable() { manager.OnKeyCollected -= UpdateUI; }
        void UpdateUI(int keysCollected)
        {
            KeyImages[keysCollected - 1].sprite = collectedSprite;
        }
    }
}