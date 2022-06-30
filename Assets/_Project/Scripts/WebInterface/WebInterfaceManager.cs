using System;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

[Serializable]
public class WebOutputData
{
    public int score;
    public float time;
}

public class WebInterfaceManager : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern bool IsMobile();

        [DllImport("__Internal")]
        private static extern void ReadyCallback();
        [DllImport("__Internal")]
        static extern void StartCallback();
        [DllImport("__Internal")]
        static extern void EndCallback(string data);
#endif

    private static WebInterfaceManager m_Instance;
    private bool m_IsMobile;
    private bool m_IsReady;
    private float m_StartTime;

    [SerializeField]
    private bool m_dontDestroyOnLoad;

    public static WebInterfaceManager Instance
    {
        get { return m_Instance; }
        private set { m_Instance = value; }
    }

    private void Awake()
    {
        if (!m_Instance)
            m_Instance = this;

        if (m_dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        m_IsMobile = false;
#if !UNITY_EDITOR && UNITY_WEBGL
        m_IsMobile = IsMobile();
#endif
    }

    #region --C# to Javascript--
    public bool IsOnMobileDevice()
    {
        return m_IsMobile;
    }

    public void CallReadyGameCallback()
    {
        m_IsReady = true;
#if !UNITY_EDITOR && UNITY_WEBGL
    ReadyCallback();
#endif
        //Debug.Log("Ready !");
    }
    public void CallStartGameCallback()
    {
        m_IsReady = false;
        m_StartTime = Time.time;
#if !UNITY_EDITOR && UNITY_WEBGL
    StartCallback();
#endif
        //Debug.Log("Start !");
    }
    public void CallEndGameCallback()
    {
        WebOutputData output = new WebOutputData();
        output.time = Time.time - m_StartTime;
        output.score = GetCurrentScore();
        string data = JsonUtility.ToJson(output);

#if !UNITY_EDITOR && UNITY_WEBGL
    EndCallback(data);
#endif
        //Debug.Log("End !");
    }
    #endregion

    #region --Javascript to C#--
    public int GetCurrentScore()
    {
        return GameManager.Instance.Score;
    }
    public string GetCurrentState()
    {
        return StateMachine.Instance.CurrentState.GetName();
    }

    public bool RunGame()
    {
        State currentState = StateMachine.Instance.CurrentState;

        StartState startState = currentState as StartState;
        if (startState && m_IsReady)
        {
            startState.SwitchToNextState();
            return true;
        }

        GameOverState gameOverState = currentState as GameOverState;
        if (gameOverState)
        {
            gameOverState.SwitchToNextState();
            return true;
        }

        return false;
    }
    public bool GoHome()
    {
        State currentState = StateMachine.Instance.CurrentState;
        GameOverState gameOverState = currentState as GameOverState;
        if (gameOverState)
        {
            gameOverState.GoBacktoStartGameState();
            return true;
        }

        return false;
    }

#if UNITY_EDITOR
    [NaughtyAttributes.Button]
    private void DebugRunGame()
    {
        RunGame();
    }
    [NaughtyAttributes.Button]
    private void DebugGoHome()
    {
        GoHome();
    }
#endif
    #endregion
}