using System;
using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using NaughtyAttributes;
using UnityEngine;

public abstract class AAudioProducer : MonoBehaviour
{
    public AudioSource attachedAudioSource;
    public Audio assignedSound;

    [BoxGroup("Sound Producer Flags")][ReadOnly] public bool onlyOne;
    [BoxGroup("Sound Producer Flags")][ReadOnly] public bool paused;
    [BoxGroup("Sound Producer Flags")][ReadOnly] public bool autoReproduce;
    [ReadOnly] [SerializeField] protected bool hasPlayed = false;

    protected abstract void OnEnable();
    protected abstract void OnDisable();

    #region Initialization
    public abstract void PrepareAudioProducer(Audio sound, AudioOptions audioOptions);
    protected abstract void InitializeAudioSource(bool loop);
    #endregion

    #region Simple Methods
    public abstract void PlaySound();
    #endregion

    #region Fade
    public Coroutine fadeCoroutine;
    public abstract void ClearFadeCoroutine();
    public abstract void FadeSound(float fromVolume, float toVolume, float fadeTime, Action onCompletion);
    #endregion
}
