using Lean.Touch;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action OnTouchScreenLeft;
    public Action OnTouchScreenRight;

    private float m_ScreenWidth;
    private float m_ScreenHeight;

    private void Start()
    {
        m_ScreenWidth = Screen.width;
        m_ScreenHeight = Screen.height;
    }

    public void OnFingerDown(LeanFinger LeanFingerData)
    {
        //Debug.Log(LeanFingerData.ScreenPosition); 
    }
    public void OnFingerDownOnScreen(Vector2 FingerPosition)
    {
        if(FingerPosition.x < m_ScreenWidth/2.0f)
        {
            if (OnTouchScreenLeft != null)
                OnTouchScreenLeft();
        }
        else if(FingerPosition.x > m_ScreenWidth/2.0f)
        {
            if (OnTouchScreenRight != null)
                OnTouchScreenRight();
        }
    }
}
