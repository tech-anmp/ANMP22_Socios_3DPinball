using Lean.Touch;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private bool m_UseUIInputs;
    [SerializeField]
    private TouchButton m_LeftButton;
    [SerializeField]
    private TouchButton m_RightButton;

    public Action OnTouchScreenLeftStart;
    public Action OnTouchScreenLeftEnd;

    public Action OnTouchScreenRightStart;
    public Action OnTouchScreenRightEnd;

    private float m_ScreenWidth;
    private float m_ScreenHeight;

    private void Start()
    {
        m_ScreenWidth = Screen.width;
        m_ScreenHeight = Screen.height;

        if (m_UseUIInputs)
        {
            m_LeftButton.OnButtonDown += OnLeftFlipperBtnDown;
            m_LeftButton.OnButtonUp += OnLeftFlipperBtnUp;

            m_RightButton.OnButtonDown += OnRightFlipperBtnDown;
            m_RightButton.OnButtonUp += OnRightFlipperBtnUp;
        }
    }

    public void OnFingerDown(LeanFinger LeanFingerData)
    {
        //Debug.Log(LeanFingerData.ScreenPosition); 
    }
    public void OnFingerDownOnScreen(Vector2 FingerPosition)
    {
        if (m_UseUIInputs)
            return;

        if(FingerPosition.x < m_ScreenWidth/2.0f)
        {
            OnLeftFlipperBtnDown();
        }
        else if(FingerPosition.x > m_ScreenWidth/2.0f)
        {
            OnRightFlipperBtnDown();
        }
    }
    public void OnFingerUpOnScreen(Vector2 FingerPosition)
    {
        if (m_UseUIInputs)
            return;

        if (FingerPosition.x < m_ScreenWidth / 2.0f)
        {
            OnLeftFlipperBtnUp();
        }
        else if (FingerPosition.x > m_ScreenWidth / 2.0f)
        {
            OnRightFlipperBtnUp();
        }
    }

    private void OnLeftFlipperBtnDown()
    {
        if (OnTouchScreenLeftStart != null)
            OnTouchScreenLeftStart();
    }
    private void OnRightFlipperBtnDown()
    {
        if (OnTouchScreenRightStart != null)
            OnTouchScreenRightStart();
    }
    private void OnLeftFlipperBtnUp()
    {
        if (OnTouchScreenLeftEnd != null)
            OnTouchScreenLeftEnd();
    }
    private void OnRightFlipperBtnUp()
    {
        if (OnTouchScreenRightEnd != null)
            OnTouchScreenRightEnd();
    }
}
