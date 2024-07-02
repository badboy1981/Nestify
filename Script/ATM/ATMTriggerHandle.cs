using Collectable;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Collectable
{
    public class ATMTriggerHandle : Collectable
    {
        [SerializeField] TextMeshProUGUI AtmID;
        [SerializeField] GameObject AtmPanel;
        [SerializeField] GameObject Player;
        private HandleInput handleInput;

        private void Awake()
        {
            handleInput = Player.GetComponent<HandleInput>();
            AtmPanel.SetActive(false);
        }
        public override void Collect() { }

        private void OnTriggerEnter(Collider other)
        {
            handleInput.EnableMove = false;
            AtmPanel.SetActive(true);
            AtmID.text = $"ATM ID: {name}";
            //TryGetComponent<Collector>( out var _Collector);
            var _Collector = other.GetComponent<Collector>();
            _Collector._PlayerData.AtmID = name;
        }
        private void OnTriggerExit(Collider other)
        {
            handleInput.EnableMove = true;
            AtmPanel.SetActive(false);
        }
        private void _OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Collector>(out var _Collector))
            {

                var PD = new PlayerData()
                {
                    PlayerPosition = _Collector.transform.position,
                    PlayerRotation = _Collector.transform.rotation
                };
            }
        }
    }
}