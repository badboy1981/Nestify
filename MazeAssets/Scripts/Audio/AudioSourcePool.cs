using UnityEngine;
using System.Collections.Generic;

public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] private int poolSize = 5;
    private List<AudioSource> audioSources;

    private void Awake()
    {
        Debug.Log("AudioSourcePool: Awake called");
        audioSources = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioSources.Add(source);
        }
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