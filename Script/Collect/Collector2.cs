using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Collectable
{
    public class Collector2 : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI CointText;
        [SerializeField] TextMeshProUGUI KeyText;
        [SerializeField] SaveSystem.SaveLevelDataSObject LevelData;

        private void Awake()
        {
            transform.SetPositionAndRotation(new Vector3(0, 0.5f, 0), Quaternion.identity);
            InitCollectedText();
            RemoveCellectedKeys();
        }
        private void OnTriggerEnter(Collider other)
        {
            InitCollectedText();
        }
        private void InitCollectedText()
        {
            CointText.text = $"Coin: {LevelData.CoinCounter}";
            KeyText.text = $"Key: {LevelData.KeyList.Count}";
        }
        private void RemoveCellectedKeys()
        {
            foreach (var Item in LevelData.KeyList)
            {
                var CollectedKey = GameObject.Find(Item);
                Destroy(CollectedKey);
            }
        }
    }
}