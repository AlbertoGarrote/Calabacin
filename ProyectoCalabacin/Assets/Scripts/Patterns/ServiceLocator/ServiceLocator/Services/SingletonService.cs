using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator.Interfaces;
using UnityEngine;

namespace Patterns.ServiceLocator.Services
{
    public class SingletonService<T> : MonoBehaviour, IService where T : Component, IService
    {
        protected virtual void Awake()
        {
            var serviceLocator = ServiceLocator.Instance;
            if (!serviceLocator.RegisterService<T>(this))
            {
                Destroy(gameObject.transform.root.gameObject);
            }else{
                DontDestroyOnLoad(gameObject.transform.root.gameObject);
            }
        }
        
    }
}