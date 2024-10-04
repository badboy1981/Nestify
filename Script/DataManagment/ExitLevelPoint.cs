using Collectable;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevelPoint : MonoBehaviour
{
    [SerializeField] SaveSystem.SaveLevelDataSObject _GameData;
    private void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent<Collector>(out var collector))
        //{
        //    ConsolMsg(collector.name);
        //    ConsolMsg(collector._PlayerData.CoinLists.Count.ToString());
        //    ConsolMsg(string.Join(',', collector._PlayerData.KeyLists));
        //}


        //_GameData.SceneName = SceneManager.GetActiveScene().name;
        //_GameData.KeyList.Clear();
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);

        var dataPath = new Me.Temp.AppPathData();
        SaveJsonStream("ApplicationPathData.json", dataPath.ToJSON());
        Debug.Log(dataPath.ToJSON());
    }

    private string JsonStr()
    {
        return JsonUtility.ToJson(saveData());
    }
    private SaveSystem.Data.SaveData saveData()
    {
        return new SaveSystem.Data.SaveData()
        {
            SlotID = new SaveSystem.Data.SlotIDs() { SlotID = _GameData.SlotID },
            scenceName = new SaveSystem.Data.ScenceName() { Name = _GameData.SceneName },
            playerData = new SaveSystem.Data.PlayerData()
            {
                CoinCounter = _GameData.CoinCounter,
                KeyLists = _GameData.KeyList
            }
        };
    }

    private void SaveJSON(string FileName, string JsonStr)
    {
        File.WriteAllText("", JsonUtility.ToJson(saveData(), true));
    }
    private void SaveJsonStream(string FileName, string JsonStr)
    {
        using (var _JsonStr = new StreamWriter($"{Application.dataPath}/{FileName}"))
        {
            _JsonStr.Write(JsonStr);
        }
    }
    //private void ConsolMsg(string msg)
    //{
    //    Debug.Log(msg);
    //}    
}

namespace Me.Temp
{
    public class AppPath
    {
        public string _consoleLogPath;
        public string _dataPath;
        public string _persistentDataPath;
        public string _streamingAssetsPath;
        public string _temporaryCachePath;
    }
    public class AppPathData
    {
        public AppPath appPath()
        {
            return new AppPath()
            {
                _consoleLogPath = Application.consoleLogPath,
                _dataPath = Application.dataPath,
                _persistentDataPath = Application.persistentDataPath,
                _streamingAssetsPath = Application.streamingAssetsPath,
                _temporaryCachePath = Application.temporaryCachePath
            };
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(appPath(), true);
        }
    }
}