using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Game.Patterns.State.StateMachine;

namespace Game.Patterns.State
{
    #region Clase StateNode
    //Clase interna auxiliar que define los estados dentro de la máquina de estados
    public class StateNode
    {
        public IState State { get; }
        public HashSet<ITransition> Transitions { get; }

        public StateNode(IState state)
        {
            this.State = state;
            Transitions = new HashSet<ITransition>();
        }

        public void AddTransition(IState to, IPredicate condition)
        {
            Transitions.Add(new BasicTransition(to, condition));
        }
    }
    #endregion
    public class StateMachine
    {
        StateNode current;
        StateNode defaultState;
        Dictionary<Type, StateNode> nodes = new();
        HashSet<ITransition> anyTransitions = new();

        public StateMachine(IState defaultState = null)
        {
            if (defaultState != null)
            {
                AddState(defaultState, true);
            }
        }

        #region Getters y Setters
        public IState GetCurrentState() { return current.State; }
        public void SetState(Type state, bool doEnter = true)
        {
            if (!nodes.ContainsKey(state)) return;

            current = nodes[state];
            if (doEnter)
                current.State?.OnEnter();
        }
        public StateNode GetNodeOfType(Type type)
        {
            var node = nodes.GetValueOrDefault(type);
            return node;
        }
        #endregion

        #region Métodos de la clase
        //Añade un nuevo estado si no existe a la máquina de estados
        public void AddState(IState state, bool isDefaultState = false)
        {
            StateNode node = AddNodeOfState(state);

            if (isDefaultState) defaultState = node;
        }

        //añade una nueva transición entre 2 estados
        public void AddTransitionFromTo(IState from, IState to, IPredicate condition)
        {
            AddNodeOfState(from).AddTransition(AddNodeOfState(to).State, condition);
        }

        //Añade una transición desde cualquier estado a un estado en concreto
        public void AddAnyTransitionTo(IState to, IPredicate condition)
        {
            anyTransitions.Add(new BasicTransition(AddNodeOfState(to).State, condition));
        }

        //Si el estado no existía, se añade el estado a los estados de la máquina, y si ya existía no es necesario. Devuelve el estado creado o especificado
        StateNode AddNodeOfState(IState state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());
            if (node == null)
            {
                node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }

            return node;
        }

        //Comprueba si alguno de los predicados de las posibles transiciones desde el nodo actual se cumplen
        ITransition EvaluateTransitions()
        {
            foreach (var transition in anyTransitions)
            {
                if (transition.To != current.State && transition.Condition.Evaluate()) return transition;
            }
            if (current == null) return null;
            foreach (var transition in current.Transitions)
            {
                if (transition.To != current.State && transition.Condition.Evaluate()) return transition;
            }
            return null;
        }

        //Cambia de un estado a otro usando a través de una transición en concreto
        public void ChangeState(Type state, int transitionSubTag)
        {
            if (!nodes.ContainsKey(state)) return;

            if (current != null)
                current.State?.OnExit(transitionSubTag);

            current = nodes[state];
            current.State?.OnEnter();

        }

        //Cambia de un estado a otro usando sin importar la transición
        public void ChangeState(Type state)
        {
            if (!nodes.ContainsKey(state)) return;

            if (current != null)
                current.State?.OnExit(-1);

            current = nodes[state];
            current.State?.OnEnter();
        }

        public void Reset()
        {
            SetState(defaultState.State.GetType(), true);
        }

        #endregion

        public void Update()
        {
            var transition = EvaluateTransitions();
            if (transition != null)
            {
                ChangeState(transition.To.GetType(), transition.transitionSubTag);
            }
            current.State?.Update();
        }
        public void FixedUpdate()
        {
            current.State?.FixedUpdate();
        }
    }

    public class SharedStateMachine
    {
        Dictionary<Type, StateNode> nodes = new();
        HashSet<ITransition> anyTransitions = new();

        #region Getters y Setters
        public StateNode GetNodeOfType(Type type)
        {
            var node = nodes.GetValueOrDefault(type);
            return node;
        }
        #endregion

        #region Métodos de la clase
        //Añade un nuevo estado si no existe a la máquina de estados
        public void AddState(IState state)
        {
            AddNodeOfState(state);
        }

        //añade una nueva transición entre 2 estados
        public void AddTransitionFromTo(IState from, IState to, IPredicate condition)
        {
            AddNodeOfState(from).AddTransition(AddNodeOfState(to).State, condition);
        }

        //Añade una transición desde cualquier estado a un estado en concreto
        public void AddAnyTransitionTo(IState to, IPredicate condition)
        {
            anyTransitions.Add(new BasicTransition(AddNodeOfState(to).State, condition));
        }

        //Si el estado no existía, se añade el estado a los estados de la máquina, y si ya existía no es necesario. Devuelve el estado creado o especificado
        StateNode AddNodeOfState(IState state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());
            if (node == null)
            {
                node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }
            return node;
        }

        //Comprueba si alguno de los predicados de las posibles transiciones desde el nodo actual se cumplen
        ITransition EvaluateTransitions(StateNode currentState)
        {
            foreach (var transition in anyTransitions)
            {
                if (transition.To != currentState.State && transition.Condition.Evaluate()) return transition;
            }
            if (currentState == null) return null;
            foreach (var transition in currentState.Transitions)
            {
                if (transition.To != currentState.State && transition.Condition.Evaluate()) return transition;
            }
            return null;
        }

        //Cambia de un estado a otro usando a través de una transición en concreto
        public void ChangeState(ref StateNode currentState, Type nextState, int transitionSubTag)
        {
            if (!nodes.ContainsKey(nextState)) return;

            if (currentState != null)
                currentState.State?.OnExit(transitionSubTag);

            currentState = nodes[nextState];
            currentState.State?.OnEnter();

        }

        //Cambia de un estado a otro usando sin importar la transición
        public void ChangeState(ref StateNode currentState, Type nextState)
        {
            if (!nodes.ContainsKey(nextState)) return;

            if (currentState != null)
                currentState.State?.OnExit(-1);

            currentState = nodes[nextState];
            currentState.State?.OnEnter();
        }
        #endregion

        public void Update(ref StateNode currentState)
        {
            var transition = EvaluateTransitions(currentState);
            if (transition != null)
            {
                ChangeState(ref currentState, transition.To.GetType(), transition.transitionSubTag);
            }
            currentState.State?.Update();
        }
        public void FixedUpdate(ref StateNode currentState)
        {
            currentState.State?.FixedUpdate();
        }
    }

}