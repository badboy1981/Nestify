using TMPro;
using UnityEngine;

namespace GameMenu
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ProgressBar : MonoBehaviour
    {
        public TextMeshProUGUI itemCounterText;
        public Collectable.CollectableEventManagement collectetedManagement;
        public int itemCounter;
        public SaveSystem.SaveLevelDataSObject LevelData;

        private void Awake()
        {
            itemCounterText = GetComponent<TextMeshProUGUI>();
            itemCounter = 1;
        }
    }
}