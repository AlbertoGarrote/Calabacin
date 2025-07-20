using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AUITransitionAnimation : MonoBehaviour
{
    [SerializeField] protected Image transitionImage;
    protected Material transitionMaterial;

    protected float cachedAnimationTime;
    protected void Awake()
    {
        transitionMaterial = transitionImage.material;
    }
    protected void OnDisable()
    {
        ResetMaterial();
    }
    public abstract void DoCoverAnimation(float time = 1f, Action onCompletion = null);
    public abstract void DoRevealAnimation(string newSceneName, bool useCachedTime = false, float time = 1f);

    public abstract void ResetMaterial();
}
