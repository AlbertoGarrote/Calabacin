using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    [CreateAssetMenu(fileName = "New Audio Gallery", menuName = "Game/Audio Gallery")]
    public class AudioGallery : ScriptableObject
    {
        [SerializeField] protected Audio[] audios;

        [Header("")]
        [SerializeField] protected AudioCollection[] audioCollecions;

        public Audio[] GetAudioArray() { return audios; }
        public Audio FindAudio(string audioName)
        {
            Audio audio = System.Array.Find(audios, a => a.audioClip.name == audioName);
            if (audio is null) throw new System.Exception($"Audio {audioName} not found in Gallery {this}");
            return audio;
        }

        public Audio FindAudioInCollection(string audioName, string collectionName)
        {
            AudioCollection audioC = System.Array.Find(audioCollecions, a => a.collectionName == collectionName);
            if (audioC == null) { return null; }
            return audioC.GetAudio(audioName);
        }

        public Audio FindAudioInCollectionRandom(string _collectionName, bool evenChance)
        {
            AudioCollection audioC = System.Array.Find(audioCollecions, a => a.collectionName == _collectionName);
            if (audioC == null) { return null; }
            return audioC.GetRandom(evenChance);
        }
    }
}