using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class LoadScene : MonoBehaviour
    {
        public void LoadNormal(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }
        
        public void LoadAdditive(string levelName)
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        }

        public void LoadLastLevel()
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("PreviousLoadedLevel"));
        }

        public void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(name, UnloadSceneOptions.None);
        }

        public void UnloadLastScene()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
        }
    }
}