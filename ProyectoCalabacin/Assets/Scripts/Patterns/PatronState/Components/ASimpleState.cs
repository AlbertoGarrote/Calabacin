using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Patterns.State
{
    public abstract class ASimpleState : IState
    {

        protected ASimpleState() { }
        public abstract void OnEnter();
        public virtual void Update(){}
        public virtual void FixedUpdate(){}
        public abstract void OnExit(int transitionSubTag); //Dependiendo de la transición pueden hacerse diferente funcionalidades
    }

    public abstract class AUpdatingState : IState
    {
        protected AUpdatingState() { }
        public abstract void OnEnter();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void OnExit(int transitionSubTag); //Dependiendo de la transición pueden hacerse diferente funcionalidades
    }
}