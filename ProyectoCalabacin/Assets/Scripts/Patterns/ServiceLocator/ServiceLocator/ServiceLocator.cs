using System.Collections.Generic;
using Patterns.ServiceLocator.Interfaces;
using UnityEngine;

namespace Patterns.ServiceLocator
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceLocator();
                }

                return _instance;
            }
        }

        private readonly Dictionary<string, IService> _services;

        private ServiceLocator()
        {
            _services = new Dictionary<string, IService>();
        }

        public bool RegisterService<T>(IService service) where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                if(service is IInitializableService s)
                s.InitializeService();

                _services.Add(key, service);
                return true;
            }
            return false;
            //Debug.Log($"Service registered: {key}");
        }

        public T GetService<T>() where T : IService
        {
            string key = typeof(T).Name;

            if (_services.ContainsKey(key))
            {
                return (T)_services[key];
            }

            return default(T);
        }

        public bool HasServiceRegistered<T>() where T : IService
        {
            string key = typeof(T).Name;
            return _services.ContainsKey(key);
        }
    }
}