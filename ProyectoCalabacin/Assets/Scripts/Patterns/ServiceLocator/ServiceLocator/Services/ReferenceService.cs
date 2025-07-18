using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns.ServiceLocator.Interfaces;

namespace Patterns.ServiceLocator.Services
{
    public class ReferenceService : IService
    {
        public void InitializeService()
        {

        }

        private readonly Dictionary<string, object> references = new Dictionary<string, object>();

        public bool Register<T>(string key, T reference)
        {
            if (references.ContainsKey(key))
            {
                //throw new InvalidOperationException($"Ya existe una referencia con la clave '{key}'");
                Debug.LogWarning($"Ya existe una referencia con la clave '{key}'");
                return false;
            }
            references[key] = reference;
            return true;
        }

        public void RegisterOrReplace<T>(string key, T reference)
        {
            references[key] = reference;
        }

        public T GetReference<T>(string key)
        {
            if (references.TryGetValue(key, out var reference) && reference is T typedReference)
            {
                return typedReference;
            }
            return default;
        }

        public T GetWrappedReference<T>(string key) where T : struct
        {
            if (references.TryGetValue(key, out var reference))
            {
                if (reference is Wrap<T> wrappedReference)
                {
                    return wrappedReference.Value;
                }
            }
            return default;
        }

        public void Unregister(string key)
        {
            if (references.ContainsKey(key))
            {
                references.Remove(key);
            }
            else
            {
                throw new KeyNotFoundException($"No se puede eliminar la clave '{key}' porque no existe.");
            }
        }
    }

    public class Wrap<T> where T : struct
    {
        public T Value { get; set; }

        public Wrap(T value)
        {
            Value = value;
        }

        public static implicit operator T(Wrap<T> reference)
        {
            return reference.Value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}