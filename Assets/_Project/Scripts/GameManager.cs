using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputManager m_InputManager;
    [SerializeField]
    private Flipper m_LeftFlipper;
    [SerializeField]
    private Flipper m_RightFlipper;
    [SerializeField]
    private GameObject m_Ball;
    [SerializeField]
    private ResetTrigger m_ResetTrigger;

    private Vector3 m_DefaultBallPosition;

    private void Start()
    {
        m_InputManager.OnTouchScreenLeft += OnLeftFlip;
        m_InputManager.OnTouchScreenRight += OnRightFlip;

        m_ResetTrigger.OnReset += OnResetBall;

        m_DefaultBallPosition = m_Ball.transform.position;
    }
 
    private void OnDestroy()
    {
        m_InputManager.OnTouchScreenLeft -= OnLeftFlip;
        m_InputManager.OnTouchScreenRight -= OnRightFlip;

        m_ResetTrigger.OnReset -= OnResetBall;
    }

    private void OnLeftFlip()
    {
        m_LeftFlipper.Spring();
    }
    private void OnRightFlip()
    {
        m_RightFlipper.Spring();
    }
    private void OnResetBall()
    {
        m_Ball.transform.position = m_DefaultBallPosition;
    }
}