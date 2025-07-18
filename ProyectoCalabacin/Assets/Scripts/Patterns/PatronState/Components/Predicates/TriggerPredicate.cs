using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Patterns.State
{
    public class TriggerPredicate : IPredicate
    {
        readonly Func<bool> triggerFunc;
        readonly Action onTriggerFunc;
        public int id { get; set; }
        public TriggerPredicate(Func<bool> func, Action onTriggerFunc, int id)
        {
            this.triggerFunc = func;
            this.id = id;
            this.onTriggerFunc = onTriggerFunc;
        }
        public bool Evaluate()
        {
            bool b = triggerFunc.Invoke();
            if (b)
            {
                onTriggerFunc?.Invoke();
            }
            return b;
        }
    }
}
