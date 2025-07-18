using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DeprecatedTests
{
    public class NewspaperTest1<TResult> : INewspaper<object[], TResult>
    {

        public event Func<object[], TResult> newspaperEvent;

        public void AddCallback(Func<object[], TResult> callback)
        {
            if (newspaperEvent == null || !IsAlreadySubscribed(callback))
            {
                newspaperEvent += callback;
            }
        }

        public void NotifyReaders(object[] objects)
        {
            newspaperEvent?.Invoke(objects);
        }

        public void PublishNewspaper()
        {
            throw new NotImplementedException();
        }

        public void RemoveCallback(Func<object[], TResult> callback)
        {
            if (newspaperEvent != null && !IsAlreadySubscribed(callback))
            {
                newspaperEvent += callback;
            }
        }

        private bool IsAlreadySubscribed(Func<object[], TResult> callback)
        {
            if (newspaperEvent == null) return false;

            foreach (var existingDelegate in newspaperEvent.GetInvocationList())
            {
                if (existingDelegate is Func<object[], TResult> existingFunc && existingFunc == callback)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
