using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    [SerializeField]
    private int m_MaxLives = 3;
    [SerializeField]
    private float m_PlungerMinPower = 20;
    [SerializeField]
    private float m_PlungerMaxPower = 100;

    private Vector3 m_DefaultBallPosition;
    private ToyBase[] m_Toys;
    private int m_Points;
    private int m_RemainingLives;

    private static GameManager m_Instance;
    public static GameManager Instance { get => m_Instance; }

    public int Score { get => m_Points; }
    public int RemainingLives { get => m_RemainingLives; }

    public Action OnGameOver;

    private InputManager m_InputManager;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
    }

    public void Initialize(InputManager InputManager)
    {
        m_InputManager = InputManager;

        m_DefaultBallPosition = m_Ball.transform.position;

        InitializePinball();
        InitializeToys();

        m_InputManager.OnLeftFlipperBtnDown += OnLeftFlip;
        m_InputManager.OnLeftFlipperBtnUp += OnLeftFlipBack;

        m_InputManager.OnRightFlipperBtnDown += OnRightFlip;
        m_InputManager.OnRightFlipperBtnUp += OnRightFlipBack;

        m_InputManager.OnPlungerBtnPressed += OnStartPlunger;

        m_ResetTrigger.OnReset += OnResetBall; 
    }

    public void UnInitialize()
    {
        m_InputManager.OnLeftFlipperBtnDown -= OnLeftFlip;
        m_InputManager.OnLeftFlipperBtnUp -= OnLeftFlipBack;

        m_InputManager.OnRightFlipperBtnDown -= OnRightFlip;
        m_InputManager.OnRightFlipperBtnUp -= OnRightFlipBack;

        m_InputManager.OnPlungerBtnPressed -= OnStartPlunger;

        m_ResetTrigger.OnReset -= OnResetBall;

        UnInitializeToys();
    }

    private void InitializePinball()
    {
        m_Points = 0;
        m_RemainingLives = m_MaxLives;
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
    private void UnInitializeToys()
    {
        if (m_Toys != null && m_Toys.Length > 0)
        {
            for (int i = 0; i < m_Toys.Length; i++)
            {
                m_Toys[i].OnSendPoints -= OnReceivePoints;
                m_Toys[i].ResetToy();
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

    private void OnStartPlunger(float PowerRatio)
    {
        float power = Mathf.Lerp(m_PlungerMinPower, m_PlungerMaxPower, PowerRatio);
        Debug.Log(power);
        m_Plunger.Push(power);
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

        //Check remaining lives
        m_RemainingLives -= 1;
        if (m_RemainingLives <= 0)
        {
            //GameOver
            if (OnGameOver != null)
                OnGameOver();
        }

        //Reset ball position
        m_Ball.GetComponent<Rigidbody>().isKinematic = true;
        m_Ball.transform.position = m_DefaultBallPosition;
        m_Ball.GetComponent<Rigidbody>().isKinematic = false;
    }
}