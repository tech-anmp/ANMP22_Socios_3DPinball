using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action OnButtonDown;
    public Action OnButtonUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnButtonDown != null)
            OnButtonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnButtonUp != null)
            OnButtonUp();
    }
}