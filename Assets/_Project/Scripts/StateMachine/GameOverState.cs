using UnityEngine;
using UnityEngine.UI;

public class GameOverState : State
{
    [Header("GameOver State")]
    [SerializeField]
    private RectTransform m_GameOverPanel;
    [SerializeField]
    private Button m_GameOverButton;

    public override void Enter(State from)
    {
        m_GameOverButton.interactable = true;
        m_GameOverButton.onClick.AddListener(OnReStartGameBtnClicked);
        m_GameOverPanel.gameObject.SetActive(true);
    }

    public override void Tick()
    {

    }

    public override void Exit(State to)
    {
        m_GameOverButton.interactable = false;
        m_GameOverButton.onClick.RemoveListener(OnReStartGameBtnClicked);
        m_GameOverPanel.gameObject.SetActive(false);
    }

    public override string GetName()
    {
        return name;
    }

    private void OnReStartGameBtnClicked()
    {
        StateMachine.SwitchState(m_NextState.GetName());
    }
}
