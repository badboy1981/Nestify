using System.Collections.Generic;
using Assets.MazeAssets.Audio.Shared.BaseClasses;
using UnityEngine;

namespace Assets.MazeAssets.Scripts.Parent
{
    internal class Parent : MonoBehaviour
    {
        [SerializeField] protected AudioLibrary PrefabAudioLibrary;
        [SerializeField] protected SoundData soundDataEvent;
        //[SerializeField] protected ChargeManagment chargeManagment;

        protected virtual void Awake()
        {
            //Debug.Log($"Parent: Awake called on {gameObject.name}");
            if (soundDataEvent == null)
            {
                //Debug.LogError($"Parent: soundDataEvent is null on {gameObject.name}");
            }
            if (PrefabAudioLibrary == null)
            {
                //Debug.LogError($"Parent: PrefabAudioLibrary is null on {gameObject.name}");
            }
        }
        protected void PlaySoundByList(List<AudioConfig> SoundList)
        {
            foreach (var sound in SoundList)
            {
                //Debug.Log($"Parent: PlaySoundByList called for {sound.SoundName} on {gameObject.name}");
                soundDataEvent?.Play(sound.SoundName, PrefabAudioLibrary);
            }
        }
        protected void StopSoundByList(List<AudioConfig> SoundList)
        {
            foreach (var sound in SoundList)
            {
                //Debug.Log($"Parent: StopSoundByList called for {sound.SoundName} on {gameObject.name}");
                soundDataEvent?.StopSound(sound.SoundName);
            }
        }
        protected void PlaySound(string soundName)
        {
            //Debug.Log($"Parent: PlaySound called for {soundName} on {gameObject.name}");
            soundDataEvent?.Play(soundName, PrefabAudioLibrary);
        }

        protected void StopSound(string soundName)
        {
            //Debug.Log($"Parent: StopSound called for {soundName} on {gameObject.name}");
            soundDataEvent?.StopSound(soundName);
        }
    }
}