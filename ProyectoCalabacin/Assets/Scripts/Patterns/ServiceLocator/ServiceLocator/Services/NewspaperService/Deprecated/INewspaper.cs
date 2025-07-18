using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeprecatedTests
{
    public interface INewspaper<TParam, TResult>
    {
        public event Func<TParam, TResult> newspaperEvent;

        public void PublishNewspaper();
        public void AddCallback(Func<TParam, TResult> callback);
        public void RemoveCallback(Func<TParam, TResult> callback);
        public void NotifyReaders(object[] objects);
    }
}