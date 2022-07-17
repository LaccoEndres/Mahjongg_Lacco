using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class UILoader : MonoBehaviour
    {
        private readonly string sceneName = "Level_UI";
        private void Start()
        {
            PlayerPrefs.SetString("PreviousLoadedLevel", SceneManager.GetActiveScene().name);
            
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}