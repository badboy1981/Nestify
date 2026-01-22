using UnityEngine;

namespace GateSystem
{
    [RequireComponent(typeof(Animator))]
    internal class StoneHatch : Interactive
    {
        [SerializeField] GateSystemManager manager;
        [SerializeField] GameObject[] gateHandle;
        [SerializeField] GameObject[] keysList;
        [SerializeField] Animator hatchAnimator;
        [SerializeField] bool hatchActivate = false;

        private void Start()
        {
            hatchAnimator = GetComponent<Animator>();
            foreach (var key in keysList) key.SetActive(false);
        }
        void OnEnable()
        {
            manager.OnAllKeysCollected += OnEnableHatch;
            manager.OnCollectedKeyCount += OnCheckKeyCoun;
        }
        void OnDisable()
        {
            manager.OnAllKeysCollected -= OnEnableHatch;
            manager.OnCollectedKeyCount -= OnCheckKeyCoun;
        }
        void OnEnableHatch()
        {
            foreach (var item in gateHandle) item.SetActive(hatchActivate);
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (!hatchActivate) manager.CollectedKeyCount();
            //manager.AllKeysCollected();
        }
        private void OnCheckKeyCoun(int KeyCount)
        {
            for (int i = 0; i < KeyCount; i++)
            {
                keysList[i].SetActive(true);
            }

            hatchActivate = (KeyCount == manager.keysRequired);
            if (hatchActivate)
            {
                hatchAnimator.SetBool("ActiveKey", true);
                PlaySound("HatchSound");

                manager.AllKeysCollected();
            }
            else
            {
                PlaySound("NeedKey");
            }
        }
    }
}