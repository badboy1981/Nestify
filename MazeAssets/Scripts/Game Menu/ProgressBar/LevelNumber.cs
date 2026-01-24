using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMenu
{
    public class LevelNumber : ProgressBar
    {
        private void Start()
        {
            Init(SceneManager.GetActiveScene().name);
        }
        private void Init(string sceneName)
        {
            itemCounterText.text = $"{SaveSystem.Data2.PlayableSceneList.levelNumber(sceneName)}";
            LevelData.SceneName = sceneName;
        }
    }
}