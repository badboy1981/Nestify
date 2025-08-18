using UnityEngine;

namespace Assets.MazeAssets.Scripts.Parent
{
    public class Parent : MonoBehaviour
    {
        [SerializeField] protected AudioLibrary PrefabAudioLibrary;
        [SerializeField] protected SoundData soundDataEvent;

        protected virtual void Awake()
        {
            Debug.Log($"Parent: Awake called on {gameObject.name}");
            if (soundDataEvent == null)
            {
                Debug.LogError($"Parent: soundDataEvent is null on {gameObject.name}");
            }
            if (PrefabAudioLibrary == null)
            {
                Debug.LogError($"Parent: PrefabAudioLibrary is null on {gameObject.name}");
            }
        }

        protected void PlaySound(string soundName)
        {
            Debug.Log($"Parent: PlaySound called for {soundName} on {gameObject.name}");
            soundDataEvent?.Play(soundName, PrefabAudioLibrary);
        }

        protected void StopSound(string soundName)
        {
            Debug.Log($"Parent: StopSound called for {soundName} on {gameObject.name}");
            soundDataEvent?.StopSound(soundName);
        }
    }
}