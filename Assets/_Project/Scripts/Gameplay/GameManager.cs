using System;
using System.Collections.Generic;
using System.Linq;
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
    private Plunger m_Plunger;
    [SerializeField]
    private GameObject m_Ball;
    [SerializeField]
    private ResetTrigger m_ResetTrigger;

    private Vector3 m_DefaultBallPosition;
    private ToyBase[] m_Toys;
    private int m_Points;

    private static GameManager m_Instance;
    public static GameManager Instance { get => m_Instance; }

    public int Score { get => m_Points; }

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
    }

    private void Start()
    {
        m_Points = 0;

        InitializeToys();

        m_InputManager.OnTouchScreenLeftStart += OnLeftFlip;
        m_InputManager.OnTouchScreenLeftEnd += OnLeftFlipBack;

        m_InputManager.OnTouchScreenRightStart += OnRightFlip;
        m_InputManager.OnTouchScreenRightEnd += OnRightFlipBack;

        m_InputManager.OnPlungerStart += OnStartPlunger;

        m_ResetTrigger.OnReset += OnResetBall;

        m_DefaultBallPosition = m_Ball.transform.position;
    }

    private void OnDestroy()
    {
        m_InputManager.OnTouchScreenLeftStart -= OnLeftFlip;
        m_InputManager.OnTouchScreenLeftEnd -= OnLeftFlipBack;

        m_InputManager.OnTouchScreenRightStart -= OnRightFlip;
        m_InputManager.OnTouchScreenRightEnd -= OnRightFlipBack;

        m_InputManager.OnPlungerStart -= OnStartPlunger;

        m_ResetTrigger.OnReset -= OnResetBall;
    }

    private void InitializeToys()
    {
        m_Toys = FindObjectsOfType<ToyBase>();
        if (m_Toys != null && m_Toys.Length > 0)
        {
            for (int i = 0; i < m_Toys.Length; i++)
            {
                m_Toys[i].OnSendPoints += OnReceivePoints;
                m_Toys[i].Activate();
            }
        }
    }

    private void OnReceivePoints(int Points)
    {
        m_Points += Points;
        Debug.Log("Global Points : " + m_Points + " - Received Points : " + Points);
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

    private void OnStartPlunger()
    {
        m_Plunger.Push();
    }

    private void OnResetBall()
    {
        //Reset disabled toys on ball reset
        if (m_Toys != null && m_Toys.Length > 0)
        {
            for (int i = 0; i < m_Toys.Length; i++)
            {
                if (!m_Toys[i].IsActivated)
                {
                    m_Toys[i].ResetToy();
                    m_Toys[i].Activate();
                }
            }
        }

        //Reset ball position
        m_Ball.GetComponent<Rigidbody>().isKinematic = true;
        m_Ball.transform.position = m_DefaultBallPosition;
        m_Ball.GetComponent<Rigidbody>().isKinematic = false;
    }
}