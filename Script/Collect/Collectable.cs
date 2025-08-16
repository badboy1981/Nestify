using Assets.MazeAssets.Audio.Shared.BaseClasses;
using UnityEngine;

namespace Collectable
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] AudioLibrary PrefabAudioLibrary;
        [SerializeField] SoundController soundController;
        private bool isCollected;

        [System.Obsolete]
        private void Awake()
        {
            isCollected = false;
            Test();
        }
        [System.Obsolete]
        private void Test()
        {
            if (soundController == null)
            {
                soundController = FindObjectOfType<SoundController>();
            }
        }
        public virtual void Collect()
        {
            Destroy(gameObject);
            Debug.Log("collected!");
        }
        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isCollected)
            {
                isCollected = true;
                if (other.CompareTag("Player") && !isCollected)
                {
                    isCollected = true;
                    soundController?.PlaySound(PrefabAudioLibrary, "Coin_Collect");
                    AudioData audioData = PrefabAudioLibrary?.GetAudioData("Coin_Collect");
                    float destroyDelay = audioData != null && audioData.clip != null ? audioData.clip.length : 0f;
                    Destroy(gameObject, destroyDelay); // حذف با تأخیر
                }
            }
        }
        public virtual void SpeedChange()
        {

        }
    }
}