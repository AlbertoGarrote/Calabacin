using System;
using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using NaughtyAttributes;
using Patterns.ServiceLocator;
using UnityEngine;

namespace Game.Music
{
    public class MusicProducer : AAudioProducer
    {
        protected static MusicManager musicManager;
        protected override void OnDisable()
        {
            musicManager.PauseMusicProducer -= PauseSound;
            musicManager.StopMusicProducer -= StopSound;
        }
        protected override void OnEnable()
        {
            MusicManager mM = ServiceLocator.Instance.GetService<MusicManager>();
            if (musicManager is null || musicManager != mM) musicManager = mM;
            if (attachedAudioSource is null) attachedAudioSource = GetComponent<AudioSource>();

            musicManager.PauseMusicProducer += PauseSound;
            musicManager.StopMusicProducer += StopSound;

            if (autoReproduce) PlaySound();
        }

        #region Initialization
        public override void PrepareAudioProducer(Audio.Audio sound, AudioOptions audioOptions)
        {

            assignedSound = sound;
            if (audioOptions.is3D)
            {
                attachedAudioSource.spatialBlend = 1;
                attachedAudioSource.maxDistance = sound.maxListeningDistance;
            }
            else
            {
                attachedAudioSource.spatialBlend = 0;
                attachedAudioSource.maxDistance = 500;
            }

            onlyOne = audioOptions.playOnlyOne;
            autoReproduce = audioOptions.autoReproduce;

            InitializeAudioSource(audioOptions.loopSound);
        }
        protected override void InitializeAudioSource(bool loop)
        {
            attachedAudioSource.volume = assignedSound.volume;
            attachedAudioSource.pitch = assignedSound.GetRandomizedPitch();
            attachedAudioSource.clip = assignedSound.audioClip;
            attachedAudioSource.loop = loop;
        }
        #endregion

        #region Simple Methods
        public override void PlaySound()
        {
            try
            {
                if (attachedAudioSource.clip is null) return;

                if (attachedAudioSource is null)
                    throw new Exception($"AudioSource for {this.gameObject} not initialized");

                attachedAudioSource.Play();
                hasPlayed = true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        #endregion

        #region Pause and Stop
        public void PauseSound(bool pause)
        {
            paused = pause;
            if (paused) attachedAudioSource?.Pause();
            else attachedAudioSource?.UnPause();
        }
        public void StopSound()
        {
            attachedAudioSource?.Stop();
            ClearFadeCoroutine();
            hasPlayed = false;
            this.gameObject.SetActive(false);
        }
        #endregion

        #region Fade
        public override void ClearFadeCoroutine() => fadeCoroutine = null;
        public override void FadeSound(float fromVolume, float toVolume, float fadeTime, Action onCompletion)
        {
            if (fadeCoroutine is not null)
            {
                StopCoroutine(fadeCoroutine);
                if (attachedAudioSource is not null) fromVolume = attachedAudioSource.volume / 1f;
            }
            fadeCoroutine = StartCoroutine(AudioProducerMixer.FadeVolume(this, fromVolume, toVolume, fadeTime, onCompletion));
        }

        #endregion
        private void Update()
        {
            if (attachedAudioSource is null) return;
            if (!attachedAudioSource.isPlaying && !attachedAudioSource.loop && !paused && hasPlayed)
            {
                StopSound();
            }
        }
    }
}