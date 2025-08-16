using System.Collections.Generic;
using Assets.MazeAssets.Audio.Shared.BaseClasses;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio Maze/Audio Library")]
public class AudioLibrary : ScriptableObject
{
    public List<AudioConfig> SoundCategoryLists;

    public AudioData GetAudioData(string soundName)
    {
        if (SoundCategoryLists == null)
        {
            return null;
        }
        var config = SoundCategoryLists.Find(config => config.SoundName == soundName);
        return config?.SoundTrack;
    }
}