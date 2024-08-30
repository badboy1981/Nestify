using Collectable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevelPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Collector>(out var collector))
        {
            ConsolMsg(collector.name);
            ConsolMsg(collector._PlayerData.CoinLists.Count.ToString());
            ConsolMsg(string.Join(',', collector._PlayerData.KeyLists));
        }
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    private void ConsolMsg(string msg)
    {
        Debug.Log(msg);
    }
}