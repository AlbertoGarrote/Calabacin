using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.ServiceLocator.Interfaces
{
    public interface IService
    {
        
    }

    public interface IInitializableService : IService
    {
        void InitializeService();
    }
}
