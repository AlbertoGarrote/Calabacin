using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Patterns.ServiceLocator.Interfaces;

namespace Patterns.ServiceLocator.Services
{
    public class SceneService : IService
    {
        public void InitializeService()
        {

        }

        #region Change and Reset Scene
        public IEnumerator ChangeSceneTo(string scene)
        {

            if (SceneManager.GetActiveScene().name == scene) { yield break; }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        public IEnumerator ChangeSceneTo(int scene)
        {
            if (SceneManager.GetActiveScene().buildIndex == scene) { yield break; }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        public IEnumerator ResetScene()
        {
            int s = SceneManager.GetActiveScene().buildIndex;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(s);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        #endregion

        #region Conversions
        public int GetSceneIndex(string sceneName)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (scene.IsValid())
            {
                return scene.buildIndex;
            }
            else
            {
                Debug.LogError($"La escena con el nombre {sceneName} no está en la lista de compilación.");
                return -1;
            }
        }
        public string GetSceneName(int sceneIndex)
        {
            if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
                return System.IO.Path.GetFileNameWithoutExtension(scenePath);
            }
            else
            {
                Debug.LogError($"El índice {sceneIndex} está fuera de los límites de las escenas en la lista de compilación.");
                return null;
            }
        }
        #endregion

        #region Checks
        public bool IsActiveScene(string scene) => SceneManager.GetActiveScene().name == scene;
        public bool IsActiveScene(int scene) => SceneManager.GetActiveScene().buildIndex == scene;
        #endregion

    }
}