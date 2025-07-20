using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator;
using Patterns.ServiceLocator.Services;
using UnityEngine;

public class UITransitionCanvas : SingletonService<UITransitionCanvas>
{
    public UITransition[] UITransitions;
}
