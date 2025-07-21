using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using Game.Music;
using Patterns.ServiceLocator;
using UnityEngine;

public class PonerMusica : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var audioOptions = AudioOptionsBuilder.BuildCommon2DAudio(true, "");
            
        var soundManager = ServiceLocator.Instance.GetService<MusicManager>();
        soundManager.SetMusicAt(0, "GameMusic", audioOptions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
