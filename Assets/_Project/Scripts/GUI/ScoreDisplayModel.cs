using TMPro;
using UnityEngine;

public class ScoreDisplayModel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_Text;

    public string Score 
    {
        get { return m_Text.text; }
        set { m_Text.text = value; }
    }
}
