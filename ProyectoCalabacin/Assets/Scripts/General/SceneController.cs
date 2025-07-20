using System;
using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonService<SceneController>
{
    public Action<string> OnOpenTransition;

    public bool CanChangeSceneTo(string sceneName)
    {
        var currentScene = SceneManager.GetActiveScene();
        return currentScene.name != sceneName;
    }
    public void ChangeSceneTo(string sceneName, Action<string> onSceneLoad)
    {
        StartCoroutine(IEChangeScene(sceneName, onSceneLoad));
    }
    public void ChangeSceneTo(int sceneIndex, Action<string> onSceneLoad)
    {
        var scene = SceneManager.GetSceneAt(sceneIndex);
        ChangeSceneTo(scene.name, onSceneLoad);
    }

    IEnumerator IEChangeScene(string sceneName, Action<string> onSceneLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return new WaitWhile(() => !asyncLoad.isDone);
        onSceneLoad?.Invoke(sceneName);
    }
}
