using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Patterns.Prototype
{
    public interface IPrototype
    {
        public IPrototype Clone();
    }
}