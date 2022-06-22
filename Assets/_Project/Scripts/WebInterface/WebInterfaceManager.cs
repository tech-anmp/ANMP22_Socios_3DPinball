using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

[Serializable]
public class WebOutputData
{
    public int score;
    public int multiplier;
    public float distance;
    public int coins;
    public string state;
    public float time;
    public bool isTutorial;
}

[Serializable]
public class LoyaltyPointsData
{
    public int Min;
    public int Owned;
}

[Serializable]
public class SendMessageCallbackData
{
    public string ObjectName;
    public string MethodName;
}

public class WebInterfaceManager : MonoBehaviour
{
#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern bool IsMobile();
        //[DllImport("__Internal")]
        //private static extern void GetOwnedLoyaltyPointsAsync(string data);
        //[DllImport("__Internal")]
        //private static extern void GetMinLoyaltyPointsAsync(string data);

        [DllImport("__Internal")]
        private static extern void ReadyCallback();
        [DllImport("__Internal")]
        static extern void StartCallback();
        [DllImport("__Internal")]
        static extern void EndCallback(string data);

        [DllImport("__Internal")]
        private static extern void ConsumeLoyaltyPointsCallback(int points);
#endif

    private static WebInterfaceManager m_Instance;
    private bool m_IsMobile;
    private bool m_IsReady;
    private float m_StartTime;

    private int m_OwnedLoyaltyPoints;
    private int m_MinLoyaltyPoints;

    //private Action<int> setOwnedPointsCallback;
    //private Action<int> setMinPointsCallback;

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

    /*public void GetOwnedPointsAsync(Action<int> callback)
    {
        setOwnedPointsCallback = callback;

#if !UNITY_EDITOR && UNITY_WEBGL
        SendMessageCallbackData data = new SendMessageCallbackData();
        data.ObjectName = gameObject.name;
        data.MethodName = "SetOwnedPoints";
        string callbackData = JsonUtility.ToJson(data);
        //GetOwnedLoyaltyPointsAsync(callbackData);
#else
        SetOwnedPoints(100);
#endif
    }*/
    public int GetOwnedPoints()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
    return m_OwnedLoyaltyPoints;
#endif
        return 100;
    }
    /*public void SetOwnedPoints(int points)
    {
        if (setOwnedPointsCallback != null)
        {
            setOwnedPointsCallback(points);
            setOwnedPointsCallback = null;
        }
    }*/
    /*public void GetMinPointsAsync(Action<int> callback)
    {
        setMinPointsCallback = callback;

#if !UNITY_EDITOR && UNITY_WEBGL
        SendMessageCallbackData data = new SendMessageCallbackData();
        data.ObjectName = gameObject.name;
        data.MethodName = "SetMinPoints";
        string callbackData = JsonUtility.ToJson(data);
        //GetMinLoyaltyPointsAsync(callbackData);
#else
        SetMinPoints(5);
#endif
    }*/
    public int GetMinPoints()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
    return m_MinLoyaltyPoints;
#endif
        return 5;
    }
    /*public void SetMinPoints(int points)
    {
        if (setMinPointsCallback != null)
        {
            setMinPointsCallback(points);
            setMinPointsCallback = null;
        }
    }*/
    public void CallConsumeLoyaltyPointsCallback(int points)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        ConsumeLoyaltyPointsCallback(points);
#endif
    }
    public void CallReadyGameCallback()
    {
        m_IsReady = true;
#if !UNITY_EDITOR && UNITY_WEBGL
    ReadyCallback();
#endif
        Debug.Log("Ready !");
    }
    public void CallStartGameCallback()
    {
        m_IsReady = false;
        m_StartTime = Time.time;
#if !UNITY_EDITOR && UNITY_WEBGL
    StartCallback();
#endif
        Debug.Log("Start !");
    }
    public void CallEndGameCallback()
    {
        WebOutputData output = new WebOutputData();
        output.time = Time.time - m_StartTime;
        output.score = GetCurrentScore();
        output.coins = GetCurrentCoins();
        output.distance = GetCurrentDistance();
        output.multiplier = GetCurrentMultiplier();
        output.state = GetCurrentState();
        output.isTutorial = IsTutorial();
        string data = JsonUtility.ToJson(output);

#if !UNITY_EDITOR && UNITY_WEBGL
    EndCallback(data);
#endif
        Debug.Log("End !");
    }
    #endregion

    #region --Javascript to C#--
    public int GetCurrentScore()
    {
        TrackManager trackManager = TrackManager.instance;
        if (trackManager)
            return trackManager.score;

        return 0;
    }
    public int GetCurrentMultiplier()
    {
        TrackManager trackManager = TrackManager.instance;
        if (trackManager)
            return trackManager.multiplier;

        return 0;
    }
    public float GetCurrentDistance()
    {
        TrackManager trackManager = TrackManager.instance;
        if (trackManager)
            return trackManager.worldDistance;

        return 0.0f;
    }
    public int GetCurrentCoins()
    {
        TrackManager trackManager = TrackManager.instance;
        if (trackManager)
            return trackManager.characterController.coins;

        return 0;
    }
    public string GetCurrentState()
    {
        GameManager gameManager = GameManager.instance;
        if (gameManager)
            return gameManager.topState.name;

        return "";
    }
    public bool IsTutorial()
    {
        TrackManager trackManager = TrackManager.instance;
        if (trackManager)
            return trackManager.isTutorial;

        return false;
    }

    public bool RunGame(string pointsObject)
    {
        LoyaltyPointsData points = JsonUtility.FromJson<LoyaltyPointsData>(pointsObject);

        if (points != null)
        {
            m_MinLoyaltyPoints = points.Min;
            m_OwnedLoyaltyPoints = points.Owned;
        }

        AState currentState = GameManager.instance.topState;

        LoadoutState loadoutState = currentState as LoadoutState;
        if (loadoutState && m_IsReady)
        {
            loadoutState.StartGame();
            return true;
        }

        GameOverState gameOverState = currentState as GameOverState;
        if (gameOverState)
        {
            gameOverState.RunAgain();
            return true;
        }

        return false;
    }  
    public bool GoHome()
    {
        AState currentState = GameManager.instance.topState;
        GameOverState gameOverState = currentState as GameOverState;
        if (gameOverState)
        {
            gameOverState.GoToLoadout();
            return true;
        }

        return false;
    }

#if UNITY_EDITOR
    [ContextMenu("Debug_RunGame")]
    private void DebugRunGame()
    {
        LoyaltyPointsData points = new LoyaltyPointsData();
        points.Min = 5;
        points.Owned = 100;
        string json = JsonUtility.ToJson(points);

        RunGame(json);
    }
    [ContextMenu("Debug_GoHome")]
    private void DebugGoHome()
    {
        GoHome();
    }
#endif
#endregion
}
