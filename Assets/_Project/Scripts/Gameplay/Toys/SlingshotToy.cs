using UnityEngine;

public class SlingshotToy : GenericToyBase<Slingshot>
{
    [Header("Components")]
    [SerializeField]
    private Slingshot[] m_Slingshots;

    public override Slingshot[] GetToyComponents()
    {
        return m_Slingshots;
    }
}
