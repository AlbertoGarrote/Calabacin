using System;
using System.Collections;
using System.Collections.Generic;
using Game.Patterns.ObjectPool;
using NaughtyAttributes;
using Patterns.ServiceLocator.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Audio
{ 
    public class SoundManager : SingletonService<SoundManager>
    {
        [SerializeField] protected AudioGallery selectedSoundGallery;

        [BoxGroup("Sound Pool")] public GameObject SoundProducerPrefab;
        [BoxGroup("Sound Pool")] [SerializeField] Transform StartingSoundContainer;

        GenericObjectPool<GameObject> soundSourcesPool;
        HashSet<string> activeAudioSourcesTags = new HashSet<string>();

        public Action<string, bool> PauseSoundProducer;
        public Action<string, bool> StopSoundProducer;
        public Action<string, float, float, float, Action> FadeSoundProducer;

        protected override void Awake()
        {
            base.Awake();
            soundSourcesPool =
            new GenericObjectPool<GameObject>(
                CreateSoundProducerObject,
                true, 5
            );
        }

        #region Pool Handling
        private GameObject CreateSoundProducerObject(){
            GameObject go = Instantiate(SoundProducerPrefab, StartingSoundContainer);
            go.name = SoundProducerPrefab.name;
            go.tag = "SoundProducer";
            go.SetActive(false);
            return go;
        }
        public void ReturnSoundProducerToPool(GameObject audioProducerGO)
        {
            try
            {
                SoundProducer audioProducer = audioProducerGO.GetComponent<SoundProducer>();
                if (audioProducer is null)
                    throw new Exception($"Audio Producer no fue encontrado en el objeto {audioProducerGO}");

                if (audioProducer.tag != "SoundProducer")
                {
                    Debug.LogWarning($"El objeto {audioProducerGO} no es un AudioProducer creado por AudioManager");
                    return;
                }

                if (audioProducer.onlyOne) activeAudioSourcesTags.Remove(audioProducer.tag);

                soundSourcesPool.ReturnObjectToPool(audioProducerGO);
                audioProducerGO.SetActive(false);

                audioProducerGO.transform.SetParent(StartingSoundContainer);
                audioProducerGO.transform.localPosition = Vector3.zero;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        #endregion

        #region PlaySounds
        private Audio FindSound(string soundName)
        {
            return selectedSoundGallery.FindAudio(soundName);
        }

        public SoundProducer PlayOverlaySound(string soundName, AudioOptions audioOptions)
        {
            if (TryGetSoundProducer(soundName, audioOptions, out GameObject audioProducerGO))
            {
                audioProducerGO.transform.parent = this.transform;
                audioProducerGO.transform.localPosition = Vector3.zero;

                audioProducerGO.SetActive(true);
                return audioProducerGO.GetComponent<SoundProducer>();
            }
            return null;
        }

        public SoundProducer PlayOverlaySound(Audio audio, AudioOptions audioOptions)
        {
            if (TryGetSoundProducer(audio, audioOptions, out GameObject audioProducerGO))
            {
                audioProducerGO.transform.parent = this.transform;
                audioProducerGO.transform.localPosition = Vector3.zero;

                audioProducerGO.SetActive(true);
                return audioProducerGO.GetComponent<SoundProducer>();
            }
            return null;
        }

        public SoundProducer PlaySoundOn(GameObject obj, string soundName, AudioOptions audioOptions){
            if (TryGetSoundProducer(soundName, audioOptions, out GameObject audioProducerGO))
            {
                audioProducerGO.transform.parent = obj.transform;
                audioProducerGO.transform.localPosition = Vector3.zero;

                audioProducerGO.SetActive(true);
                return audioProducerGO.GetComponent<SoundProducer>();
            }
            return null;
        }
        public SoundProducer PlaySoundOn(GameObject obj, Audio audio, AudioOptions audioOptions)
        {
            if (TryGetSoundProducer(audio, audioOptions, out GameObject audioProducerGO))
            {
                audioProducerGO.transform.parent = obj.transform;
                audioProducerGO.transform.localPosition = Vector3.zero;

                audioProducerGO.SetActive(true);
                return audioProducerGO.GetComponent<SoundProducer>();
            }
            return null;
        }
        public SoundProducer PlaySoundAt(Vector3 worldPosition, string soundName, AudioOptions audioOptions){
            if (TryGetSoundProducer(soundName, audioOptions, out GameObject audioProducerGO)){
                audioProducerGO.transform.parent = null;
                audioProducerGO.transform.position = worldPosition;

                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.MoveGameObjectToScene(audioProducerGO, currentScene);
                audioProducerGO.SetActive(true);
                return audioProducerGO.GetComponent<SoundProducer>();
            }
            return null;
        }
        public SoundProducer PlaySoundAt(Vector3 worldPosition, Audio audio, AudioOptions audioOptions)
        {
            if (TryGetSoundProducer(audio, audioOptions, out GameObject audioProducerGO))
            {
                audioProducerGO.transform.parent = null;
                audioProducerGO.transform.position = worldPosition;

                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.MoveGameObjectToScene(audioProducerGO, currentScene);
                audioProducerGO.SetActive(true);
                return audioProducerGO.GetComponent<SoundProducer>();
            }
            return null;
        }
        private bool TryGetSoundProducer(string soundName, AudioOptions audioOptions, out GameObject audioProducerGO)
        {
            audioProducerGO = null;

            Audio sound = FindSound(soundName);
            if (sound is null) { Debug.LogError($"{soundName} no ha sido encontrado en la galeria actual"); return false; }

            string pTag = audioOptions.audioProducerTag;
            if (pTag != string.Empty)
            {
                if (!activeAudioSourcesTags.Contains(pTag)) activeAudioSourcesTags.Add(pTag);
                else if (audioOptions.playOnlyOne) { return false; }
            }

            audioProducerGO = soundSourcesPool.GetObjectFromPool();
            SoundProducer audioProducer = audioProducerGO.GetComponent<SoundProducer>();
            audioProducer.PrepareAudioProducer(sound, audioOptions);
            return true;
        }
        private bool TryGetSoundProducer(Audio sound, AudioOptions audioOptions, out GameObject audioProducerGO)
        {
            audioProducerGO = null;
            string pTag = audioOptions.audioProducerTag;
            if (pTag != string.Empty)
            {
                if (!activeAudioSourcesTags.Contains(pTag)) activeAudioSourcesTags.Add(pTag);
                else if (audioOptions.playOnlyOne) { return false; }
            }

            audioProducerGO = soundSourcesPool.GetObjectFromPool();
            SoundProducer audioProducer = audioProducerGO.GetComponent<SoundProducer>();
            audioProducer.PrepareAudioProducer(sound, audioOptions);
            return true;
        }
        #endregion

        #region Fade
        public void ApplyFadeToSoundProducersWithTag(string soundProducerTag, float fromVolume, float toVolume, float fadeTime, Action onCompletion = null)
        {
            FadeSoundProducer?.Invoke(soundProducerTag, fromVolume, toVolume, fadeTime, onCompletion);
        }
        #endregion
        #region Pause And Stop
        public void PauseSoundProducersWithTag(string soundProducerTag, bool pause)
        {
            PauseSoundProducer?.Invoke(soundProducerTag, pause);
        }
        public void PauseAllSoundProducers(bool pause)
        {
            PauseSoundProducer?.Invoke(string.Empty, pause);
        }
        public void StopSoundProducersWithTag(string soundProducerTag, bool includeDontDestroyOnLoad = false)
        {
            StopSoundProducer?.Invoke(soundProducerTag, includeDontDestroyOnLoad);
        }
        public void StopAllSoundProducers(bool includeDontDestroyOnLoad = false)
        {
            StopSoundProducer?.Invoke(string.Empty, includeDontDestroyOnLoad);
        }

        #endregion

        private SoundProducer ap;
        private void Start()
        {
            //PlaySoundAt(new Vector3(1,0,0), "Presente_profesora", AudioOptionsBuilder.BuildUnique2DAudio(true, "tag"));
            //ap = PlaySoundOn(this.gameObject, "Presente_profesora", AudioOptionsBuilder.BuildUnique2DImmediateAudio(true, "tag"));
            //PlaySoundOn(this.gameObject, "UI_back", AudioOptionsBuilder.BuildUnique2DAudio(true, "tag1"));
            //PlaySoundOn(this.gameObject, "UI_back", aparams);
            //ap = PlaySoundOn(this.gameObject, "ClienteEnfadado2", AudioOptionsBuilder.BuildUnique2DImmediateAudio(true, "tag"));
        }


    }
}
