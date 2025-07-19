using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UltEvents;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInteractiveElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    [ReadOnly][SerializeField] bool isMouseOver = false;
    [ReadOnly][SerializeField] bool isHoldingClick = false;

    const string buttonEvents = "Button Events";
    [BoxGroup(buttonEvents)][SerializeField] UltEvent onPointerEnter;
    [BoxGroup(buttonEvents)][SerializeField] UltEvent onPointerExit;
    [BoxGroup(buttonEvents)][SerializeField] UltEvent onPointerDown;
    [BoxGroup(buttonEvents)][SerializeField] UltEvent onActionConfirm;
    [BoxGroup(buttonEvents)][SerializeField] UltEvent onActionCancel;
    [BoxGroup(buttonEvents)][SerializeField] UltEvent onPointerUp;

    private void OnDisable()
    {
        isMouseOver = false;
        isHoldingClick = false;
    }

    #region PointerEvents

    #region IPointerEnterHandler
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        onPointerEnter?.Invoke();
        Debug.Log("Mouse est� sobre el bot�n");
    }
    #endregion

    #region IPointerExitHandler
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        onPointerExit?.Invoke();
        Debug.Log("Mouse sali� del bot�n");
    }
    #endregion

    #region IPointerDownHandler
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        isHoldingClick = true;
        onPointerDown?.Invoke();
        //Debug.Log("Mouse hizo clic en el bot�n (sin soltar)");
    }
    #endregion

    #region IPointerUpHandler
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (isMouseOver && isHoldingClick)
        {
            onActionConfirm?.Invoke();
            //Debug.Log("Click confirmado: acci�n del bot�n ejecutada");
        }
        else
        {
            onActionCancel?.Invoke();
            //Debug.Log("Click cancelado: el mouse no estaba sobre el bot�n al soltar");
        }

        onPointerUp?.Invoke();
        isHoldingClick = false;
    }
    #endregion

    #endregion
}
