using System.Collections.Generic;
using Collectable.Gate;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatePropertyReset : MonoBehaviour
{
    [SerializeField] List<GateProperty> gateProperties;
    private void Start()
    {
        foreach (var gate in gateProperties)
        {
            gate.ActiveGateHandleState = false;
            gate.gateIsBusy = false;
            gate.AnimationWaitTime = LevelWaitTime(SceneManager.GetActiveScene().name);
            gate.keysLists = KeysLists();
        }
    }
    private KeysList KeyProperty(string KeyName)
    {
        return new()
        {
            KeyName = KeyName,
            Collected = false
        };
    }
    private List<KeysList> KeysLists()
    {
        string[] KeysName = { "A", "B", "C" };
        var list = new List<KeysList>();
        foreach (var Sign in KeysName)
        {
            list.Add(KeyProperty($"Key{Sign}"));
        }
        return list;
    }
    private readonly Dictionary<string, int> LevelWaitTimes = new()
    {
     { "B_Maze", 6 },
     { "C_Maze", 7 },
     { "D_Maze", 8 },
     { "E_Maze", 9 },
     { "F_Maze", 10 },
     { "G_Maze", 11 },
     { "H_Maze", 12 },
     { "I_Maze", 13 },
     { "J_Maze", 14 },
     { "K_Maze", 15 },
     { "L_Maze", 16 },
     { "M_Maze", 17 },
    };
    private int LevelWaitTime(string levelName)
    {
        return LevelWaitTimes.TryGetValue(levelName, out var time) ? time : 6;
    }
}