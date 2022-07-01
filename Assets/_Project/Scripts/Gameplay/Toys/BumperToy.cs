using UnityEngine;

public class BumperToy : GenericToyBase<Bumper>
{
    [SerializeField]
    private Bumper[] m_Bumpers;

    public override Bumper[] GetToyComponents()
    {
        return m_Bumpers;
    }
}
