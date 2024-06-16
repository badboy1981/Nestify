using System;
using System.IO;
using UnityEngine;
using Collectable;
using TMPro;

public class PlayerDataReadWriteJSON : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Collector CollectedName;
    private string JsonPath = "/Script/SaveScript/PlayerData.json";
    public void SavePlayerData()
    {
        CollectedName = Player.GetComponent<Collector>();
        string Keys = String.Join(",", CollectedName.KeyList);
        string Coin = CollectedName.CoinCounter.ToString();
        var data = new PlayerData()
        {
            PlayerPosition = Player.transform.position,
            PlayerRotation = Player.transform.rotation,
            KeyLists = Keys
        };

        string Json = JsonUtility.ToJson(data, true);
        if (!string.IsNullOrEmpty(Json))
        {
            using var sw = new StreamWriter(Application.dataPath + JsonPath);
            sw.Write(Json);
        }
    }
    //#01
    public PlayerData CreatePlayerObjectData()
    {
        CollectedName = Player.GetComponent<Collector>();
        return new()
        {
            PlayerPosition = Player.transform.position,
            PlayerRotation = Player.transform.rotation,

            KeyValet = CollectedName.KeyCounter,
            KeyLists = String.Join(",", CollectedName.KeyList),

            CoinValet = CollectedName.CoinCounter,
            CoinLists = String.Join(",", CollectedName.CoinList)
        };
    }
    //#02
    public string CreatePlayerJsonData()
    {
        return JsonUtility.ToJson(CreatePlayerObjectData(), true);
    }
    //#03
    public void SaveJsonFile()
    {
        string PlayerDataJson = CreatePlayerJsonData();
        if (!string.IsNullOrEmpty(PlayerDataJson))
        {
            using var PlayerData = new StreamWriter(Application.dataPath + JsonPath);
            PlayerData.Write(PlayerDataJson);
        }
    }
}