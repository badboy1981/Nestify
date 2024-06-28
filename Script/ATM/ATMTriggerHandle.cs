using Collectable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectable
{
    public class ATMTriggerHandle : MonoBehaviour
    {
        private void _OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Collector>(out var _Collector))
            {

                var PD = new PlayerData()
                {
                    PlayerPosition = _Collector.transform.position,
                    PlayerRotation = _Collector.transform.rotation,
                    CoinValet = _Collector.CoinList.Count,
                    KeyValet = _Collector.KeyList.Count
                };
                //string CoinList = String.Join(",", PData.CoinList);
                //string KeyList = String.Join(",", PData.KeyList);
                //Vector3 vector = other.transform.position;
                //Debug.Log($"Atm Enter: {other.name}" +
                //    $"|| CollectorName: {Collector.name} || Position: {vector} ||" +
                //    $" Coin List: {CoinList} || Key List: {KeyList}");
                //Debug.Log($"CollectableName: {Collector.name}");
                Debug.Log(JsonUtility.ToJson(PD, true));
            }
        }
        private PlayerData playerData()
        {
            return new PlayerData()
            {
                
            };
        }
    }
}