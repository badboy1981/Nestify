using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public List<string> KeyLists;
    public int KeyValet;
    public List<string> CoinLists;
    public int CoinValet;
    //public string KeyLists;    
    //public string CoinLists;
}