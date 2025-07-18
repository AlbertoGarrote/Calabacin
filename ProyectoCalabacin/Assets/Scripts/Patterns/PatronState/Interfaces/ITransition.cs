using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Patterns.State
{
    public interface ITransition
    {
        public IState To { get; set; }
        public IPredicate Condition { get; set; }
        public int transitionSubTag { get; set; }
    }
}