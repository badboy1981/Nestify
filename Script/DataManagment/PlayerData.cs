using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagment
{
    [System.Serializable]
    public class PlayerData
    {
        public string SceneName;
        public string AtmID;
        public Vector3 PlayerPosition;
        public Quaternion PlayerRotation;
        public List<string> KeyLists;
        public List<string> CoinLists;
    }
    //public class 
}