using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Patterns.ServiceLocator;
using Patterns.ServiceLocator.Services;
using UnityEngine;

public struct TransformProperties
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Quaternion LocalRotation;
    public Vector3 Scale;
    public Vector3 LocalScale;

    public TransformProperties(Transform transform)
    {
        Position = transform.position;
        Rotation = transform.rotation;
        LocalRotation = transform.localRotation;
        Scale = transform.lossyScale;
        LocalScale = transform.localScale;
    }
}
public class SimpleAnimations : MonoBehaviour, IPausableObject
{
    TransformProperties originalProperties;
    private void Awake()
    {
        originalProperties = new TransformProperties(transform);
    }

    #region IPausableObject
    [SerializeField] bool canBePaused = true;

    [ShowIf("canBePaused")]
    [SerializeField] GamePauseReasons minimunPauseReasonToReact;

    void OnDisable()
    {
        ServiceLocator.Instance.GetService<PauseService>().UnSubscribeToPauseEvent(OnPause);
    }
    [field: ReadOnly][field: SerializeField] public bool isPaused { get; set; }
    [field: ReadOnly][field: SerializeField] public int pauseLevel { get; set; }

    public void OnPause(int currentPauseLevel, params object[] parameters)
    {
        isPaused = currentPauseLevel >= (int)minimunPauseReasonToReact && canBePaused;

        if (isPaused) LeanTween.pause(gameObject);
        else LeanTween.resume(gameObject);
    }
    #endregion

    #region Animations
    public void PlayAnimation(string clipName)
    {
        GetComponent<Animator>().Play(clipName,0,0);
    }
    public void MoveOffset(Vector3 offset, float time, LeanTweenType easeType){
        LeanTween.move(gameObject, transform.position + offset, time).setEase(easeType);
    }
    public void MoveTo(Vector3 position, float time, LeanTweenType easeType)
    {
        LeanTween.move(gameObject, position, time).setEase(easeType);
    }

    public void Scale(float allAxis, float time, LeanTweenType easeType)
    {
        Vector3 newScale = new Vector3(allAxis, allAxis, allAxis);
        LeanTween.scale(gameObject, newScale, time).setEase(easeType);
    }
    public void Scale(Vector3 newScale, float time, LeanTweenType easeType)
    {
        LeanTween.scale(gameObject, newScale, time).setEase(easeType);
    }
    public void ResetScale(float time, LeanTweenType easeType)
    {
        LeanTween.scale(gameObject, originalProperties.LocalScale, time).setEase(easeType);
    }

    LTDescr pulseLTD;
    public void Pulse(Vector3 newLocalScale, float pulseTime, float recoveryTime, LeanTweenType easeType)
    {
        if (pulseLTD is not null) LeanTween.cancel(pulseLTD.id);

        Vector3 ogScale = originalProperties.LocalScale;

        pulseLTD = LeanTween.scale(gameObject, newLocalScale, pulseTime).setEase(easeType)
        .setOnComplete
        (() =>
            {
                pulseLTD = LeanTween.scale(gameObject, ogScale, recoveryTime).setEase(LeanTweenType.linear)
                .setOnComplete (() => pulseLTD = null);
            }
        );

    }
    #endregion

}
