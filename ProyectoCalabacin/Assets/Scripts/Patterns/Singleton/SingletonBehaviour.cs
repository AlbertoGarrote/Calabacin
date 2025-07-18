using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.Singleton
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; protected set; }
        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject.transform.root.gameObject);
                return;
            }
            else
            {
                Instance = this as T;
                DontDestroyOnLoad(this.gameObject.transform.root.gameObject);
            }
        }
    }
}
