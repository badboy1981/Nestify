using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GateSystem3
{
    internal class UpdateKeyUI : MonoBehaviour
    {
        [SerializeField] GateManagement gateManagement;
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
        void OnEnable() { gateManagement.gateEvent.OnKeyCollectedEvent += UpdateUI; }
        void OnDisable() { gateManagement.gateEvent.OnKeyCollectedEvent -= UpdateUI; }
        void UpdateUI(string keyTag)
        {
            KeyImages.Find(t => t.CompareTag(keyTag)).sprite = collectedSprite;
        }
    }
}