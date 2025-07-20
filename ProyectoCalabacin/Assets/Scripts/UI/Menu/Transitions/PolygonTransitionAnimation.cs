using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonTransitionAnimation : AUITransitionAnimation
{
    public override void DoCoverAnimation(float time = 1f, Action onCompletion = null)
    {
        if(!Mathf.Approximately(cachedAnimationTime, time))
            cachedAnimationTime = time;

        LeanTween.value(gameObject, new Vector2(1.2f, 0f), new Vector2(0f, -5f), time)
            .setOnUpdate((Vector2 val) =>
            {
                transitionMaterial.SetFloat("_ShapeSize", val.x);
                transitionMaterial.SetFloat("_ShapeRotation", val.y);
            })
            .setOnComplete(() => onCompletion?.Invoke());
    }

    public override void DoRevealAnimation(string newSceneName, bool useCachedTime = false, float time = 1f)
    {
        if (useCachedTime) time = cachedAnimationTime;

        LeanTween.value(gameObject, new Vector2(0, -5f), new Vector2(1.2f, 0), time)
            .setOnUpdate((Vector2 val) => {
                transitionMaterial.SetFloat("_ShapeSize", val.x);
                transitionMaterial.SetFloat("_ShapeRotation", val.y);
            });
    }

    public override void ResetMaterial()
    {
        transitionMaterial.SetFloat("_ShapeSize", 1.2f);
        transitionMaterial.SetFloat("_ShapeRotation", 0f);
    }
}
