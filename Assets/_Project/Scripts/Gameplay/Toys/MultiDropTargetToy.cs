using UnityEngine;

public class MultiDropTargetToy : MultiToyBase<DropTarget>
{
    [SerializeField]
    private DropTarget[] m_DropTargets;

    //private List<DropTarget> m_HittedDropTargets = new List<DropTarget>();

    /*protected override void Update()
    {
        if (!m_IsActivated)
            return;

        // Check if all bumpers were hitted, if so, then send points and deactivate
        if (m_HittedDropTargets.Count == m_DropTargets.Length)
        {
            SendPoints();
            DeActivate();
        }
    }*/

    /*public override void Activate()
    {
        if (m_DropTargets != null && m_DropTargets.Length > 0)
        {
            for (int i = 0; i < m_DropTargets.Length; i++)
            {
                m_DropTargets[i].OnHit += OnDropTargetHitted;
                m_DropTargets[i].SetActive(true);
            }
        }

        m_IsActivated = true;
    }*/

    /*public override void DeActivate()
    {
        m_IsActivated = false;

        if (m_DropTargets != null && m_DropTargets.Length > 0)
        {
            for (int i = 0; i < m_DropTargets.Length; i++)
            {
                m_DropTargets[i].OnHit -= OnDropTargetHitted;
                m_DropTargets[i].SetActive(false);
            }
        }
    }*/

    /*public override void ResetToy()
    {
        m_IsActivated = false;
        m_HittedDropTargets.Clear();

        if (m_DropTargets != null && m_DropTargets.Length > 0)
        {
            for (int i = 0; i < m_DropTargets.Length; i++)
            {
                m_DropTargets[i].OnHit -= OnDropTargetHitted;
                m_DropTargets[i].ResetToyComponent();
            }
        }
    }*/

    /*private void OnDropTargetHitted(ToyComponentBase ToyComponent)
    {
        DropTarget hittedDropTarget = ToyComponent as DropTarget;
        if (hittedDropTarget == null)
            return;

        if (m_DropTargets != null && m_DropTargets.Length > 0)
        {
            for (int i = 0; i < m_DropTargets.Length; i++)
            {
                if (m_DropTargets[i] == hittedDropTarget && !m_HittedDropTargets.Contains(hittedDropTarget))
                {
                    m_HittedDropTargets.Add(hittedDropTarget);

                    //Bumper will not be active until all bumpers are hitted
                    m_DropTargets[i].SetActive(false);
                }
            }
        }
    }*/

    public override DropTarget[] GetToyComponents()
    {
        return m_DropTargets;
    }
}
