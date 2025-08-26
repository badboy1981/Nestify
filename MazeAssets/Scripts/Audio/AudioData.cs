using UnityEngine;

namespace Assets.MazeAssets.Audio.Shared.BaseClasses
{
    public enum AudioType { Ambient, SFX, Music, Alert }
    [System.Serializable]
    public class AudioData
    {
        //public string SoundName;
        public AudioClip clip;
        public bool loop;
        public bool is3D;
        public float spatialBlend;
        public bool useMaxDistance;
        public float soundMaxDistance;
        //public AudioClip clip;
        //public string clipName; // برای شناسایی (مثلاً "Volt_Movement")
        //public bool loop;
        //[SerializeField] private AudioType type;
        //public AudioType Type => type;
        //public UnityEngine.Audio.AudioMixerGroup mixerGroup; // گروه میکسر (Music یا SFX)
        //public bool is3D; // برای صدای سه‌بعدی
        //public float spatialBlend = 0f; // 0 برای 2D، 1 برای 3D
    }
}