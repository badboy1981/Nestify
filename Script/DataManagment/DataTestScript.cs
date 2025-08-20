using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectable
{
    internal class DataTestScript : Collectable
    {
        [SerializeField] DataTest.ScriptableObjectDataTEST dataTEST;

        internal override void Collect()
        {
            base.Collect();
        }
        protected void Test()
        {
            if (dataTEST != null)
            {
                int cnt = dataTEST.CoinCounter++;
                Debug.Log(cnt);
            }
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            Test();
        }
    }
}