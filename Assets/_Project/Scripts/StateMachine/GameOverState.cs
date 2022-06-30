using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverState : State
{
    [Header("GameOver State")]
    [SerializeField]
    private State m_StartgameState;
    [SerializeField]
    private RectTransform m_GameOverPanel;
    [SerializeField]
    private Button m_GameOverButton;
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;

    public override void Enter(State from)
    {
        //Call End event
        WebInterfaceManager.Instance.CallEndGameCallback();

        m_GameOverButton.interactable = true;
        m_GameOverButton.onClick.AddListener(OnReStartGameBtnClicked);
        m_GameOverPanel.gameObject.SetActive(true);

        m_ScoreText.text = GameManager.Instance.Score.ToString();
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
        SwitchToNextState();
    }

    public void GoBacktoStartGameState()
    {
        StateMachine.Instance.SwitchState(m_StartgameState.GetName());
    }
}
