using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : State
{
    [Header("Game State")]
    [SerializeField]
    private InputManager m_InputManager;
    [SerializeField]
    private GameManager m_GameManager;

    [SerializeField]
    private RectTransform m_GamePanel;
    [SerializeField]
    private ScoreDisplayModel m_ScoreDisplayModel;

    private int m_CurrentScore;
    private int m_RemainingLives;

    public override void Enter(State from)
    {
        //Call Start event
        WebInterfaceManager.Instance.CallStartGameCallback();

        m_GameManager.OnGameOver += OngameOver;

        m_InputManager.EnableInputs();
        m_GameManager.Initialize(m_InputManager);
        m_GamePanel.gameObject.SetActive(true);
    }

    public override void Tick()
    {
        //Update score display
        if (m_GameManager && (m_GameManager.Score != m_CurrentScore || m_GameManager.RemainingLives != m_RemainingLives))
        {
            m_CurrentScore = m_GameManager.Score;
            m_RemainingLives = m_GameManager.RemainingLives;

            UpdateGUI();
        }
    }

    public override void Exit(State to)
    {
        m_InputManager.DisableInputs();
        m_GameManager.UnInitialize();
        m_GamePanel.gameObject.SetActive(false);

        m_GameManager.OnGameOver -= OngameOver;
    }

    public override string GetName()
    {
        return name;
    }

    private void UpdateGUI()
    {
        m_ScoreDisplayModel.Score = m_CurrentScore.ToString();
        m_ScoreDisplayModel.Lives = m_RemainingLives.ToString();
    }

    private void OngameOver()
    {
        SwitchToNextState();
    }
}