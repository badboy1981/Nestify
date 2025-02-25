using UnityEngine;
using UnityEngine.SceneManagement;
public class MyScenesManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SceneLoader(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
    }
}