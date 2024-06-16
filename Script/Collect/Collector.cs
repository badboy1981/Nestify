using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace Collectable
{
    public class Collector : MonoBehaviour
    {
        public TextMeshProUGUI CointText;
        public TextMeshProUGUI KeyText;
        public int CoinCounter = 0;
        public int KeyCounter = 0;
        public List<string> KeyList;
        public List<string> CoinList;

        private void Start()
        {
            if (!string.IsNullOrEmpty(LoadStringFromJson()))
            {
                SetPlayerDataOnReLoad();
            }
        }
        private void Awake()
        {
            KeyList = new List<string>();
            CoinList = new List<string>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Collectable>(out var Collectable))
            {
                Collectable.Collect();
                if (other.GetComponent<Collider>().name.Contains("Coin"))
                {
                    //_Animator.SetBool("SpeedUp", true);
                    CoinCounter++;
                    CointText.text = "Coin: " + CoinCounter;
                    CoinList.Add(other.name);
                }
                if (other.GetComponent<Collider>().name.Contains("Key"))
                {
                    KeyCounter++;
                    KeyList.Add(other.name);
                    KeyText.text = "Key: " + KeyCounter;
                }
            }
        }
        public string LoadStringFromJson()
        {
            string JsonPath = "/Script/SaveScript/PlayerData.json";
            using var PlayerData = new StreamReader(Application.dataPath + JsonPath);
            return PlayerData.ReadToEnd();
        }
        public void SetPlayerDataOnReLoad()
        {

            //PlayerData PDataObject = JsonUtility.FromJson<PlayerData>(LoadStringFromJson()); ;
            //transform.SetPositionAndRotation(PDataObject.PlayerPosition, PDataObject.PlayerRotation);
            
            SaveAndLoad.PlayerData PDataObject=JsonUtility.FromJson<SaveAndLoad.PlayerData>(LoadStringFromJson());
            transform.SetPositionAndRotation(PDataObject.Position, PDataObject.Angle);

            CoinList.AddRange(PDataObject.CoinLists.Split(','));
            foreach (var coin in CoinList)
            {
                var CoinObj = GameObject.Find(coin);
                Destroy(CoinObj);
            }
            CointText.text = "Coin: " + CoinList.Count.ToString();

            KeyList.AddRange(PDataObject.KeyLists.Split(','));
            foreach (var key in KeyList)
            {
                var KeyObj = GameObject.Find(key);
                Destroy(KeyObj);
            }
            KeyText.text = "Key: " + KeyList.Count.ToString();
        }
    }
}