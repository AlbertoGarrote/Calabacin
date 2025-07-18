using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Patterns.ServiceLocator;
using UnityEngine;

namespace Game.Audio
{
    public class SoundProducer : AAudioProducer
    {
        [ReadOnly] public string soundProducerTag;
        protected static SoundManager soundManager;
        protected override void OnDisable()
        {
            soundManager.PauseSoundProducer -= PauseSound;
            soundManager.StopSoundProducer -= StopSound;
            soundManager.FadeSoundProducer -= FadeSound;
        }
        protected override void OnEnable()
        {
            SoundManager aM = ServiceLocator.Instance.GetService<SoundManager>();
            if (soundManager is null || soundManager != aM) soundManager = aM;
            if (attachedAudioSource is null) attachedAudioSource = GetComponent<AudioSource>();

            soundManager.PauseSoundProducer += PauseSound;
            soundManager.StopSoundProducer += StopSound;
            soundManager.FadeSoundProducer += FadeSound;

            if (autoReproduce) PlaySound();
        }


        #region Initialization
        public override void PrepareAudioProducer(Audio sound, AudioOptions audioOptions)
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
            soundProducerTag = audioOptions.audioProducerTag;
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
        public override void PlaySound(){
            try
            {
                if (attachedAudioSource.clip is null)
                {
                    return;
                }

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
        private void PauseSound(string stopProducerWithTag, bool pause){
            if(stopProducerWithTag==soundProducerTag || stopProducerWithTag == string.Empty){
                paused = pause;
                if (paused) attachedAudioSource?.Pause();
                else attachedAudioSource?.UnPause();
            }
        }
        private void StopSound(string stopProducerWithTag, bool includeDontDestroyOnLoad)
        {
            if (this.gameObject.scene.name == "DontDestroyOnLoad" && !includeDontDestroyOnLoad) return;

            if (stopProducerWithTag == soundProducerTag || stopProducerWithTag == string.Empty)
            {
                ImmediateStop();
            }
        }
        public void ImmediateStop()
        {
            attachedAudioSource?.Stop();
            ClearFadeCoroutine();
            hasPlayed = false;
            soundManager.ReturnSoundProducerToPool(this.gameObject);
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
        private void FadeSound(string fadeProducerWithTag, float fromVolume, float toVolume, float fadeTime, Action onCompletion)
        {
            if (fadeProducerWithTag != soundProducerTag) return;
            if (fadeCoroutine is not null)
            {
                StopCoroutine(fadeCoroutine);
                if(attachedAudioSource is not null) fromVolume = attachedAudioSource.volume / 1f;
            }
            fadeCoroutine = StartCoroutine(AudioProducerMixer.FadeVolume(this, fromVolume, toVolume, fadeTime, onCompletion));
        }
        
        #endregion

        private void Update()
        {
            if (attachedAudioSource is null) return;

            if(!attachedAudioSource.isPlaying && !attachedAudioSource.loop && !paused && hasPlayed)
            {
                StopSound(string.Empty, true);
            }
        }
    }
}