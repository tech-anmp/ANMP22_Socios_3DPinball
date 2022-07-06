using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main Components")]
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

    [Header("Lives")]
    [SerializeField]
    private int m_MaxLives = 3;
    [SerializeField]
    private int m_MinPointsForExtraLife = 500;

    [Header("Plunger")]
    [SerializeField]
    private float m_PlungerMinPower = 20;
    [SerializeField]
    private float m_PlungerMaxPower = 100;

    [Header("Audio")]
    [SerializeField]
    private AudioClip m_LooseBallAudioClip;

    private Vector3 m_DefaultBallPosition;
    private ToyBase[] m_Toys;
    private ToyComponentBase[] m_ToyComponents;
    private BonusComponentBase[] m_Bonus;
    private ActiveableWall[] m_ActiveableWalls;
    private int m_Points;
    private int m_TargetPointsBeforeExtraLife;
    private int m_RemainingLives;

    private static GameManager m_Instance;
    public static GameManager Instance { get => m_Instance; }

    public int Score { get => m_Points; }
    public int RemainingLives { get => m_RemainingLives; }

    public Action OnGameOver;
    public Action OnBallReset;

    private InputManager m_InputManager;
    private AudioSource m_AudioSource;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
    }

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void Initialize(InputManager InputManager)
    {
        m_InputManager = InputManager;

        m_DefaultBallPosition = m_Ball.transform.position;
        m_Ball.GetComponent<Rigidbody>().isKinematic = false;

        InitializePinball();
        InitializeToyComponents();
        InitializeToys();
        InitializeBonus();

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

        UnInitializeBonus();
        UnInitializeToys();
        UnInitializeToyComponents();
    }

    private void InitializePinball()
    {
        m_Points = 0;
        m_TargetPointsBeforeExtraLife = m_MinPointsForExtraLife;
        m_RemainingLives = m_MaxLives;

        m_ActiveableWalls = FindObjectsOfType<ActiveableWall>();
        if (m_ActiveableWalls != null && m_ActiveableWalls.Length > 0)
        {
            for (int i = 0; i < m_ActiveableWalls.Length; i++)
            {
                m_ActiveableWalls[i].DeActivate();
            }
        }
    }
    private void InitializeBonus()
    {
        m_Bonus = GetComponentsInChildren<BonusComponentBase>();// FindObjectsOfType<BonusComponentBase>();
        if (m_Bonus != null && m_Bonus.Length > 0)
        {
            for (int i = 0; i < m_Bonus.Length; i++)
            {
                m_Bonus[i].OnSendBonus += OnReceiveBonusPoints;
                m_Bonus[i].Activate();
            }
        }
    }
    private void UnInitializeBonus()
    {
        m_Bonus = FindObjectsOfType<BonusComponentBase>();
        if (m_Bonus != null && m_Bonus.Length > 0)
        {
            for (int i = 0; i < m_Bonus.Length; i++)
            {
                m_Bonus[i].OnSendBonus -= OnReceiveBonusPoints;
                m_Bonus[i].DeActivate();
            }
        }
    }
    private void InitializeToys()
    {
        m_Toys = GetComponentsInChildren<ToyBase>();// FindObjectsOfType<ToyBase>();
        if (m_Toys != null && m_Toys.Length > 0)
        {
            for (int i = 0; i < m_Toys.Length; i++)
            {
                m_Toys[i].OnSendPoints += OnReceiveToyPoints;
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
                m_Toys[i].OnSendPoints -= OnReceiveToyPoints;
                m_Toys[i].DeActivate();
            }
        }
    }
    private void InitializeToyComponents()
    {
        m_ToyComponents = FindObjectsOfType<ToyComponentBase>();
        if(m_ToyComponents != null && m_ToyComponents.Length > 0)
        {
            for (int i = 0; i < m_ToyComponents.Length; i++)
            {
                m_ToyComponents[i].OnHit += OnReceiveToyComponentPoints;
            }
        }
    }
    private void UnInitializeToyComponents()
    {
        if (m_ToyComponents != null && m_ToyComponents.Length > 0)
        {
            for (int i = 0; i < m_ToyComponents.Length; i++)
            {
                m_ToyComponents[i].OnHit -= OnReceiveToyComponentPoints;
            }
        }
    }

    private void OnReceiveToyComponentPoints(ToyComponentBase ToyComponent)
    {
        if (ToyComponent) UpdateScore(ToyComponent.Points);
    }
    private void OnReceiveToyPoints(ToyBase Toy, int Points)
    {
        UpdateScore(Points);
    }
    private void OnReceiveBonusPoints(int Points)
    {
        UpdateScore(Points);
    }

    private void UpdateScore(int Points)
    {
        m_Points += Points;
        if(m_Points >= m_TargetPointsBeforeExtraLife)
        {
            m_TargetPointsBeforeExtraLife += m_MinPointsForExtraLife;
            m_RemainingLives += 1;
        }
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
        m_Plunger.Push(power);
    }

    private void OnResetBall()
    {
        if (OnBallReset != null)
            OnBallReset();

        m_AudioSource.PlayOneShot(m_LooseBallAudioClip);

        //Reset disabled toys on ball reset
        if (m_Toys != null && m_Toys.Length > 0)
        {
            for (int i = 0; i < m_Toys.Length; i++)
            {
                if (!m_Toys[i].IsActivated || m_Toys[i].RestartOnBallReset)
                {
                    m_Toys[i].Restart();
                }
            }
        }

        //Reset disabled bonus components
        if (m_Bonus != null && m_Bonus.Length > 0)
        {
            for (int i = 0; i < m_Bonus.Length; i++)
            {
                if(!m_Bonus[i].IsActivated || m_Bonus[i].RestartOnBallReset)
                {
                    m_Bonus[i].Restart();
                }
            }
        }

        //Reset activeable walls
        if (m_ActiveableWalls != null && m_ActiveableWalls.Length > 0)
        {
            for (int i = 0; i < m_ActiveableWalls.Length; i++)
            {
                m_ActiveableWalls[i].DeActivate();
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