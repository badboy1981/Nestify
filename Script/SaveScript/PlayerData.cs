using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string SceneName;
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public List<string> KeyLists;    
    public List<string> CoinLists;
}