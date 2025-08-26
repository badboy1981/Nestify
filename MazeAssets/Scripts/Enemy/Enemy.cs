using UnityEngine;
using Assets.MazeAssets.Scripts.Parent;

internal class Enemy : Parent
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"Enemy: Enter trigger with {other.gameObject.name}");
            PlaySoundByList(PrefabAudioLibrary.SoundCategoryLists);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"Enemy: Exited trigger with {other.gameObject.name}");
            StopSoundByList(PrefabAudioLibrary.SoundCategoryLists);
        }
    }
}