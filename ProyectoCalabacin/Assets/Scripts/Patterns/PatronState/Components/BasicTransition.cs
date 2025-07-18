using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Patterns.State
{
    public class BasicTransition : ITransition
    {
        public IState To { get; set; }
        public IPredicate Condition { get; set; }
        public int transitionSubTag { get; set; }

        public BasicTransition(IState to, IPredicate condition)
        {
            this.To = to;
            this.Condition = condition;
            this.transitionSubTag = condition.id;
        }
    }
}