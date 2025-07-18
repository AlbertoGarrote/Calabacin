using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Audio
{
    public enum AudioType
    {
        SFX,
        Music,
    }

    [System.Serializable]
    public class Audio
    {
        public AudioType audioType;
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume = 1.0f;
        public float maxListeningDistance;

        [BoxGroup("Pitch")]
        [Range(0f, 3f)]
        public float pitch = 1.0f;

        [BoxGroup("Pitch")]
        [Tooltip("Range of variation for the normal pitch")]
        [MinMaxSlider(-3.0f, 3.0f)]
        public Vector2 randomPitchVariation = new Vector2(0, 0);
        public float GetRandomizedPitch()
        {
            return pitch + Random.Range(randomPitchVariation.x, randomPitchVariation.y);
        }
    }

    [System.Serializable]
    public class AudioChance
    {
        public Audio audio;
        public float chance;
    }
    [System.Serializable]
    public class AudioCollection
    {
        public string collectionName;
        public AudioChance[] audios;

        public Audio GetAudio(string _name)
        {
            AudioChance audio = System.Array.Find(audios, audioChance => audioChance.audio.audioClip.name == _name);
            return audio.audio;
        }
        public Audio GetRandom(bool evenChance)
        {
            if (!evenChance)
            {
                float totalChance = 0f;
                foreach (var audio in audios)
                {
                    totalChance += audio.chance;
                }

                float randomValue = Random.Range(0f, totalChance);

                foreach (var audio in audios)
                {
                    if (randomValue < audio.chance)
                    {
                        return audio.audio;
                    }
                    randomValue -= audio.chance;
                }

                return null;

            }
            else
            {
                int r = Random.Range(0, audios.Length);
                return audios[r].audio;
            }
        }

    }
}