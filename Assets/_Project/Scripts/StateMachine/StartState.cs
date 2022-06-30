using UnityEngine;
using UnityEngine.UI;

public class StartState : State
{
    [Header("Start State")]
    [SerializeField]
    private RectTransform m_StartPanel;
    [SerializeField]
    private Button m_StartGameButton;

    public override void Enter(State from)
    {
        m_StartGameButton.interactable = true;
        m_StartGameButton.onClick.AddListener(OnStartGameBtnClicked);
        m_StartPanel.gameObject.SetActive(true);
    }

    public override void Tick()
    {

    }

    public override void Exit(State to)
    {
        m_StartGameButton.interactable = false;
        m_StartGameButton.onClick.RemoveListener(OnStartGameBtnClicked);
        m_StartPanel.gameObject.SetActive(false);
    }

    public override string GetName()
    {
        return name;
    }

    private void OnStartGameBtnClicked()
    {
        StateMachine.SwitchState(m_NextState.GetName());
    }
}
