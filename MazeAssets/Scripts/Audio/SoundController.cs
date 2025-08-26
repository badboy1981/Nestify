using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private SoundData soundData;
    private AudioPlayer audioPlayer;
    private VolumeFader volumeFader;

    private void Awake()
    {
        Debug.Log("SoundController: Awake called");
        audioPlayer = GetComponent<AudioPlayer>();
        volumeFader = GetComponent<VolumeFader>();
        if (soundData == null)
        {
            Debug.LogError("SoundController: soundData is null. Please assign SoundData asset in Inspector.");
            enabled = false; // غیرفعال کردن کامپوننت برای جلوگیری از ارورهای بعدی
            return;
        }
        if (audioPlayer == null)
        {
            Debug.LogError("SoundController: AudioPlayer component is missing.");
            enabled = false;
            return;
        }
        if (volumeFader == null)
        {
            Debug.LogError("SoundController: VolumeFader component is missing.");
            enabled = false;
            return;
        }
    }

    private void OnEnable()
    {
        Debug.Log("SoundController: OnEnable called");
        if (soundData != null)
        {
            soundData.onPlaySoundRequested += (soundName, library) => audioPlayer.PlaySound(soundName, library, FindSourceObject(soundName));
            soundData.onStopSoundRequested += audioPlayer.StopSound;
            soundData.onPauseSoundRequested += audioPlayer.PauseSound;
            soundData.onSetVolumeRequested += volumeFader.SetVolume;
        }
    }

    private void OnDisable()
    {
        if (soundData != null)
        {
            soundData.onPlaySoundRequested -= (soundName, library) => audioPlayer.PlaySound(soundName, library, FindSourceObject(soundName));
            soundData.onStopSoundRequested -= audioPlayer.StopSound;
            soundData.onPauseSoundRequested -= audioPlayer.PauseSound;
            soundData.onSetVolumeRequested -= volumeFader.SetVolume;
        }
    }

    private GameObject FindSourceObject(string soundName)
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.GetComponentsInChildren<AudioSource>().Length > 0)
            {
                return enemy.gameObject;
            }
        }
        return null;
    }
}