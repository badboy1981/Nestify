using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectable
{
    public class DataTestScript : Collectable
    {
        [SerializeField] DataTest.ScriptableObjectDataTEST dataTEST;

        public override void Collect()
        {
            base.Collect();
        }
        public void Test()
        {
            if (dataTEST != null)
            {
                int cnt = dataTEST.CoinCounter++;
                Debug.Log(cnt);
            }
        }
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            Test();
        }
    }
}