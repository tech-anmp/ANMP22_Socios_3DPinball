using TMPro;
using UnityEngine;

public class ScoreDisplayModel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextScore;
    [SerializeField]
    private TextMeshProUGUI m_TextLives;

    public string Score 
    {
        get { return m_TextScore.text; }
        set { m_TextScore.text = value; }
    }
    public string Lives
    {
        get { return m_TextLives.text; }
        set { m_TextLives.text = value; }
    }
}
