using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Patterns.ObjectPool
{
    public class GenericObjectPool<T> : IGenericObjectPool<T>
    {
        private readonly Stack<T> pool = new Stack<T>();
        private readonly Func<T> factoryMethod;
        private readonly bool allowAddNewObjects;

        #region Getters
        public int PoolCount() => pool.Count;
        public Stack<T> GetPool() => pool;
        #endregion

        #region Constructor
        public GenericObjectPool(Func<T> factoryMethod, bool allowAutoCreateObjects)
        {
            this.factoryMethod = factoryMethod ?? throw new ArgumentNullException(nameof(factoryMethod));
            this.allowAddNewObjects = allowAutoCreateObjects;
        }
        public GenericObjectPool(Func<T> factoryMethod, bool allowAutoCreateObjects, int initialPoolsize)
        {
            this.factoryMethod = factoryMethod;
            this.allowAddNewObjects = allowAutoCreateObjects;
            for (int i=0; i<initialPoolsize; i++) {
                pool.Push(factoryMethod());
            }
        }
        #endregion

        #region Methods
        public T GetObjectFromPool()
        {
            if (pool.Count > 0)
                return pool.Pop();

            if (allowAddNewObjects)
                return factoryMethod();
            else
                return default;
        }
        public virtual void ReturnObjectToPool(T obj)
        {
            pool.Push(obj);
        }
        public virtual void ResetPool()
        {
            pool.Clear();
        }
        public virtual void ResetPool(uint newSize)
        {
            while (pool.Count > newSize)
            {
                pool.Pop();
            }
        }
        #endregion
    }
}