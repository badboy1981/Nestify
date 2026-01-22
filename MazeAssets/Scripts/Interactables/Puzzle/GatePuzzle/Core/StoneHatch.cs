using System.Collections.Generic;
using UnityEngine;

namespace GateSystem3
{
    [RequireComponent(typeof(Animator))]
    internal class StoneHatch : Interactive
    {
        [Header("Managment")]
        [SerializeField] GateManagement gateManagement;
        [SerializeField] Animator hatchAnimator;
        [Header("----------------------")]

        [Header("Key List")]
        [SerializeField] List<GameObject> keyList;

        private void Start()
        {
            foreach (var key in keyList)
            {
                key.SetActive(false);
            }
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
             gateManagement = GetComponentInParent<GatePuzzleManager>().gateManagement;
            if (gateManagement.stoneHatchActive) return;
            hatchAnimator = GetComponent<Animator>();
            Reaction();
        }
        private void Reaction()
        {
            ActiveKey();
            if (gateManagement.allKeyCollected)
            {
                gateManagement.stoneHatchActive = true;
                gateManagement.gateEvent.OnStoneHatchActive = true;
                PlaySound("HatchSound");
                hatchAnimator.SetBool("ActiveKey", true);
            }
            else
            {
                PlaySound("NeedKey");
            }
        }
        private void ActiveKey()
        {
            for (int i = 0; i < gateManagement.collectedKeyList.Count; i++)
            {
                keyList[i].SetActive(true);
            }
        }
    }
}