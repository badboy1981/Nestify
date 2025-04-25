using TMPro;
using UnityEngine;

namespace Collectable
{
    public class Door : Collectable
    {
        [Range(0, 10f)]
        [SerializeField] float DoorOpen;
        [SerializeField] Animator _Animator;
        [SerializeField] GameObject _Collector;
        [SerializeField] TextMeshProUGUI WinMessage;

        Collector _CollectorScript;


        private void Awake()
        {
            DoorOpen = 0;
            _Animator = GetComponent<Animator>();
            _CollectorScript = _Collector.GetComponent<Collector>();
        }
        public override void Collect()
        {
            //Lock And Unlock Door State            
        }
        private void Update()
        {
            transform.position = new Vector3(transform.position.x, DoorOpen, transform.position.z);
        }
      public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            //Debug.Log($"Door Name: {name}");
            if (_CollectorScript._PlayerData.KeyLists.Contains(name.Replace("Door", "Key")))
            {
                _Animator.SetBool("Inside", true);
            }
            if (name == "DoorFinal")
            {
                if (_CollectorScript._PlayerData.CoinLists.Count == 20)
                {
                    _Animator.SetBool("Inside", true);
                    WinMessage.text = "YOU WIN!!!";
                    //Debug.Log("Final Door!");
                }
                else
                {
                    WinMessage.text = "You need more Coin!!!";
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            _Animator.SetBool("Inside", false);
            if (name == "DoorFinal")
            {
                WinMessage.text = null;
            }
        }
    }
}