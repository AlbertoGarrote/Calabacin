using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    public static class AudioOptionsBuilder{
        #region 2D
        public static AudioOptions BuildUnique2DAudio(bool loopingAudio, string audioProducerTag){
            return new AudioOptions(false, loopingAudio, audioProducerTag, true, false);
        }
        public static AudioOptions BuildCommon2DAudio(bool loopingAudio, string audioProducerTag)
        {
            return new AudioOptions(false, loopingAudio, audioProducerTag, false, false);
        }
        public static AudioOptions BuildUnique2DImmediateAudio(bool loopingAudio, string audioProducerTag)
        {
            return new AudioOptions(false, loopingAudio, audioProducerTag, true, true);
        }
        public static AudioOptions BuildCommon2DImmediateAudio(bool loopingAudio, string audioProducerTag)
        {
            return new AudioOptions(false, loopingAudio, audioProducerTag, false, true);
        }
        #endregion

        #region 3D
        public static AudioOptions BuildUnique3DAudio(bool loopingAudio, string audioProducerTag)
        {
            return new AudioOptions(true, loopingAudio, audioProducerTag, true, false);
        }
        public static AudioOptions BuildCommon3DAudio(bool loopingAudio, string audioProducerTag)
        {
            return new AudioOptions(true, loopingAudio, audioProducerTag, false, false);
        }
        public static AudioOptions BuildUnique3DImmediateAudio(bool loopingAudio, string audioProducerTag)
        {
            return new AudioOptions(true, loopingAudio, audioProducerTag, true, true);
        }
        public static AudioOptions BuildCommon3DImmediateAudio(bool loopingAudio, string audioProducerTag)
        {
            return new AudioOptions(true, loopingAudio, audioProducerTag, false, true);
        }

        #endregion
    }
    public class AudioOptions
    {
        public bool is3D { get; private set; }
        public bool loopSound { get; private set; }
        public bool playOnlyOne { get; private set; }
        public string audioProducerTag { get; private set; }
        public bool autoReproduce { get; private set; }
        public AudioOptions(bool is3D, bool loopSound, string audioProducerTag, bool playOnlyOne, bool autoReproduce)
        {
            this.is3D = is3D;
            this.loopSound = loopSound;
            this.playOnlyOne = playOnlyOne;
            this.audioProducerTag = audioProducerTag;
            this.autoReproduce = autoReproduce;
        }
    }

    
}