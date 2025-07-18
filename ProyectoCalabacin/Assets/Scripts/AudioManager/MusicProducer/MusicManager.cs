using System;
using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using NaughtyAttributes;
using Patterns.ServiceLocator.Services;
using UnityEngine;

namespace Game.Music
{
    public class MusicManager : SingletonService<MusicManager>
    {
        [SerializeField] protected AudioGallery selectedSoundGallery;

        [BoxGroup("Music Tracks Settings")] public GameObject MusicProducerPrefab;
        [BoxGroup("Music Tracks Settings")] [SerializeField] int numberOfMusicTracks;
        
        [BoxGroup("Music Tracks")] [SerializeField] MusicProducer[] musicTracks;

        public Action<bool> PauseMusicProducer;
        public Action StopMusicProducer;

        protected override void Awake()
        {
            base.Awake();

            musicTracks = new MusicProducer[numberOfMusicTracks];
            for(int i=0; i<numberOfMusicTracks; i++) {
                GameObject go = Instantiate(MusicProducerPrefab, transform);
                go.transform.localPosition = Vector3.zero;
                go.name = MusicProducerPrefab.name;
                go.tag = "MusicProducer";
                go.SetActive(false);
                musicTracks[i] = go.GetComponent<MusicProducer>();
            }
        }

        #region Getters
        public MusicProducer GetMusicProducerAt(int trackIndex){
            if (trackIndex < 0 || trackIndex >= musicTracks.Length) {
                throw new Exception($"Index {trackIndex} is out of bound for MusicTrack Array");
            }
            return musicTracks[trackIndex];
        }
        #endregion

        #region Set/PlayMusic
        private Audio.Audio FindSound(string soundName)
        {
            return selectedSoundGallery.FindAudio(soundName);
        }
        public MusicProducer SetMusicAt(int trackIndex, string soundName, AudioOptions audioOptions){
            try
            {
                Audio.Audio sound = FindSound(soundName);
                MusicProducer musicProducer = GetMusicProducerAt(trackIndex);

                musicProducer.PrepareAudioProducer(sound, audioOptions);
                musicProducer.gameObject.SetActive(true);
                return musicProducer;
            }catch(Exception e){
                Debug.LogError(e.Message);
                return null;
            }
        }
        public void ChangeMusicAt(int trackIndex, string newSoundName, AudioOptions newAudioOptions, float fadeTime){
            try
            {
                Audio.Audio sound = FindSound(newSoundName);
                MusicProducer musicProducer = GetMusicProducerAt(trackIndex);
                StartCoroutine(IEChangeMusic(musicProducer, fadeTime, sound, newAudioOptions));
            }catch(Exception e){
                Debug.LogError(e.Message);
            }
        }
        protected IEnumerator IEChangeMusic(MusicProducer musicProducer, float fadeTime, Audio.Audio sound, AudioOptions newAudioOptions)
        {
            float ogVolume = musicProducer.assignedSound.volume;

            musicProducer.FadeSound(ogVolume, 0, fadeTime * 0.5f, null);
            yield return new WaitWhile(() => musicProducer.fadeCoroutine is not null);

            musicProducer.PrepareAudioProducer(sound, newAudioOptions);

            musicProducer.FadeSound(0, ogVolume, fadeTime * 0.5f, null);
            yield return new WaitWhile(() => musicProducer.fadeCoroutine is not null);
        }
        #endregion

        #region Pause and Stop
        public void PauseMusicAt(int trackIndex, bool pause){
            try{
                GetMusicProducerAt(trackIndex).PauseSound(pause);
            }catch(Exception e){
                Debug.LogError(e.Message);
            }
        }
        public void PauseAllMusic(bool pause){
            PauseMusicProducer?.Invoke(pause);
        }
        public void StopAllMusic()
        {
            StopMusicProducer?.Invoke();
        }

        public void StopMusicAt(int trackIndex)
        {
            try
            {
                GetMusicProducerAt(trackIndex).StopSound();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        #endregion

        #region FadeOut
        public void FadeOutMusicAt(int trackIndex, float fadeTime){
            try
            {
                MusicProducer musicProducer = GetMusicProducerAt(trackIndex);
                float ogVolume = musicProducer.assignedSound.volume;
                musicProducer.FadeSound(ogVolume, 0, fadeTime, musicProducer.StopSound);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        #endregion
    }
}