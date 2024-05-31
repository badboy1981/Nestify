using System;
using System.IO;
using UnityEngine;
using Collectable;

public class PlayerDataReadWriteJSON : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Collector KeyCollectedName;
    private string JsonPath = "/Script/SaveScript/PlayerData.json";
    public void SavePlayerData()
    {
        KeyCollectedName = Player.GetComponent<Collector>();
        string Keys = String.Join(",", KeyCollectedName.CollectableName);

        var data = new PlayerData()
        {
            PlayerPosition = Player.transform.position,
            PlayerRotation = Player.transform.rotation,
            keyLists = Keys
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
        KeyCollectedName = Player.GetComponent<Collector>();
        return new()
        {
            PlayerPosition = Player.transform.position,
            PlayerRotation = Player.transform.rotation,
            keyLists = String.Join("'", KeyCollectedName.CollectableName),
            CoinValet = 10
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
        if (!string.IsNullOrEmpty(CreatePlayerJsonData()))
        {
            using var PlayerData = new StreamWriter(Application.dataPath + JsonPath);
            PlayerData.Write(CreatePlayerJsonData());
        }
    }
    
    public string LoadStringFromJson()
    {
        using var PlayerData = new StreamReader(Application.dataPath + JsonPath);
        return PlayerData.ReadToEnd();
    }
    public void LoadPlayerDataFromJson()
    {
        PDataObject = JsonUtility.FromJson<PlayerData>(LoadStringFromJson());
    }

    private PlayerData PDataObject;

    public void SetPlayerDataOnReLoad()
    {
        LoadPlayerDataFromJson();
        Player.transform.SetPositionAndRotation(PDataObject.PlayerPosition, PDataObject.PlayerRotation);
        int CoinCounter = PDataObject.CoinValet;
    }
}