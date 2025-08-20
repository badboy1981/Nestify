using System.Collections;
using System.Collections.Generic;
using Assets.MazeAssets.Audio.Shared.BaseClasses;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private SoundData soundData; // مرجع به SoundData
    [SerializeField] private int audioSourcePoolSize = 5; // تعداد AudioSource‌ها
    private List<AudioSource> audioSourcePool;
    private List<(string soundName, AudioSource source)> activeSounds; // برای مدیریت توقف/مکث

    private void Awake()
    {
        // مقداردهی Pool
        audioSourcePool = new List<AudioSource>();
        activeSounds = new List<(string, AudioSource)>();
        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.spatialBlend = 0f; // 2D
            audioSourcePool.Add(source);
        }
    }

    private void OnEnable()
    {
        if (soundData != null)
        {
            soundData.onPlaySoundRequested += PlaySound;
            soundData.onStopSoundRequested += StopSound;
            soundData.onPauseSoundRequested += PauseSound;
            soundData.onSetVolumeRequested += SetVolume;
        }
    }

    private void OnDisable()
    {
        if (soundData != null)
        {
            soundData.onPlaySoundRequested -= PlaySound;
            soundData.onStopSoundRequested -= StopSound;
            soundData.onPauseSoundRequested -= PauseSound;
            soundData.onSetVolumeRequested -= SetVolume;
        }
    }

    private void PlaySound(string soundName, AudioLibrary library)
    {
        if (library == null || string.IsNullOrEmpty(soundName))
        {
            Debug.LogWarning("Library or soundName is null/empty");
            return;
        }

        AudioData audioData = library.GetAudioData(soundName);
        if (audioData == null || audioData.clip == null)
        {
            Debug.LogWarning($"No AudioData found for {soundName}");
            return;
        }

        AudioSource freeSource = audioSourcePool.Find(source => !source.isPlaying);
        if (freeSource == null)
        {
            Debug.LogWarning("No free AudioSource available");
            return;
        }

        freeSource.clip = audioData.clip;
        freeSource.loop = audioData.loop;
        freeSource.spatialBlend = audioData.spatialBlend;

        if (audioData.loop)
        {
            activeSounds.Add((soundName, freeSource));
            freeSource.Play();
        }
        else
        {
            freeSource.PlayOneShot(audioData.clip);
        }
    }

    private void StopSound(string soundName)
    {
        var sound = activeSounds.Find(s => s.soundName == soundName);
        if (sound.source != null)
        {
            sound.source.Stop();
            activeSounds.Remove(sound);
        }
    }

    private void PauseSound(string soundName)
    {
        var sound = activeSounds.Find(s => s.soundName == soundName);
        if (sound.source != null)
        {
            sound.source.Pause();
        }
    }
    private void SetVolume(SoundData.SoundVolumeParams volumeParams)
    {
        Debug.Log($"SoundController: SetVolume called for {volumeParams.soundName}, from {volumeParams.startVolume} to {volumeParams.endVolume} over {volumeParams.duration}s, stopAfterFade: {volumeParams.stopAfterFade}");
        var sound = activeSounds.Find(s => s.soundName == volumeParams.soundName);
        if (sound.source != null)
        {
            StartCoroutine(FadeVolume(sound.source, volumeParams.startVolume, volumeParams.endVolume, volumeParams.duration, volumeParams.stopAfterFade, volumeParams.soundName));
        }
    }
    private IEnumerator FadeVolume(AudioSource source, float startVolume, float endVolume, float duration, bool stopAfterFade, string soundName)
    {
        source.volume = startVolume;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, endVolume, elapsed / duration);
            yield return null;
        }
        source.volume = endVolume;
        if (stopAfterFade)
        {
            StopSound(soundName);
        }
    }
}