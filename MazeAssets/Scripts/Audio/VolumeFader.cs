using UnityEngine;
using System.Collections;

public class VolumeFader : MonoBehaviour
{
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        Debug.Log("VolumeFader: Awake called");
        audioPlayer = GetComponent<AudioPlayer>();
    }

    public void SetVolume(SoundData.SoundVolumeParams volumeParams)
    {
        Debug.Log($"VolumeFader: SetVolume called for {volumeParams.soundName}, from {volumeParams.startVolume} to {volumeParams.endVolume} over {volumeParams.duration}s, stopAfterFade: {volumeParams.stopAfterFade}");
        AudioSource source = audioPlayer.GetActiveAudioSource(volumeParams.soundName);
        if (source != null)
        {
            StartCoroutine(FadeVolume(source, volumeParams.startVolume, volumeParams.endVolume, volumeParams.duration, volumeParams.stopAfterFade, volumeParams.soundName));
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
            audioPlayer.StopSound(soundName);
        }
    }
}