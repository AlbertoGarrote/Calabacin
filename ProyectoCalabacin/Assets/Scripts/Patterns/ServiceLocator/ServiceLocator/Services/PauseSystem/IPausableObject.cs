using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.ServiceLocator.Services
{
    public interface IPausableObject
    {
        public bool isPaused { get; set; }
        public int pauseLevel { get; set; }
        public void OnPause(int currentPauseLevel, params object[] parameters);
    }
}