using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Patterns.ObjectPool
{
    public interface IGenericObjectPool<T>
    {
        public T GetObjectFromPool();
        public void ReturnObjectToPool(T obj);
        public void ResetPool();
        public void ResetPool(uint newSize);
    }
}