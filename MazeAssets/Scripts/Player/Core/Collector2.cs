using Assets.MazeAssets.Scripts.Parent;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Collectable
{
    public class Collector2 : Parent
    {
        [SerializeField] TextMeshProUGUI CoinBank;
        [SerializeField] TextMeshProUGUI CointText;
        [SerializeField] TextMeshProUGUI KeyText;
        [SerializeField] TextMeshProUGUI LevelNumber;
        [SerializeField] SaveSystem.SaveLevelDataSObject LevelData;

        protected override void Awake()
        {
            transform.SetPositionAndRotation(new Vector3(0, 0.5f, 0), Quaternion.identity);
            LevelData.SceneName = SceneManager.GetActiveScene().name;
            //LevelData.KeyList.Clear();
            LevelNumber.text = $"{SaveSystem.Data2.PlayableSceneList.levelNumber(SceneManager.GetActiveScene().name)}";
            InitCollectedText();
            RemoveCellectedKeys();
        }
        private void OnTriggerEnter(Collider other)
        {
            InitCollectedText();
        }
        private void InitCollectedText()
        {
            CoinBank.text = $"{LevelData.CoinBank}";
            CointText.text = $"{LevelData.CoinCounter}";
            KeyText.text = $"{LevelData.KeyList.Count}";

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