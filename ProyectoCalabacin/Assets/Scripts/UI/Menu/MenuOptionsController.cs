using System;
using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsController : MonoBehaviour
{
    private GameSettings gameSettings;

    private void Awake()
    {
        gameSettings = ServiceLocator.Instance.GetService<GameSettings>();
    }
    public void SetSFXVolume(Slider slider)
    {
        gameSettings.SetSfxVolume(slider.value);
    }
    public void SetMusicVolume(Slider slider)
    {
        gameSettings.SetMusicVolume(slider.value);
    }
}
