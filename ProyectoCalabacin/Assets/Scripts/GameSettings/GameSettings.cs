using System;
using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator.Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameSettings : SingletonService<GameSettings>
{
    public float sfxVolume;
    public float musicVolume;
    public int colorblindSetting;

    #region Actions
    public Action OnVolumeChanged;
    #endregion
    
    public void SetSFXVolume(Slider slider){
        sfxVolume = slider.value;
        OnVolumeChanged?.Invoke();
    }
    public void SetMusicVolume(Slider slider){
        musicVolume = slider.value;
        OnVolumeChanged?.Invoke();
    }
}