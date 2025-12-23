using UnityEngine;

namespace GateSystem
{
    internal class StoneHatch : Interactive
    {
        [SerializeField] private GateSystemManager manager;
        [SerializeField] private GameObject[] handle;
        [SerializeField] GameObject[] keysList;
        [SerializeField] bool canActivate = false;

        private void Start()
        {
            foreach (var key in keysList) key.SetActive(false);
        }
        void OnEnable()
        {
            manager.OnAllKeysCollected += OnEnableHatch;
            manager.OnCheckKeyCount += OnCheckKeyCoun;
        }
        void OnDisable()
        {
            manager.OnAllKeysCollected -= OnEnableHatch;
            manager.OnCheckKeyCount -= OnCheckKeyCoun;
        }
        void OnEnableHatch()
        {
            foreach (var item in handle) item.SetActive(canActivate);
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            manager.CheckKeyCount();
            manager.AllKeysCollected();
        }
        private void OnCheckKeyCoun(int KeyCount)
        {
            canActivate = (KeyCount == manager.keysRequired);

            for (int i = 0; i < KeyCount; i++)
            {
                keysList[i].SetActive(true);
            }
        }
    }
}