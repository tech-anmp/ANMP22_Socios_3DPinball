using UnityEngine;

public class DropTargetToy : GenericToyBase<DropTarget>
{
    [Header("Components")]
    [SerializeField]
    private DropTarget[] m_DropTargets;

    public override DropTarget[] GetToyComponents()
    {
        return m_DropTargets;
    }
}
