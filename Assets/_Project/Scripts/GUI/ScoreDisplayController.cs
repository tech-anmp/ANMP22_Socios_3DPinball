using UnityEngine;

public class ScoreDisplayController : MonoBehaviour
{
    [SerializeField]
    private ScoreDisplayModel m_ScoreDisplayModel;

    private GameManager m_GameManagerRef;
    private int m_CurrentScore;

    private void Start()
    {
        m_GameManagerRef = GameManager.Instance;
    }

    private void Update()
    {
        if(m_GameManagerRef && m_GameManagerRef.Score != m_CurrentScore)
        {
            m_CurrentScore = m_GameManagerRef.Score;
            UpdateGUI();
        }
    }

    private void UpdateGUI()
    {
        m_ScoreDisplayModel.Score = m_CurrentScore.ToString("###");
    }
}
