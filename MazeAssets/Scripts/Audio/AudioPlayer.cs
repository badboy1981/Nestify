using System;
using System.Collections.Generic;
using Assets.MazeAssets.Audio.Shared.BaseClasses;
using UnityEngine;

[RequireComponent(typeof(AudioSourcePool))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSourcePool audioSourcePool;
    [SerializeField] private List<(string soundName, AudioSource source, GameObject sourceObject)> activeSounds;
    [SerializeField] private GameObject player; // (Player)

    private void Awake()
    {
        Debug.Log("AudioPlayer: Awake called");
        audioSourcePool = GetComponent<AudioSourcePool>();
        activeSounds = new List<(string, AudioSource, GameObject)>();
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("AudioPlayer: Player with tag 'Player' not found");
        }
        if (activeSounds != null)
        {
            Debug.Log($"Active Sounds List Lenght: {activeSounds.Count} ||" +
                $" Active Sound List: {String.Join(',', activeSounds)}");
        }
    }

    public void PlaySound(string soundName, AudioLibrary library, GameObject sourceObject = null)
    {
        //Debug.Log($"AudioPlayer: PlaySound called for {soundName}");
        if (library == null || string.IsNullOrEmpty(soundName))
        {
            Debug.LogWarning("AudioPlayer: Library or soundName is null/empty");
            return;
        }

        AudioData audioData = library.GetAudioData(soundName);
        if (audioData == null || audioData.clip == null)
        {
            Debug.LogWarning($"AudioPlayer: No AudioData found for {soundName}");
            return;
        }

        AudioSource source = audioSourcePool.GetFreeAudioSource();
        if (source == null)
        {
            Debug.LogWarning("AudioPlayer: No free AudioSource available");
            return;
        }

        source.clip = audioData.clip;
        source.loop = audioData.loop;
        source.spatialBlend = audioData.is3D ? 1f : 0f;

        if (audioData.is3D && sourceObject != null)
        {
            source.transform.SetParent(sourceObject.transform);
            source.transform.localPosition = Vector3.zero;
            if (audioData.useMaxDistance)
            {
                source.maxDistance = audioData.soundMaxDistance;
                source.rolloffMode = AudioRolloffMode.Linear;
                if (player != null)
                {
                    float distance = Vector3.Distance(sourceObject.transform.position, player.transform.position);
                    Debug.Log($"AudioPlayer: Distance to player for {soundName}: {distance}, MaxDistance: {audioData.soundMaxDistance}");
                }
            }
        }
        else
        {
            source.transform.SetParent(transform);
            source.transform.localPosition = Vector3.zero;
        }

        if (audioData.loop)
        {
            activeSounds.Add((soundName, source, sourceObject));
            source.Play();
        }
        else
        {
            source.PlayOneShot(audioData.clip);
        }
    }

    public void StopSound(string soundName)
    {
        //Debug.Log($"AudioPlayer: StopSound called for {soundName}");
        var sound = activeSounds.Find(s => s.soundName == soundName);
        if (sound.source != null)
        {
            sound.source.Stop();
            audioSourcePool.ReturnAudioSource(sound.source);
            activeSounds.Remove(sound);
        }
    }

    public void PauseSound(string soundName)
    {
        Debug.Log($"AudioPlayer: PauseSound called for {soundName}");
        var sound = activeSounds.Find(s => s.soundName == soundName);
        if (sound.source != null)
        {
            sound.source.Pause();
        }
    }

    public AudioSource GetActiveAudioSource(string soundName)
    {
        var sound = activeSounds.Find(s => s.soundName == soundName);
        return sound.source;
    }
}