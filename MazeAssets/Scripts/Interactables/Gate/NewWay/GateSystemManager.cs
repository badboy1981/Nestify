using UnityEngine;
using UnityEngine.Events;

namespace GateSystem
{
    public class GateSystemManager : MonoBehaviour
    {
        public int keysRequired = 3;
        private int keysCollected = 0;

        public UnityAction OnAllKeysCollected;
        public UnityAction OnHatchActivated;
        public UnityAction<bool> OnGateOpen;
        public UnityAction<int> OnKeyCollected;
        public UnityAction<int> OnCheckKeyCount;
        public void CollectKey()
        {
            keysCollected++;
            OnKeyCollected?.Invoke(keysCollected);
        }

        public void ActivateHatch()
        {
            OnHatchActivated?.Invoke();
        }
        public void AllKeysCollected()
        {
            //if (keysCollected >= keysRequired)
                OnAllKeysCollected?.Invoke();
        }
        public void OpenGate()
        {
            if (keysCollected >= keysRequired)
            {                
                OnGateOpen?.Invoke(true);
            }
            else
            {
                OnGateOpen?.Invoke(false);
            }
        }
        public void CheckKeyCount()
        {
            OnCheckKeyCount?.Invoke(keysCollected);
        }
    }
}