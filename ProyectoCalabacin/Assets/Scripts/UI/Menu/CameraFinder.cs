using System;
using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFinder : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        AssignCamera();
    }
    private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        AssignCamera();
    }
    private void AssignCamera()
    {
        Camera cam = Camera.main;
        if (cam != null)
            canvas.worldCamera = cam;
        else
            Debug.LogWarning("No se encontr� c�mara con la etiqueta MainCamera en la nueva escena");
    }
}
