using UnityEngine;
using UnityEngine.Events;
//using Assets.MazeAssets.Audio.Shared.BaseClasses;

[CreateAssetMenu(fileName = "SoundDataEvent", menuName = "Audio Maze/Sound Data Event")]
public class SoundData : ScriptableObject
{
    public struct SoundVolumeParams
    {
        public string soundName;
        public float startVolume;
        public float endVolume;
        public float duration;
        public bool stopAfterFade;

        public SoundVolumeParams(string soundName, float startVolume, float endVolume, float duration, bool stopAfterFade)
        {
            this.soundName = soundName;
            this.startVolume = startVolume;
            this.endVolume = endVolume;
            this.duration = duration;
            this.stopAfterFade = stopAfterFade;
        }
    }
    [System.NonSerialized]
    public UnityAction<string, AudioLibrary> onPlaySoundRequested;

    [System.NonSerialized]
    public UnityAction<string> onStopSoundRequested;

    [System.NonSerialized]
    public UnityAction<string> onPauseSoundRequested;

    //[System.NonSerialized]
    //public UnityAction<string, float, float, float> onSetVolumeRequested; 

    [System.NonSerialized]
    public UnityAction<SoundVolumeParams> onSetVolumeRequested;

    public void Play(string soundName, AudioLibrary library)
    {
        //Debug.Log($"Play requested for sound: {soundName}");
        onPlaySoundRequested?.Invoke(soundName, library);
    }

    public void StopSound(string soundName)
    {
        //Debug.Log($"Stop requested for sound: {soundName}");
        onStopSoundRequested?.Invoke(soundName);
    }

    public void PauseSound(string soundName)
    {
        //Debug.Log($"Pause requested for sound: {soundName}");
        onPauseSoundRequested?.Invoke(soundName);
    }
    //public void SetVolume(string soundName, float startVolume, float endVolume, float duration)
    //{
    //    //Debug.Log($"SetVolume requested for sound: {soundName}, from {startVolume} to {endVolume} over {duration}s");
    //    onSetVolumeRequested?.Invoke(soundName, startVolume, endVolume, duration);
    //}
    public void SetVolume(string soundName, float startVolume, float endVolume, float duration, bool stopAfterFade = false)
    {
        Debug.Log($"SetVolume requested for sound: {soundName}, from {startVolume} to {endVolume} over {duration}s, stopAfterFade: {stopAfterFade}");
        //onSetVolumeRequested?.Invoke(soundName, startVolume, endVolume, duration, stopAfterFade);
        onSetVolumeRequested?.Invoke(new SoundVolumeParams(soundName, startVolume, endVolume, duration, stopAfterFade));
    }
}