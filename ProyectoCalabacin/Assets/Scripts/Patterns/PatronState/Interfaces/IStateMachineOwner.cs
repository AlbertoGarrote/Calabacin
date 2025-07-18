using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Patterns.State
{
    public interface IStateMachineOwner
    {
        public StateMachine handCanvasStateMachine { get; set; }
        public void InitializeStateMachine();
        public void ResetStateMachine();
    }
}