using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using Game.Music;
using Patterns.ServiceLocator;
using UnityEngine;

public class SimpleAudioReproducer : MonoBehaviour
{
    private static SoundManager soundManager;
    private static MusicManager musicManager;

    private void OnEnable()
    {
        if (soundManager == null) soundManager = ServiceLocator.Instance.GetService<SoundManager>();

        if (musicManager == null) musicManager = ServiceLocator.Instance.GetService<MusicManager>();
    }
    public void Play3DSound(string audioName, string audioTag)
    {
        var audioOptions = AudioOptionsBuilder.BuildCommon3DImmediateAudio(false, audioTag);
        if (soundManager == null) soundManager = ServiceLocator.Instance.GetService<SoundManager>();
        Debug.Log(soundManager);
        soundManager.PlaySoundOn(gameObject, audioName, audioOptions);
    }
    public void Play2DSound(string audioName, string audioTag)
    {
        var audioOptions = AudioOptionsBuilder.BuildCommon2DImmediateAudio(false, audioTag);
        if (soundManager == null) soundManager = ServiceLocator.Instance.GetService<SoundManager>();


        soundManager.PlaySoundOn(gameObject, audioName, audioOptions);
    }

    public void PlayOverlaySound(string audioName, string audioTag)
    {
        var audioOptions = AudioOptionsBuilder.BuildCommon2DImmediateAudio(false, audioTag);
        if (soundManager == null) soundManager = ServiceLocator.Instance.GetService<SoundManager>();


        soundManager.PlayOverlaySound(audioName, audioOptions);
    }

    public void PlayMusic(int trackIndex, string audioName, string audioTag, float fadeTime)
    {
        var audioOptions = AudioOptionsBuilder.BuildUnique3DImmediateAudio(true, audioTag);
        if (musicManager == null) musicManager = ServiceLocator.Instance.GetService<MusicManager>();
        musicManager.ChangeMusicAt(trackIndex, audioName, audioOptions, fadeTime);
    }
}
