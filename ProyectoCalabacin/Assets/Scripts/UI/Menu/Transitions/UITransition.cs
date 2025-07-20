using System;
using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public AUITransitionAnimation UITransitionAnimation;
    private SceneController sceneController;

    private void Awake()
    {
        sceneController = ServiceLocator.Instance.GetService<SceneController>();
    }

    /*private void OnEnable()
    {
        if(!sceneController) return;

        sceneController.OnOpenTransition += RevealScreen;
    }

    private void OnDisable()
    {
        if (!sceneController) return;

        sceneController.OnOpenTransition -= RevealScreen;
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TryTransitionTo("Escena", 1f);
        }
    }

    public void TryTransitionTo(string sceneName, float transitionTime)
    {
        if (sceneController.CanChangeSceneTo(sceneName))
        {
            CoverScreen(sceneName, transitionTime);
        }
    }

    private void CoverScreen(string sceneName, float transitionTime)
    {
        UITransitionAnimation.DoCoverAnimation(transitionTime, ()=>sceneController.ChangeSceneTo(sceneName,RevealScreen));
    }

    private void RevealScreen(string newSceneName)
    {
        UITransitionAnimation.DoRevealAnimation(newSceneName, true);
    }
}
