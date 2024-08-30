using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataTest
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Game Data")]

    public class ScriptableObjectDataTEST : ScriptableObject
    {
        public int CoinCounter = 0;
    }
}