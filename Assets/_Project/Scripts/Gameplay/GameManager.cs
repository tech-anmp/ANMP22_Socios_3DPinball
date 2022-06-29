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
        m_InputManager.OnTouchScreenLeftStart += OnLeftFlip;
        m_InputManager.OnTouchScreenLeftEnd += OnLeftFlipBack;

        m_InputManager.OnTouchScreenRightStart += OnRightFlip;
        m_InputManager.OnTouchScreenRightEnd += OnRightFlipBack;

        m_ResetTrigger.OnReset += OnResetBall;

        m_DefaultBallPosition = m_Ball.transform.position;
    }
 
    private void OnDestroy()
    {
        m_InputManager.OnTouchScreenLeftStart -= OnLeftFlip;
        m_InputManager.OnTouchScreenLeftEnd -= OnLeftFlipBack;

        m_InputManager.OnTouchScreenRightStart -= OnRightFlip;
        m_InputManager.OnTouchScreenRightEnd -= OnRightFlipBack;

        m_ResetTrigger.OnReset -= OnResetBall;
    }

    private void OnLeftFlip()
    {
        m_LeftFlipper.Flip();
    }
    private void OnRightFlip()
    {
        m_RightFlipper.Flip();
    }
    private void OnLeftFlipBack()
    {
        m_LeftFlipper.FlipBack();
    }
    private void OnRightFlipBack()
    {
        m_RightFlipper.FlipBack();
    }

    private void OnResetBall()
    {
        //m_Ball.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        m_Ball.GetComponent<Rigidbody>().isKinematic = true;
        m_Ball.transform.position = m_DefaultBallPosition;
        m_Ball.GetComponent<Rigidbody>().isKinematic = false;
        //m_Ball.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
}