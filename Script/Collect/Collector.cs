using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Collectable
{
    internal class Collector : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI CointText;
        [SerializeField] TextMeshProUGUI KeyText;
        
       internal DataManagment.PlayerData _PlayerData;

        //private Collector collector;

        //private string AtmID;

        private void Start()
        {            
            if (!string.IsNullOrEmpty(LoadStringFromJson()))
            {
                SetPlayerDataOnReLoad();
            }
        }
        private void Awake()
        {
            //collector = GetComponent<Collector>();
        }
        private void OnTriggerEnter(Collider other)
        {
            //string AtmID = other.name;
            OnTriggerEnter_M001(other);
            if (other.name.Contains("ATM"))
            {
                _PlayerData.SceneName = SceneManager.GetActiveScene().name;
                SaveJsonTest();
            }
        }
        private void OnTriggerEnter_M002(Collider other)
        {
            bool Checkcollectable = other.TryGetComponent<Collectable>(out var _Collectable);
            if (Checkcollectable)
            {
                _Collectable.Collect();
                Debug.Log(_Collectable.name);
                string cl = _Collectable.name.Remove(_Collectable.name.IndexOf('_', _Collectable.name.Length));
                string _name = _Collectable.name;
                switch (cl)
                {
                    case "Coin":

                        break;
                    case "Key":

                        break;
                }
                if (_Collectable.name.Contains("Key"))
                {
                    _PlayerData.KeyLists.Add(_Collectable.name);
                    return;
                }
            }
        }
        private void OnTriggerEnter_M001(Collider other)
        {            
            if (other.TryGetComponent<Collectable>(out var Collectable))
            {
                Collectable.Collect();
                _PlayerData.PlayerPosition = gameObject.transform.position;
                _PlayerData.PlayerRotation = gameObject.transform.rotation;

                string CollectedName = Collectable.GetComponent<Collider>().name;
                CollectedName = CollectedName.Remove(CollectedName.IndexOf('_'));
                int Counter;
                switch (CollectedName)
                {
                    case "Coin":
                        _PlayerData.CoinLists.Add(other.name);
                        Counter = _PlayerData.CoinLists.Count;
                        CointText.text = $"Coin: {Counter}";
                        break;
                    case "Key":
                        _PlayerData.KeyLists.Add(other.name);
                        Counter = _PlayerData.KeyLists.Count;
                        KeyText.text = $"Key: {Counter}";
                        break;
                }
            }
        }
        private void SaveJsonTest()
        {
            //string JsonPath = $"/Script/SaveScript/PlayerData.json";
            string JsonPath = "PlayerData.json";
            string Json = JsonUtility.ToJson(_PlayerData, true);
            if (!string.IsNullOrEmpty(Json))
            {
                using var sw = new StreamWriter(Application.dataPath + JsonPath);
                sw.Write(Json);
            }
        }
        public string LoadStringFromJson()
        {            
            //string JsonPath = "/Script/SaveScript/PlayerData.json";
            string JsonPath = "PlayerData.json";
            Debug.Log(Application.dataPath + JsonPath);
            using var PlayerData = new StreamReader(Application.dataPath + JsonPath);
            return PlayerData.ReadToEnd();
        }
        public void SetPlayerDataOnReLoad()
        {
            PlayerData PDataObject = JsonUtility.FromJson<PlayerData>(LoadStringFromJson()); ;
            transform.SetPositionAndRotation(PDataObject.PlayerPosition, PDataObject.PlayerRotation);

            //SaveAndLoad.PlayerData PDataObject=JsonUtility.FromJson<SaveAndLoad.PlayerData>(LoadStringFromJson());
            //transform.SetPositionAndRotation(PDataObject.Position, PDataObject.Angle);

            _PlayerData.CoinLists.AddRange(PDataObject.CoinLists);

            foreach (var coin in _PlayerData.CoinLists)
            {
                var CoinObj = GameObject.Find(coin);
                Destroy(CoinObj);
            }
            CointText.text = "Coin: " + _PlayerData.CoinLists.Count.ToString();

            _PlayerData.KeyLists.AddRange(PDataObject.KeyLists);

            foreach (var key in _PlayerData.KeyLists)
            {
                var KeyObj = GameObject.Find(key);
                Destroy(KeyObj);
            }
            KeyText.text = "Key: " + _PlayerData.KeyLists.Count.ToString();
        }
    }
}