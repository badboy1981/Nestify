using Collectable;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

        Debug.Log(JsonStr());
    }

    private void SaveGameDataToJSON()
    {

    }

    private string JsonStr()
    {
        var SaveData = new SaveSystem.Data.SaveData()
        {
            
        };
        return JsonUtility.ToJson(_GameData);
    }
    //private void ConsolMsg(string msg)
    //{
    //    Debug.Log(msg);
    //}
}