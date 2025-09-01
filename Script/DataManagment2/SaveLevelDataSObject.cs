using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "SaveData", menuName = "Save System/Save Data")]

    public class SaveLevelDataSObject : ScriptableObject
    {
        public string SlotID;
        public string SceneName;
        public int LifeCounter;
        public int CoinBank;
        public int CoinCounter;
        //public float ChargeCapacityMax;
        //public float _ChargeStatus;
        public List<string> KeyList;
        public List<string> CollectedGateActivatorListKey;

        //public float ChargeStatus
        //{
        //    get => _ChargeStatus;
        //    //set => ChargeStatus = value;
        //    set
        //    {
        //        //_ChargeStatus = 375;
        //        _ChargeStatus = Mathf.Min(_ChargeStatus + value, ChargeCapacityMax);
        //    }
        //}
    }
}