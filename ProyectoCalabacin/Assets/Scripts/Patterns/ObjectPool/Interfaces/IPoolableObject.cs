using System.Collections;
using System.Collections.Generic;
using Game.Patterns.Prototype;
using UnityEngine;

namespace Game.Patterns.ObjectPool
{
    public interface IPooleableObject : IPrototype
    {
        public bool Active
        {
            get;
            set;
        }
        public void Reset();
        public void Destroy();
    }
}