using TMPro;
using UnityEngine;

public class IconTargetToy : GenericToyBase<IconTarget>
{
    [Header("Components")]
    [SerializeField]
    private IconTarget[] m_IconTargets;

    [Header("Display")]
    [SerializeField]
    private TextMeshPro m_PointsDisplay;

    protected override void Start()
    {
        base.Start();

        if (m_PointsDisplay) m_PointsDisplay.text = m_Points.ToString();
    }

    public override IconTarget[] GetToyComponents()
    {
        return m_IconTargets;
    }
}
