using Assets.MazeAssets.Audio.Shared.BaseClasses;
using Assets.MazeAssets.Scripts.Parent;
using UnityEngine;

namespace Collectable
{
    public class Collectable : Parent
    {
        private bool isCollected;

        protected override void Awake()
        {
            isCollected = false;
        }

        internal virtual void Collect()
        {
            Destroy(gameObject);
            Debug.Log("collected!");
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isCollected)
            {
                //Debug.Log("Collectable: Player collided and not collected");
                isCollected = true;
                PlaySound("Collected");
                AudioData audioData = PrefabAudioLibrary?.GetAudioData("Collected");
                float destroyDelay = audioData != null && audioData.clip != null ? audioData.clip.length : 0f;
                //Debug.Log($"Collectable: Destroying {gameObject.name} after {destroyDelay} seconds");
                Destroy(gameObject, destroyDelay);
            }
        }
        protected virtual void SpeedChange()
        {

        }
    }
}