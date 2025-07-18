using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Patterns.ServiceLocator.Interfaces;

namespace Patterns.ServiceLocator.Services
{
    public class ActionSubscriber : IService
    {
        public void InitializeService()
        {

        }

        public void SafeSubscribe(ref Action action, Action callback)
        {
            if (action == null || !IsAlreadySubscribed(action, callback))
            {
                action += callback;
            }
        }
        public void SafeSubscribe<T>(ref Action<T> action, Action<T> callback)
        {
            if (action == null || !IsAlreadySubscribed(action, callback))
            {
                action += callback;
            }
        }

        public void SafeUnsubscribe(ref Action action, Action callback)
        {
            if (action != null && IsAlreadySubscribed(action, callback))
            {
                action -= callback;
            }
        }

        public void SafeUnsubscribe<T>(ref Action<T> action, Action<T> callback)
        {
            if (action != null && IsAlreadySubscribed(action, callback))
            {
                action -= callback;
            }
        }
        private bool IsAlreadySubscribed(Action action, Action callback)
        {
            return action != null && (action.GetInvocationList()?.Contains(callback) ?? false);
        }

        private bool IsAlreadySubscribed<T>(Action<T> action, Action<T> callback)
        {
            if (action == null) return false;

            foreach (var existingDelegate in action.GetInvocationList())
            {
                if (existingDelegate == (Delegate)callback)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
