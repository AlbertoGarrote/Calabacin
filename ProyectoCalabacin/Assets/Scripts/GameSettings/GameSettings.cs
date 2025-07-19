using System;
using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using NaughtyAttributes;
using Patterns.ServiceLocator.Services;
using Patterns.Singleton;
using UltEvents;
using UnityEngine;
using UnityEngine.Audio;

public enum ColorblindMode
{
    None = 0,
    Deuteranopia = 1,
    Protanopia = 2,
    Tritanopia = 3,
}

public class GameSettings : SingletonService<GameSettings>
{
    #region Audio Settings
    const string audioSettings = "Audio Settings";
    [BoxGroup(audioSettings)][SerializeField] AudioMixer masterMixer;
    [BoxGroup(audioSettings)][SerializeField][Range(0, 1)] float sfxVolume = 0;
    [BoxGroup(audioSettings)][SerializeField][Range(0, 1)] float musicVolume = 0;

    const string sfxVolumeName = "SoundVolume";
    const string musicVolumeName = "MusicVolume";

    public float GetSfxVolume() => sfxVolume;
    public float GetMusicVolume() => musicVolume;
    public void SetSfxVolume(float normalizedVolume)
    {
        sfxVolume = normalizedVolume;
        if (normalizedVolume < 0.0001f) normalizedVolume = 0.0001f;

        masterMixer.SetFloat(sfxVolumeName, Mathf.Log10(normalizedVolume) * 20);
    }
    public void SetMusicVolume(float normalizedVolume)
    {
        musicVolume = normalizedVolume;
        if (normalizedVolume < 0.0001f) normalizedVolume = 0.0001f;

        masterMixer.SetFloat(musicVolumeName, Mathf.Log10(normalizedVolume) * 20);
    }

    #endregion
}