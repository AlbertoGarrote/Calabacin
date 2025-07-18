using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Patterns.State
{
    public interface IPredicate
    {
        public bool Evaluate();
        public int id { get; set; }
    }
}
