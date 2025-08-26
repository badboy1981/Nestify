using System.Collections.Generic;
using Assets.MazeAssets.Audio.Shared.BaseClasses;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] AudioLibrary audioLibrary;
    [SerializeField] AudioSource audioSource;
    private AudioData AudioData()
    {
        return new()
        {

        };
    }
    private void Temp1()
    {
        var Adi = new AudioData
        {

        };
    }
    private void Temp2()
    {
        var audioConfig = new AudioConfig
        {
            SoundName = "TestCategory",
            SoundTrack = new AudioData
            {

            }
        };
        audioLibrary.SoundCategoryLists.Add(audioConfig);
    }
}