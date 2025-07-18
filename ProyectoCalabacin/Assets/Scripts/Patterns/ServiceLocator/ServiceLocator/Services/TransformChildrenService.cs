using System.Collections;
using System.Collections.Generic;
using Patterns.ServiceLocator.Interfaces;
using UnityEngine;

namespace Patterns.ServiceLocator.Services
{
    public class TransformChildrenService : IService
    {
        public void InitializeService()
        {

        }

        #region Control de Children
        public void SetActiveAllChildren(Transform transform, bool active)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(active);
            }
        }

        public void SetActiveAllChildrenExcept(Transform transform, bool active, int exceptionIndex)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(i != exceptionIndex)
                transform.GetChild(i).gameObject.SetActive(active);
            }
        }

        public void SetActiveChildAt(Transform transform, int at, bool active)
        {
            transform.GetChild(at).gameObject.SetActive(active);
        }

        public Transform GetChildAt(Transform transform, int at) {
            return transform.GetChild(at);
        }
        #endregion


    }
}
