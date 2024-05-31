using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

namespace Collectable
{
    public class Collector : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI CointText;
        [SerializeField] TextMeshProUGUI KeyText;
        public int CoinCounter = 0;
        public int KeyCounter = 0;
        public List<string> CollectableName;
        
        private void Awake()
        {
            CollectableName = new List<string>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Collectable>(out var Collectable))
            {
                Collectable.Collect();
                if (other.GetComponent<Collider>().name.Contains("Coin"))
                {
                    //_Animator.SetBool("SpeedUp", true);
                    CoinCounter++;
                    CointText.text = "Coin: " + CoinCounter;
                }
                if (other.GetComponent<Collider>().name.Contains("Key"))
                {
                    KeyCounter++;
                    CollectableName.Add(other.name);
                    KeyText.text = "Key: " + KeyCounter;
                }
            }
        }
    }
}