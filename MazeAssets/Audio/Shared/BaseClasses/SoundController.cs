using UnityEngine;
using System.Collections.Generic;
using Assets.MazeAssets.Audio.Shared.BaseClasses;

public class SoundController :MonoBehaviour
{
    [SerializeField] private int audioSourcePoolSize = 5; // تعداد AudioSource‌ها در Pool
    private List<AudioSource> audioSourcePool;

    private void Awake()
    {
        // مقداردهی Pool
        audioSourcePool = new List<AudioSource>();
        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.spatialBlend = 0f; // 2D برای همه صداها
            audioSourcePool.Add(source);
        }
    }

    public void PlaySound(AudioLibrary library, string soundName)
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

        // پیدا کردن AudioSource آزاد
        AudioSource freeSource = audioSourcePool.Find(source => !source.isPlaying);
        if (freeSource == null)
        {
            Debug.LogWarning("No free AudioSource available");
            return;
        }

        // اعمال تنظیمات
        freeSource.clip = audioData.clip;
        freeSource.loop = audioData.loop;
        freeSource.spatialBlend = audioData.spatialBlend;

        // پخش صدا
        if (audioData.loop)
        {
            freeSource.Play(); // برای صداهای Loop
        }
        else
        {
            freeSource.PlayOneShot(audioData.clip); // برای صداهای کوتاه مثل "جیرینگ"
        }
    }
}