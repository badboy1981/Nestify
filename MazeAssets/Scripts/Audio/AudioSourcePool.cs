using UnityEngine;
using System.Collections.Generic;

public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] private int poolSize = 8;
    [SerializeField] private List<AudioSource> audioSources;

    private void Awake()
    {
        Debug.Log("AudioSourcePool: Awake called");
        audioSources = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioSources.Add(source);
        }
        Debug.Log($"Audio Source List Lenght: {audioSources.Count} && Audio Source List: {string.Join(',', audioSources)}");
    }

    public AudioSource GetFreeAudioSource()
    {
        AudioSource freeSource = audioSources.Find(source => !source.isPlaying);
        if (freeSource == null)
        {
            Debug.LogWarning("AudioSourcePool: No free AudioSource available");
        }
        return freeSource;
    }

    public void ReturnAudioSource(AudioSource source)
    {
        source.transform.SetParent(transform);
        source.transform.localPosition = Vector3.zero;
    }
}