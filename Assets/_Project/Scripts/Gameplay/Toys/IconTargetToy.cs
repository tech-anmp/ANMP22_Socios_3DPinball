using UnityEngine;

public class IconTargetToy : GenericToyBase<IconTarget>
{
    [Header("Components")]
    [SerializeField]
    private IconTarget[] m_IconTargets;

    public override IconTarget[] GetToyComponents()
    {
        return m_IconTargets;
    }
}
