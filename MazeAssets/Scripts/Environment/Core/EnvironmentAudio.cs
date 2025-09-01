using Assets.MazeAssets.Scripts.Parent;

internal class EnvironmentAudio : Parent
{
    private void Start()
    {
        PlaySoundByList(PrefabAudioLibrary.SoundCategoryLists);
    }
}