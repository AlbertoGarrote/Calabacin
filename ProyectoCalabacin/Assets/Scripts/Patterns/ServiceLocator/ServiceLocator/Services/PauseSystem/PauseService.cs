using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Patterns.ServiceLocator.Interfaces;
using UnityEngine;

namespace Patterns.ServiceLocator.Services
{
    public enum GamePauseReasons
    {
        None = 0,
        Dialog = 1,
        UI = 2,
    }
    public class PauseService : IService
    {
        public void InitializeService()
        {

        }

        private Stack<int> pauseStack = new Stack<int>();
            public int GetCurrentPauseLevel()
            {
                int firstStackedPauseLevel = pauseStack.Count == 0 ? 0 : pauseStack.Peek();
                return firstStackedPauseLevel;
            }
        private Action<int, object[]> pauseEvent;

        private bool isPaused = false;
        public bool IsPaused() => isPaused;

        ///<summary>
        /// Suscribe una función correspondiente a pauseEvent
        ///</summary>
        public void SubscribeToPauseEvent(Action<int, object[]> onPauseAction){
            if(pauseEvent == null || !IsAlreadySubscribedToPauseEvent(onPauseAction))
               pauseEvent += onPauseAction;
        }

        ///<summary>
        /// Desuscribe una función correspondiente a pauseEvent
        ///</summary>
        public void UnSubscribeToPauseEvent(Action<int, object[]> onPauseAction)
        {
            if (pauseEvent != null && IsAlreadySubscribedToPauseEvent(onPauseAction))
                pauseEvent -= onPauseAction;
        }

        ///<summary>
        /// Comprueba si una función ya se encuentra suscrita a pauseEvent
        ///</summary>
        private bool IsAlreadySubscribedToPauseEvent(Action<int, object[]> callback)
        {
            if (pauseEvent == null) return false;

            foreach (var existingDelegate in pauseEvent.GetInvocationList())
            {
                if (existingDelegate == (Delegate)callback)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Añade una pausa con un cierto nivel de prioridad
        /// 
        /// </summary>
        public void Pause(int pauseLevel)
        {
            if (pauseLevel < 0)
            {
                Debug.LogError($"Tried to Pause with a negative pause level of {pauseLevel}");
                return;
            }

            int topPauseLevel = GetCurrentPauseLevel();
            if (topPauseLevel < pauseLevel)
            {
                pauseStack.Push(pauseLevel);
                InvokePauseEvent(pauseLevel);
            }
            else
            {
                Debug.LogWarning($"Tried to Pause with a pause level of {pauseLevel} when the current top stacked pause level is {topPauseLevel}");
                Debug.LogWarning($"Lower or Equal Pause calls with lower or equal levels when will be ignored");
            }
        }
        public void Pause(int pauseLevel, params object[] parameters)
        {
            if (pauseLevel < 0)
            {
                Debug.LogError($"Tried to Pause with a negative pause level of {pauseLevel}");
                return;
            }

            int topPauseLevel = GetCurrentPauseLevel();
            if (topPauseLevel < pauseLevel)
            {
                pauseStack.Push(pauseLevel);
                InvokePauseEvent(pauseLevel, parameters);
            }else{
                Debug.LogWarning($"Tried to Pause with a pause level of {pauseLevel} when the current top stacked pause level is {topPauseLevel}");
                Debug.LogWarning($"Lower or Equal Pause calls with lower or equal levels when will be ignored");
            }
        }


        /// <summary>
        /// Remueve una pausa de cierto nivel de prioridad
        /// </summary>
        public void UnPause(int pauseLevel)
        {
            if (pauseLevel < 0)
            {
                Debug.LogError($"Tried to UnPause with a negative pause level of {pauseLevel}");
                return;
            }

            int topPauseLevel = GetCurrentPauseLevel();
            if (topPauseLevel == pauseLevel)
            {
                pauseStack.Pop();
                int firstStackedPauseLevel = pauseStack.Count == 0 ? 0 : pauseStack.Peek();
                InvokePauseEvent(firstStackedPauseLevel);
            }
            else if (topPauseLevel < pauseLevel)
            {
                Debug.LogWarning($"Tried to Unpause with a pause level of {pauseLevel}");
                Debug.LogWarning($"This value is higher than the top stacked pause level: {topPauseLevel}");
            }
            else
            {
                Debug.LogWarning($"Tried to Unpause with a pause level of {pauseLevel}");
                Debug.LogWarning($"This value is lower than the top stacked pause level: {topPauseLevel}");
            }
        }
        public void UnPause(int pauseLevel, params object[] parameters)
        {
            if (pauseLevel < 0)
            {
                Debug.LogError($"Tried to UnPause with a negative pause level of {pauseLevel}");
                return;
            }

            int topPauseLevel = GetCurrentPauseLevel();
            if (topPauseLevel == pauseLevel){
                pauseStack.Pop();
                InvokePauseEvent(GetCurrentPauseLevel(), parameters);
            }
            else if(topPauseLevel < pauseLevel){
                Debug.LogWarning($"Tried to Unpause with a pause level of {pauseLevel}");
                Debug.LogWarning($"This value is higher than the top stacked pause level: {topPauseLevel}");
            }
            else{
                Debug.LogWarning($"Tried to Unpause with a pause level of {pauseLevel}");
                Debug.LogWarning($"This value is lower than the top stacked pause level: {topPauseLevel}");
            }
        }

        /// <summary>
        /// Resetea la pila de pausas
        /// </summary>
        public void ResetPauseStack(){
            pauseStack.Clear();
            InvokePauseEvent(0);
        }
        public void ResetPauseStack(params object[] parameters)
        {
            pauseStack.Clear();
            InvokePauseEvent(0, parameters);
        }

        /// <summary>
        /// Remueve de la pila de pausas aquellos niveles que sean mayor que "pauseLevel"
        /// y llama al evento de pausa con el valor que quede como superior
        /// </summary>
        public void DropPauseStackTo(int pauseLevel){
            if (pauseLevel < 0)
            {
                Debug.LogError($"Tried to Drop the PauseStack with a negative pause level of {pauseLevel}");
                return;
            }

            while (pauseStack.TryPeek(out int topPauseLevel) && topPauseLevel > pauseLevel){
                pauseStack.Pop();
            }
            InvokePauseEvent(GetCurrentPauseLevel());
        }
        public void DropPauseStackTo(int pauseLevel, object[] parameters)
        {
            if (pauseLevel < 0)
            {
                Debug.LogError($"Tried to Drop the PauseStack with a negative pause level of {pauseLevel}");
                return;
            }

            while (pauseStack.TryPeek(out int topPauseLevel) && topPauseLevel > pauseLevel)
            {
                pauseStack.Pop();
            }
            InvokePauseEvent(GetCurrentPauseLevel(), parameters);
        }

        /// <summary>
        /// Invoca pauseEvent con parámetros extra o sin ellos (null)
        /// </summary>
        private void InvokePauseEvent(int newPauseLevel)
        {
            pauseEvent?.Invoke(newPauseLevel, null);
        }
        private void InvokePauseEvent(int newPauseLevel, object[] parameters)
        {
            pauseEvent?.Invoke(newPauseLevel, parameters);
        }

        
    }
}
