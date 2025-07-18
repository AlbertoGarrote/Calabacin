using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Patterns.State
{
    public class FunctionPredicate : IPredicate
    {
        readonly Func<bool> func;
        public int id { get; set; }
        public FunctionPredicate(Func<bool> func, int id)
        {
            this.func = func;
            this.id = id;
        }
        public bool Evaluate() => func.Invoke();
    }
}