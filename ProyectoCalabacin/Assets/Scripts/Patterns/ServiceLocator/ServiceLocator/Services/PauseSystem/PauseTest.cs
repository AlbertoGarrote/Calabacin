using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator;
using Patterns.ServiceLocator.Services;
using UnityEngine;

public class PauseTest : MonoBehaviour, IPausableObject
{
    #region IPausableObject
        public bool isPaused { get; set; }
        public int pauseLevel { get; set; }

        public void OnPause(int currentPauseLevel, object[] parameters)
        {
            pauseLevel = currentPauseLevel;
            isPaused = pauseLevel > 0;
            Debug.Log($"Pause Event called with level {currentPauseLevel}");    
            if(isPaused)
            {
                Debug.Log($"Received {(int)parameters[0]}");
            }
        }
    #endregion

    public void OnEnable(){
        ServiceLocator.Instance.GetService<PauseService>().SubscribeToPauseEvent(OnPause);
    }

    int debugPause;
    public void OnDisable()
    {
        ServiceLocator.Instance.GetService<PauseService>().UnSubscribeToPauseEvent(OnPause);
    }
}
