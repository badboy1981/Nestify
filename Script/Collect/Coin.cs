using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    public class Coin : Collectable
    {
        public static event UnityAction CoinCollectedEvent = delegate { };
        public static event UnityAction<string> TestEvent = delegate { };

        [SerializeField] DataTest.ScriptableObjectDataTEST CoinCounter;

        public override void Collect()
        {
            base.Collect();
            //CoinCollectedEvent?.Invoke();
            //Destroy(gameObject);
        }
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            CoinCounter.CoinCounter += 1;
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    //Counter++;
        //    //CointText.text = "Coin: " + Counter;
        //    Debug.Log($"Coin Collected: {Counter}");
        //}
        //public void EventTest(string ab)
        //{
        //    TestEvent?.Invoke(ab);
        //}
    }
}