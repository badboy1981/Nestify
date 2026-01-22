using System.Collections.Generic;
using UnityEngine;

namespace GateSystem2
{
    [RequireComponent(typeof(Animator))]
    internal class StoneHatch : Interactive
    {
        [Header("Mission Check")]
        [SerializeField] bool Compelete;
        [Header("----------------------")]

        [Header("Managment")]
        [SerializeField] GateManagment gateManagment;
        [SerializeField] Animator hatchAnimator;
        [SerializeField] Animator HandleAnimator;
        [Header("----------------------")]

        [Header("Key's List")]
        [SerializeField] List<GameObject> Keys;

        private void Start()
        {
            Compelete = false;
            hatchAnimator = GetComponent<Animator>();
            foreach (var key in Keys)
            {
                key.SetActive(false);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (Compelete) return;
            gateManagment.gateEvent.HatchKeyCheck = gateManagment.CheckAllKeyCollected();
        }

        private void OnEnable()
        {
            gateManagment.gateEvent.OnKeyNeedEvent += OnStoneHatchNeedKey;
            gateManagment.gateEvent.OnAllKeyCollectedEvent += OnStoneHatchActive;
            gateManagment.gateEvent.OnHatchActiveCompleteEvent += OnHatchActiveComplete;
        }

        private void OnDisable()
        {
            gateManagment.gateEvent.OnKeyNeedEvent -= OnStoneHatchNeedKey;
            gateManagment.gateEvent.OnAllKeyCollectedEvent -= OnStoneHatchActive;
            gateManagment.gateEvent.OnHatchActiveCompleteEvent -= OnHatchActiveComplete;
        }
        private void OnStoneHatchNeedKey()
        {
            PlaySound("NeedKey");
        }
        private void OnStoneHatchActive()
        {
            Compelete = true;
            hatchAnimator.SetBool("ActiveKey", true);
            PlaySound("HatchSound");
        }
        private void OnHatchActiveComplete()
        {
            gateManagment.gateEvent.HatchActiveComplete = true;
        }
        private void StTrigger(bool state)
        {
            for (int i = 0; i < gateManagment.collectedKeyList.Count; i++)
            {
                Keys[i].SetActive(true);
            }

            if (state)
            {
                Compelete = state;
                hatchAnimator.SetBool("ActiveKey", state);
                PlaySound("HatchSound");
            }
            else
            {
                PlaySound("NeedKey");
            }
        }
    }
}