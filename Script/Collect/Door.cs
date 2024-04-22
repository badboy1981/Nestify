using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectable
{
    public class Door : Collectable
    {
        [Range(0, 10f)]
        [SerializeField] float DoorOpen;
        [SerializeField] Animator _Animator;
        [SerializeField] GameObject _Collector;
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
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Door Name: {name}");
            if (_CollectorScript.CollectableName.Contains(name.Replace("Door", "Key")))
            {
                _Animator.SetBool("Inside", true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            _Animator.SetBool("Inside", false);
        }
    }
}