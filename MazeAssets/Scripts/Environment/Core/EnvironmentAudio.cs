using UnityEngine;
using Assets.MazeAssets.Scripts.Parent;

public class EnvironmentAudio : Parent
{
    [SerializeField] private string[] soundNames = { "Wind", "Birds" };

    private void Start()
    {
        foreach (string soundName in soundNames)
        {
            PlaySound(soundName);
        }
    }

    public void StopAllSounds()
    {
        foreach (string soundName in soundNames)
        {
            StopSound(soundName);
        }
    }
}