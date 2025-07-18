using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    public static class AudioProducerMixer
    {
        public static IEnumerator FadeVolume(AAudioProducer soundProducer, float startVolume, float endVolume, float fadeTime, Action onCompletion){
            if (soundProducer is null)
            {
                Debug.LogError("AudioProducer es null");
                yield break;
            }

            AudioSource audioSource = soundProducer.attachedAudioSource;
            if (soundProducer is null){
                Debug.LogError($"{soundProducer} no tiene asignado un Audio Source"); yield break;
            }
            Audio assingedSound = soundProducer.assignedSound;
            if (assingedSound is null)
            {
                Debug.LogError($"{soundProducer} no tiene asignado un Sound"); yield break;
            }

            startVolume = Mathf.Clamp(startVolume, 0, assingedSound.volume);
            endVolume = Mathf.Clamp(endVolume, 0, assingedSound.volume);

            float elapsed = 0f;
            Debug.LogWarning("No hay global settings");
            while (elapsed < fadeTime)
            {
                float t = elapsed / fadeTime;
                float currentRatio = Mathf.Lerp(startVolume, endVolume, t);
                float currentTargetVolume = currentRatio * 1f;

                audioSource.volume = currentTargetVolume;

                elapsed += Time.deltaTime;
                yield return new WaitWhile(()=>soundProducer.paused);
            }

            audioSource.volume = endVolume * 1f;
            soundProducer.ClearFadeCoroutine();
            onCompletion?.Invoke();

            
        }

    }
}