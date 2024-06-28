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

        [SerializeField] PlayerData _PlayerData;
        private Collector collector;

        private void Start()
        {
            if (!string.IsNullOrEmpty(LoadStringFromJson()))
            {
                SetPlayerDataOnReLoad();
            }
        }
        private void Awake()
        {
            collector= GetComponent<Collector>();
            //KeyList = new List<string>();
            //CoinList = new List<string>();
        }
        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnter_M001(other);
            if (other.name.Contains("ATM"))
            {
                SaveJsonTest();
            }
        }
        private void SaveJsonTest()
        {
            string JsonPath = "/Script/SaveScript/PlayerData2.json";
            string Json = JsonUtility.ToJson(playerData(), true);
            if (!string.IsNullOrEmpty(Json))
            {
                using var sw = new StreamWriter(Application.dataPath + JsonPath);
                sw.Write(Json);
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
                    playerData().KeyLists.Add(_Collectable.name);
                    return;
                }
            }
        }
        private PlayerData playerData()
        {
            return _PlayerData = new PlayerData()
            {
                PlayerPosition = transform.position,
                PlayerRotation = transform.rotation,
                //CoinLists = collector.CoinList,
                CoinLists = _PlayerData.CoinLists,
                //KeyLists = collector.KeyList,
                KeyLists = _PlayerData.KeyLists,
                //CoinValet = CoinList.Count,
                CoinValet = _PlayerData.CoinLists.Count,
                //KeyValet = KeyList.Count,
                KeyValet = _PlayerData.KeyLists.Count,
            };
        }
        private void OnTriggerEnter_M001(Collider other)
        {
            int Counter;
            if (other.TryGetComponent<Collectable>(out var Collectable))
            {
                Collectable.Collect();
                string CollectedName = Collectable.GetComponent<Collider>().name;

                CollectedName = CollectedName.Remove(CollectedName.IndexOf('_'));
                switch (CollectedName)
                {
                    case "Coin":
                        _PlayerData.CoinLists.Add(other.name);
                        Counter = _PlayerData.CoinLists.Count;
                        _PlayerData.CoinValet = Counter;
                        CointText.text = $"Coin: {Counter}";
                        break;
                    case "Key":
                        _PlayerData.KeyLists.Add(other.name);
                        Counter = _PlayerData.KeyLists.Count;
                        _PlayerData.KeyValet = Counter;
                        KeyText.text = $"Key: {Counter}";
                        break;
                }
                //if (other.GetComponent<Collider>().name.Contains("Coin"))
                //{
                //    _PlayerData.CoinLists.Add(other.name);
                //    Counter = _PlayerData.CoinLists.Count;
                //    _PlayerData.CoinValet = Counter;
                //    CointText.text = $"Coin: {Counter}";
                //}
                //else if (other.GetComponent<Collider>().name.Contains("Key"))
                //{
                //    _PlayerData.KeyLists.Add(other.name);
                //    Counter = _PlayerData.KeyLists.Count;
                //    _PlayerData.KeyValet = Counter;
                //    KeyText.text = $"Key: {Counter}";
                //}
            }
        }
        public string LoadStringFromJson()
        {
            string JsonPath = "/Script/SaveScript/PlayerData2.json";
            using var PlayerData = new StreamReader(Application.dataPath + JsonPath);
            return PlayerData.ReadToEnd();
        }
        public void SetPlayerDataOnReLoad()
        {

            PlayerData PDataObject = JsonUtility.FromJson<PlayerData>(LoadStringFromJson()); ;
            transform.SetPositionAndRotation(PDataObject.PlayerPosition, PDataObject.PlayerRotation);

            //SaveAndLoad.PlayerData PDataObject=JsonUtility.FromJson<SaveAndLoad.PlayerData>(LoadStringFromJson());
            //transform.SetPositionAndRotation(PDataObject.Position, PDataObject.Angle);

            //CoinList.AddRange(PDataObject.CoinLists.Split(','));
            playerData().CoinLists.AddRange(PDataObject.CoinLists);

            //foreach (var coin in CoinList)
            foreach (var coin in _PlayerData.CoinLists)
            {
                var CoinObj = GameObject.Find(coin);
                Destroy(CoinObj);
            }
            //CointText.text = "Coin: " + CoinList.Count.ToString();
            CointText.text = "Coin: " + _PlayerData.CoinLists.Count.ToString();

            //KeyList.AddRange(PDataObject.KeyLists.Split(','));
            playerData().KeyLists.AddRange(PDataObject.KeyLists);

            //foreach (var key in KeyList)
            foreach (var key in _PlayerData.KeyLists)
            {
                var KeyObj = GameObject.Find(key);
                Destroy(KeyObj);
            }
            //KeyText.text = "Key: " + KeyList.Count.ToString();
            KeyText.text = "Key: " + _PlayerData.KeyLists.Count.ToString();
        }
    }
}