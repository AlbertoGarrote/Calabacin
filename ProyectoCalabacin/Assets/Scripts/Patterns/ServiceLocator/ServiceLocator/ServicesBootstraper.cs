
using UnityEngine;
using Patterns.ServiceLocator.Services;
using Patterns.ServiceLocator.Interfaces;
using Patterns.ServiceLocator;

namespace Patterns.ServiceLocator.Services
{
    public class ServicesBootstraper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BootstrapServices()
        {
            var serviceLocator = ServiceLocator.Instance;
            serviceLocator.RegisterService<ActionSubscriber>(new ActionSubscriber());
            serviceLocator.RegisterService<SceneService>(new SceneService());
            serviceLocator.RegisterService<TransformChildrenService>(new TransformChildrenService());
            serviceLocator.RegisterService<ReferenceService>(new ReferenceService());
            serviceLocator.RegisterService<NewspaperService>(new NewspaperService());
            serviceLocator.RegisterService<PauseService>(new PauseService());
        }
    }
}