using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Patterns.State
{
    public interface IState
    {
        public void OnEnter();
        public void Update();
        public void FixedUpdate();
        public void OnExit(int transitionSubTag);
    }
}